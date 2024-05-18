using System.ComponentModel.DataAnnotations;

namespace WeSplit.Common.Entities.Org
{
    public class AuthenticationGarant
    {
        [Key]
        public Guid Id { get; set; }

        [Key]
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

        public TimeSpan TimeToLive { get; set; }
    }
}
