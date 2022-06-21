using ECommerce.Domain.Model;
using System.Collections.Generic;

namespace ECommerce.Domain.ViewModel
{
    public class ShippingViewModel
    {
        public List<ProductShipping> products = new List<ProductShipping>();
        public ShippingDetails shipping = new ShippingDetails();
        public decimal Total { get; set; }
    }
}
