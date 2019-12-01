using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ClassroomContext _context;

        public BooksController(ClassroomContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _context.Set<Book>().ToArrayAsync();
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBook([FromBody] Book book)
        {
            await _context.Set<Book>().AddAsync(book);
            await _context.SaveChangesAsync();
            var books = await _context.Set<Book>().ToArrayAsync();
            return Ok(new {id = book.Id, books});
        }

        [HttpGet("{bookId}/chapters")]
        public async Task<IActionResult> GetThemes(int bookId)
        {
            return Ok(await _context.Set<Chapter>().Where(x => x.BookId == bookId).ToArrayAsync());
        }

        [HttpPost("{bookId}/chapters")]
        public async Task<IActionResult> AddTheme(int bookId, [FromBody] Chapter chapter)
        {
            chapter.BookId = bookId;
            await _context.Set<Chapter>().AddAsync(chapter);
            await _context.SaveChangesAsync();
            return Ok(new { id = chapter.Id, chapters = await _context.Set<Chapter>().ToArrayAsync() });
        }
    }
}