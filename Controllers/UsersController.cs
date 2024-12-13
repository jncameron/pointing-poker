using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointingPoker.Data;
using PointingPoker.Models;

namespace PointingPokerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> JoinSession(int sessionId, string name)
        {
            var session = await _context.Sessions.FindAsync(sessionId);
            if (session == null) return NotFound();

            var user = new User { SessionId = sessionId, Name = name };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
    }
}