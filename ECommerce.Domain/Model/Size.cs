using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Model
{
    public class Size
    {
        [Key]
        public int SizeID { get; set; }
        public string Title { get; set; }
        public virtual ICollection<ProductSize> ProductSizes { get; set; }
        public virtual ICollection<ProductShipping> ProductShippings { get; set; }
    }
}
