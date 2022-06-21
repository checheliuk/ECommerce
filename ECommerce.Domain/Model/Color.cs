using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Model
{
    public class Color
    {
        [Key]
        public int ColorID { get; set; }
        public string Title { get; set; }
        public virtual ICollection<ProductColor> ProductColor { get; set; }
        public virtual ICollection<ProductShipping> ProductShippings { get; set; }
    }
}
