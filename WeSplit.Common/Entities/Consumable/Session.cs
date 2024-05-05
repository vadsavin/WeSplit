using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeSplit.Common.Entities.Org;

namespace WeSplit.Common.Entities.Consumable
{
    public class Session
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime? FinishedAt { get; set; }

        public DateTime LastChangedAt { get; set; }

        public IEnumerable<User> Users { get; set; }

        public EntityStatus Status { get; set; }

        public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();

        public Session()
        {
            StartedAt = DateTime.UtcNow;
            LastChangedAt = DateTime.UtcNow;
            Status = EntityStatus.Opened;
        }
    }
}
