using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointingPoker.Models
{
  public class Session
  {
    public int Id { get; set; }
    public string Code { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    public Guid FacilitatorId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Active"; // Active, Expired

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();

    [Timestamp]
    public byte[] RowVersion { get; set; }
  }
}