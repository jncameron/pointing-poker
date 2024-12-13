using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointingPoker.Models;
using PointingPoker.Data;

namespace PointingPokerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SessionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] Session session)
        {
            if (session == null)
            {
                return BadRequest();
            }

            // Initialize RowVersion if not provided
            if (session.RowVersion == null || session.RowVersion.Length == 0)
            {
                session.RowVersion = new byte[8];
            }

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSession), new { code = session.Code }, session);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetSession(string code)
        {
            var session = await _context.Sessions
                .Include(s => s.Users)
                .Include(s => s.Votes)
                .FirstOrDefaultAsync(s => s.Code == code);

            if (session == null) return NotFound();
            return Ok(session);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSessions()
        {
            var sessions = await _context.Sessions
                .Include(s => s.Users)
                .Include(s => s.Votes)
                .ToListAsync();

            return Ok(sessions);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession(int id, [FromBody] Session updatedSession)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null) return NotFound();

            _context.Entry(session).CurrentValues.SetValues(updatedSession);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Concurrency conflict occurred. The session was modified by another user.");
            }

            return Ok(session);
        }
    }
}