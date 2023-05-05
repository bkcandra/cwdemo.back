using cwdemo.data.Entities;
using cwdemo.data.Interfaces;
using cwdemo.infrastructure;

namespace cwdemo.data
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly List<CatalogEntity> _catalogEntities;

        public CatalogRepository()
        {
            _catalogEntities = Singleton<List<CatalogEntity>>.Instance;
        }

        public async Task<CatalogEntity> GetById(long id)
        {
            return _catalogEntities.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<CatalogEntity>> GetAll()
        {
            return _catalogEntities;
        }

        public async Task<CatalogEntity> Add(CatalogEntity entity)
        {
            entity.Id = _catalogEntities.Count > 0 ? _catalogEntities.Max(x => x.Id) + 1 : 1;
            _catalogEntities.Add(entity);
            return entity;
        }

        public async Task<CatalogEntity> Update(CatalogEntity entity)
        {
            var existingEntity = await GetById(entity.Id);
            if (existingEntity == null)
            {
                return null;
            }
            existingEntity.Name = entity.Name;
            existingEntity.Description = entity.Description;
            existingEntity.Price = entity.Price;
            existingEntity.Active = entity.Active;
            existingEntity.Type = entity.Type;
            return existingEntity;
        }

        public async Task<bool> Delete(long id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                return false;
            }
            _catalogEntities.Remove(entity);
            return true;
        }
    }

}
