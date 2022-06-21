using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ECommerce.Domain.Model
{
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        [DataType(DataType.MultilineText)]
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string SeoText { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
