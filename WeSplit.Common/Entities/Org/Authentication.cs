using System.ComponentModel.DataAnnotations;

namespace WeSplit.Common.Entities.Org
{
    public class Authentication
    {
        [Key]
        public Guid Id { get; set; }

        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

        public TimeSpan TimeToLive { get; set; }
    }
}
