using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Entities
{
    public class Attachement : BaseAuditClass
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string EntityId { get; set; }
        public EntityType EntityType { get; set; }
    }
}
