
using Microsoft.EntityFrameworkCore;
using SystemStock.RelationalData.Entities;

namespace SystemStock.RelationalData.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ContextDB _contextDb;

        public StoreRepository(ContextDB contextDB)
        {
            _contextDb = contextDB;
        }

        public async Task SaveChanges()
        {
            await _contextDb.SaveChangesAsync();
        }

        public DbSet<StoreEntity> GetDbSetStore()
        {
            return _contextDb.Store;
        }

        public async Task<List<StoreEntity>> GetByUser(long Id)
        {
            int size = 20;
            int page = 1;
            int indiceInicial = (page - 1) * size;

            return await _contextDb.Store.Where(e => e.UserId == Id)
                                              .OrderBy(e => e.Id)
                                              .Skip(indiceInicial)
                                              .Take(size)
                                              .ToListAsync();
        }

        public async Task<StoreEntity?> GetById(long Id)
        {
            return await _contextDb.Store.Where(e => e.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<StoreEntity?> GetByName(string name)
        {
            return await _contextDb.Store.Where(e => e.Name == name).FirstOrDefaultAsync();
        }

    }
}
