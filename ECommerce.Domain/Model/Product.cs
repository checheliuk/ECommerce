using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using static ECommerce.Domain.Model.EnumModel;

namespace ECommerce.Domain.Model
{
    public class Product
    {
        public Product()
        {
            this.Categories = new HashSet<Category>();
        }

        [Key]
        public int ProductID { get; set; }
        public string Url { get; set; }
        [Display(Name = "Названия")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [AllowHtml]
        public string Description { get; set; }
        public ProductStatus Status { get; set; }
        [Display(Name = "Скидка %")]
        public int? Discount { get; set; }
        [Display(Name = "Цена + Скидка")]
        public decimal? FakePrice { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        public string PathToTitlePhoto { get; set; }
        public decimal? ProductRating { get; set; }
        public int? TotalRaters { get; set; }
        public int? CountRatings { get; set; }
        public Sex Sex { get; set; }
        public int TypeProductID { get; set; }
        public TypeProduct TypeProduct { get; set; }
        [Display(Name = "Порядковий номер")]
        public int? Order { get; set; }
        public string PathToOriginPhoto { get; set; }
        public virtual ICollection<ProductShipping> ProductShippings { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<ProductSize> ProductSizes { get; set; }
        public virtual ICollection<ProductColor> ProductColors { get; set; }
    }
}
