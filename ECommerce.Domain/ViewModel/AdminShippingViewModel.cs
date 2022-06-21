using ECommerce.Domain.Model;
using System.Collections.Generic;

namespace ECommerce.Domain.ViewModel
{
    public class AdminShippingViewModel
    {
        public List<ProductShipping> products = new List<ProductShipping>();
        public ShippingDetails shipping = new ShippingDetails();
        public bool IsSavedSuccess { get; set; }
    }
}
