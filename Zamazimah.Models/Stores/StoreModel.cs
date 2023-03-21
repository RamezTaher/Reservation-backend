using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;

namespace Zamazimah.Models.Stores
{
    public class StoreModel : BaseGetModel
    {
        public string StoreNumber { get; set; }
        public string? Title { get; set; }
        public string Address { get; set; }
        public string ResponsableFirstName { get; set; }
        public string ResponsableLastName { get; set; }
        public string ResponsableEmail { get; set; }
        public string ResponsablePhone { get; set; }
        public int Capacity { get; set; }
        public int AvailableQuantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int ActualQuantity { get; set; }
        public int AlertQuantity { get; set; }
    }
    public class StoreAutocompleteModel : StoreModel
    {
        public string Text { get; set; }
    }
}
