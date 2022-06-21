using System.Collections.Generic;

namespace ECommerce.Domain.ViewModel
{
    public class AdminAssignedViewModel
    {
        public List<AssignedData> AssignedData { get; set; }
        public int ProductID { get; set; }
        public bool IsSavedSuccess { get; set; }
    }
}
