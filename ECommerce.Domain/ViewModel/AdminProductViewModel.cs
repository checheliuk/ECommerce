
using ECommerce.Domain.Model;
using System.Collections.Generic;

namespace ECommerce.Domain.ViewModel
{
    public class AdminProductViewModel
    {
        public Product Product { get; set; }
        public List<TypeProduct> TypeProducts { get; set; }
        public List<Category> Categories { get; set; }
        public List<AssignedCategoryData> AssignedCategoryData { get; set; }
        public bool IsSavedSuccess { get; set; }
    }
}
