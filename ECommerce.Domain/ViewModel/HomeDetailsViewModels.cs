using ECommerce.Domain.Model;
using System.Collections.Generic;

namespace ECommerce.Domain.ViewModel
{
    public class HomeDetailsViewModels
    {
        public Product Product { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
