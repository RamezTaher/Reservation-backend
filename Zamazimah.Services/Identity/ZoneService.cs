using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamazimah.Data.Context;
using Zamazimah.Data.Repositories;
using Zamazimah.Entities.Identity;
using Zamazimah.Models.Identity;

namespace Zamazimah.Services
{
    public class ZoneService : IZoneService
    {
        private readonly IZoneRepository _zoneRepository;
        private readonly IApplicationUserRepository _userRepository;

        public ZoneService(IZoneRepository zoneRepository, IApplicationUserRepository userRepository)
        {
            _zoneRepository = zoneRepository;
            _userRepository = userRepository;
        }

        public Zone GetById(object id)
        {
            return _zoneRepository.GetById(id);
        }

        //public IEnumerable<Zone> GetAll()
        //{
        //    return _zoneRepository.GetAll();
        //}

        public int Create(Zone zone)
        {
            _zoneRepository.Insert(zone);
            _zoneRepository.SaveChanges();

            return zone.Id;
        }

        public int Delete(Zone zone)
        {
            _zoneRepository.Remove(zone);
            int rows = _zoneRepository.SaveChanges();
            return rows;
        }

        public int Update(Zone oldZone, Zone zone)
        {
            oldZone.Name = zone.Name;
            int rows = _zoneRepository.SaveChanges();
            return rows;
        }

        public bool HasSameName(string name)
        {
            return _zoneRepository.Exist(name);
        }

        public bool HasSameName(int id, string name)
        {
            return _zoneRepository.Exist(id, name);
        }


        public Zone GetRootZone()
        {
            return _zoneRepository.GetRootZone();
        }
    }

    public interface IZoneService
    {
        Zone GetById(object id);
        //IEnumerable<Zone> GetAll();
        int Create(Zone zone);
        int Delete(Zone zone);
        int Update(Zone oldZone, Zone zone);
        bool HasSameName(string name);
        bool HasSameName(int id, string name);
        Zone GetRootZone();
    }
}
