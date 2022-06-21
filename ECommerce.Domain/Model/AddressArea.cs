using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Model
{
    public class AddressArea
    {
        [Key]
        public int AddressAreaID { get; set; }
        public String Description { get; set; }
        public bool IsVisible { get; set; }
        public ICollection<ShippingDetails> ShippingDetails { get; set; }
    }
}
