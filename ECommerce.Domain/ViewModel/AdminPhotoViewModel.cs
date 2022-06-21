using ECommerce.Domain.Model;
using System.Collections.Generic;

namespace ECommerce.Domain.ViewModel
{
    public class AdminPhotoViewModel
    {
        public List<Photo> Photos { get; set; }
        public int ProductID { get; set; }
        public bool IsSavedSuccess { get; set; } 
    }
}
