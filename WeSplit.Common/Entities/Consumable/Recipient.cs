using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeSplit.Common.Entities.Org;

namespace WeSplit.Common.Entities.Consumable
{
    public class Recipient
    {
        [Key]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public ICollection<RecipientItem> Items { get; set; }

        public DateTime? PayedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastModifiedAt { get; set; }

        public EntityStatus Status { get; set; }

        public ICollection<User> PayedByUsers { get; set; }

        [NotMapped]
        public User Author { get; set; }

        public Recipient()
        {
            CreatedAt = DateTime.UtcNow;
            LastModifiedAt = DateTime.UtcNow;
            Items = new List<RecipientItem>();
            PayedByUsers = new List<User>();
            Status = EntityStatus.Opened;
        }
    }
}
