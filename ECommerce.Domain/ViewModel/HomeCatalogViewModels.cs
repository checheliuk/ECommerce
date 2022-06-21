using ECommerce.Domain.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ECommerce.Domain.ViewModel
{
    public class HomeCatalogViewModels
    {
        public List<Product> Products { get; set; }
        public int Page { get; set; }
        public int LastPage { get; set; }
        public string Type { get; set; }
        public string Sex { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        [AllowHtml]
        public string SeoText { get; set; }
        public decimal LowPrice { get; set; }
        public decimal HighPrice { get; set; }
        public int Count { get; set; }
        public string Order { get; set; }
        public string View { get; set; }
    }
}
