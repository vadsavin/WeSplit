using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeSplit.Common.Entities.Consumable;

namespace WeSplit.Common.Entities.Org
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public long TelegramId { get; set; }

        public string? DefaultPaymentInfo { get; set; }

        public Currency? DefaultCurrency { get; set; }

        public DateTime CreateAt { get; set; }

        public string? AuthentificatonToken { get; set; }

        public DateTime? AuthentificatonLastUpdated { get; set; }

        public virtual ICollection<RecipientItem> RecipientItems { get; set; } = new List<RecipientItem>();
        public virtual ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();
        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
        public virtual ICollection<AuthenticationGarant> AuthenticationGarant { get; set; } = new List<AuthenticationGarant>();
    }
}
