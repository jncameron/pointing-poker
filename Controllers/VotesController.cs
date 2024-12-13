using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointingPoker.Data;
using PointingPoker.Models;

namespace PointingPoker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VotesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitVote(int sessionId, Guid userId, string voteValue)
        {
            var session = await _context.Sessions.FindAsync(sessionId);
            if (session == null) return NotFound();

            var vote = new Vote { SessionId = sessionId, UserId = userId, VoteValue = voteValue };
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
            return Ok(vote);
        }

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetVotes(int sessionId)
        {
            var votes = await _context.Votes
                .Where(v => v.SessionId == sessionId)
                .ToListAsync();
            return Ok(votes);
        }
    }
}