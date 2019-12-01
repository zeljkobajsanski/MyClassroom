using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemesController : ControllerBase
    {
        private readonly ClassroomContext _context;

        public ThemesController(ClassroomContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/questions")]
        public async Task<IActionResult> GetQuestions(int id)
        {
            var questions = await _context.Set<Question>().Where(x => x.ThemeId == id).ToListAsync();
            questions.Add(new Question());
            return Ok(questions);
        }

        [HttpPost("{id}/questions")]
        public async Task<IActionResult> SaveQuestion(int id, [FromBody] Question question)
        {
            question.ThemeId = id;
            _context.Set<Question>().Update(question);
            await _context.SaveChangesAsync();
            var questions = await _context.Set<Question>().Where(x => x.ThemeId == id).ToListAsync();
            questions.Add(new Question());
            return Ok(new {id = question.Id, questions});
        }
    }
}