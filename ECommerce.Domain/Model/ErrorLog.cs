using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Model
{
    public class ErrorLog
    {
        [Key]
        public int ErrorLogID { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime Time { get; set; }
    }
}
