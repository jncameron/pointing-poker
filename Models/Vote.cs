namespace PointingPoker.Models
{
  public class Vote
  {
    public int Id { get; set; }
    public int SessionId { get; set; }
    public Session Session { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string VoteValue { get; set; } = string.Empty;
    public DateTime VotedAt { get; set; } = DateTime.UtcNow;
  }
}