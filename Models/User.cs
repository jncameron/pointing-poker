namespace PointingPoker.Models
{
  public class User
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public int SessionId { get; set; }
    public Session Session { get; set; }
  }
}