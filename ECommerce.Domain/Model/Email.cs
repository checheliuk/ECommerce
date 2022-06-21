using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Model
{
    public class Email
    {
        [Key]
        public int EmailID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}
