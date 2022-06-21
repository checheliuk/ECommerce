using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ECommerce.Domain.Model.EnumModel;

namespace ECommerce.Domain.Model
{
    public class ShippingDetails
    {
        [Key]
        public int ShippingDetailsID { get; set; }
        public string UserID { get; set; }
        public DateTime Active { get; set; }
        public DateTime Create { get; set; }
        public ShippingStatus Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DeliveryService Delivery { get; set; }
        public TypePayment Payment { get; set; }
        public int AddressAreaID { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public string Warehouses { get; set; }
        public string ZipCode { get; set; }
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }
        public string Number { get; set; }
        public decimal Total { get; set; }
        public AddressArea AddressArea { get; set; }
        public virtual ICollection<ProductShipping> ProductShippings { get; set; }
    }
}
