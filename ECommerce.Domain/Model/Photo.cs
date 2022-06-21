using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Model
{
    public class Photo
    {
        [Key]
        public int PhotoID { get; set; }
        public int ProductID { get; set; }
        public string Path { get; set; }
        public string Thumbnail { get; set; }
        [Display(Name = "Порядковий номер")]
        public int? Order { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
