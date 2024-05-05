using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WeSplit.Common.Entities.Org;

namespace WeSplit.Common.Entities.Consumable
{
    public class Currency
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CurrencyCode { get; set; }
    }
}
