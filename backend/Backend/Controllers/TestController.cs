using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dto;
using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ClassroomContext _context;

        public TestController(ClassroomContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateTest([FromBody] TestGeneration testGeneration)
        {
            var questions = await _context.Set<Question>().Include(x => x.Chapter)
                .Where(x => testGeneration.SelectedChapters.Contains(x.ChapterId.Value))
                .GroupBy(x => x.Chapter.Title).Select(x => new Test()
            {
                Theme = x.Key,
                Questions = x.Select(y => new TestQuestion(){Text = y.Text, Content = y.Content}).ToArray()
            }).ToArrayAsync();
            
            var tests = new List<Test>();
            foreach (var question in questions)
            {
                tests.Add(Generate(question, testGeneration.Percentage));
            }

            var serialized = JsonConvert.SerializeObject(tests, Formatting.Indented);
            using (var f = System.IO.File.CreateText("test.json"))
            {
                await f.WriteAsync(serialized);
            }
            return Ok(tests);
        }

        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            using (var f = System.IO.File.OpenText("test.json"))
            {
                var serialized = await f.ReadToEndAsync();
                return Ok(JsonConvert.DeserializeObject(serialized));
            }
        }

        private Test Generate(Test test, int percentage)
        {
            var numberOfQuestions = Math.Floor(test.Questions.Count * percentage / 100M);
            var newTest = new Test()
            {
                Theme = test.Theme,
                Questions = new List<TestQuestion>()
            };
            var rand = new Random(DateTime.Now.Millisecond);
            while (newTest.Questions.Count < numberOfQuestions)
            {
                var ix = rand.Next(0, test.Questions.Count);
                var q = test.Questions.ElementAt(ix);
                if (!newTest.Questions.Contains(q))
                {
                    newTest.Questions.Add(q);
                }
            }

            return newTest;
        }
    }
}