using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeSplit.Common.Entities.Org;

namespace WeSplit.Common.Entities.Consumable
{
    public class RecipientItem
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public double Amount { get; set; }

        public Currency Currency { get; set; }

        public ICollection<User> ConsumedByUsers { get; set; } = new List<User>();
    }
}
