using System.Linq;
using System.Threading.Tasks;
using Backend.Dto;
using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GenerateTest()
        {
            var questions = await _context.Set<Question>().Include(x => x.Theme).GroupBy(x => x.Theme.Title).Select(x => new Test()
            {
                Theme = x.Key,
                Questions = x.Select(y => new TestQuestion(){Text = y.Text, Content = y.Content}).ToArray()
            }).ToArrayAsync();
            return Ok(questions);
        }
    }
}