using Microsoft.AspNetCore.SignalR;

namespace PointingPokerBackend.Hubs
{
  public class PokerHub : Hub
  {
    public async Task JoinSession(string sessionCode, string userName)
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, sessionCode);
      await Clients.Group(sessionCode).SendAsync("UserJoined", userName);
    }

    public async Task SubmitVote(string sessionCode, string userId, string voteValue)
    {
      await Clients.Group(sessionCode).SendAsync("VoteSubmitted", userId, voteValue);
    }

    public async Task RevealVotes(string sessionCode)
    {
      await Clients.Group(sessionCode).SendAsync("VotesRevealed");
    }

    public async Task EndSession(string sessionCode)
    {
      await Clients.Group(sessionCode).SendAsync("SessionEnded");
    }
  }
}
