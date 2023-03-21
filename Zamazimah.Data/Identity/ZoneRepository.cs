using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamazimah.Data.Context;
using Zamazimah.Data.Generic.Repositories;
using Zamazimah.Entities.Identity;
using Zamazimah.Models;

namespace Zamazimah.Data.Repositories
{
    public class ZoneRepository : BaseRepository<Zone>, IZoneRepository
    {
        public ZoneRepository(ZamazimahContext context) : base(context) { }
        public bool Exist(string name)
        {
            return context.Zones.Where(z => z.Name == name).Count() > 0;
        }

        public bool Exist(int id, string name)
        {
            return context.Zones.Where(z => z.Name == name && z.Id == id).Count() > 0;
        }

        public bool IsParentOrGrandParent(int? parentZoneId, Zone zone)
        {
            if (zone == null)
            {
                return false;
            }
            if (zone.ParentId == parentZoneId)
            {
                return true;
            }
            else if (zone.ParentId == null)
            {
                return false;
            }
            else
            {
                return IsParentOrGrandParent(parentZoneId, zone.Parent);
            }
        }

        public Zone GetRootZone() // Seems we're only supposed to have one root zone only
        {
            return context.Zones.Where(z => z.ParentId == null).FirstOrDefault();
        }

    }
    public interface IZoneRepository : IRepository<Zone>
    {
        bool Exist(string name);
        bool Exist(int id, string name);
        bool IsParentOrGrandParent(int? parentZoneId, Zone zone);
        Zone GetRootZone();
    }
}
