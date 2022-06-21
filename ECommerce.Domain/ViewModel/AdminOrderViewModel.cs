using ECommerce.Domain.Model;
using System.Collections.Generic;

namespace ECommerce.Domain.ViewModel
{
    public class AdminOrderViewModel
    {
        public List<ShippingDetails> Orders { get; set; }
        public int Page { get; set; }
        public int LastPage { get; set; }
    }
}
