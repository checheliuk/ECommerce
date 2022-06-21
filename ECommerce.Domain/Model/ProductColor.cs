using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Model
{
    public class ProductColor
    {
        [Key, Column(Order = 0)]
        public int ProductID { get; set; }
        [Key, Column(Order = 1)]
        public int ColorID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Color Color { get; set; }
        public int? Count { get; set; }
    }
}
