using Zamazimah.Data.Repositories;
using Zamazimah.Entities;
using Zamazimah.Generic.Models;
using Zamazimah.Models.Attachements;
using static Zamazimah.Core.Enums.EntitiesEnums;

namespace Zamazimah.Services
{
    public class AttachementService : IAttachementService
    {

        private readonly IAttachementRepository _attachementRepository;

        public AttachementService(IAttachementRepository attachementRepository)
        {
            _attachementRepository = attachementRepository;
        }

        public List<AttachementModel> GetAttachements(string entityId, EntityType entityType)
        {
            var attachements = this._attachementRepository.Get(x=>x.EntityId == entityId && x.EntityType == entityType);
            var results = attachements.Select(x => new AttachementModel
            {
                Id = x.Id,
                Created = x.Created,
                Modified = x.Modified,
                Title = x.Title,
                Url = x.Url,
            }).ToList();
            return results;
        }
        public Attachement GetById(int id)
        {
            return _attachementRepository.GetById(id);
        }

        public int Create(CreateAttachementModel model)
        {
            var Attachement = new Attachement
            {
                Title = model.Title,
                EntityId = model.EntityId,
                EntityType = model.EntityType,
            };
            _attachementRepository.Insert(Attachement);
            _attachementRepository.SaveChanges();
            return Attachement.Id;
        }

        public void Update(Attachement oldAttachement, CreateAttachementModel model)
        {
            oldAttachement.Title = model.Title;
            _attachementRepository.SaveChanges();
        }

        public void Delete(Attachement Attachement)
        {
            _attachementRepository.Remove(Attachement);
            _attachementRepository.SaveChanges();
        }
    }

    public interface IAttachementService
    {
        Attachement GetById(int id);
        int Create(CreateAttachementModel model);
        void Update(Attachement oldAttachement, CreateAttachementModel newModel);
        public void Delete(Attachement Attachement);
        List<AttachementModel> GetAttachements(string entityId, EntityType entityType);
    }
}
