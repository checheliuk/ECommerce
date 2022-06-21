using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ECommerce.Domain.Model
{
    public class TypeProduct
    {
        [Key]
        public int TypeProductID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string SeoTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string SeoDescription { get; set; }
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string SeoText { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
