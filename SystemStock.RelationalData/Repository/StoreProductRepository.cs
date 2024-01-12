using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.RelationalData.Entities;

namespace SystemStock.RelationalData.Repository
{
    public class StoreProductRepository : IStoreProductRepository
    {
        private readonly ContextDB _contextDb;

        public StoreProductRepository(ContextDB contextDb)
        {
            _contextDb = contextDb;
        }

        public async Task SaveChanges()
        {
            await _contextDb.SaveChangesAsync();
        }

        public DbSet<StoreProductEntity> GetDbSetStoreProduct()
        {
            return _contextDb.StoreProduct;
        }

        public async Task<List<StoreProductEntity>> GetByStore(long storeId)
        {
            int size = 20;
            int page = 1;
            int indiceInicial = (page - 1) * size;

            return await _contextDb.StoreProduct.Where(e => e.StoreId == storeId)
                                              .OrderBy(e => e.Id)
                                              .Skip(indiceInicial)
                                              .Take(size)
                                              .ToListAsync();
        }

    }
}
