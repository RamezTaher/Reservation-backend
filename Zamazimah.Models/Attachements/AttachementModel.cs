using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamazimah.Models.Generic;

namespace Zamazimah.Models.Attachements
{
    public class AttachementModel : BaseGetModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
