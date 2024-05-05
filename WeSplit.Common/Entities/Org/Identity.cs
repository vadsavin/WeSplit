using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeSplit.Common.Entities.Org
{
    public class Identity
    {
        [Key]
        public Guid Guid { get; set; }

        public IdentityType Type { get; set; }
    }
}
