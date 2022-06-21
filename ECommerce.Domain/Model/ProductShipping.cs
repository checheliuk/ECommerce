using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Model
{
    public class ProductShipping
    {
        [Key]
        public int ProductShippingID { get; set; }
        public int ShippingDetailsID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int SizeID { get; set; }
        public int ColorID { get; set; }
        public decimal Price { get; set; }

        public virtual ShippingDetails ShippingDetails { get; set; }
        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
        public virtual Color Color { get; set; }
    }
}
