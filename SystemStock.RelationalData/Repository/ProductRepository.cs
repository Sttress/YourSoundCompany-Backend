
using Microsoft.EntityFrameworkCore;
using SystemStock.RelationalData.Entities;

namespace SystemStock.RelationalData.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ContextDB _contextDB;

        public ProductRepository(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }

        public async Task SaveChanges()
        {
            await _contextDB.SaveChangesAsync();
        }

        public DbSet<ProductEntity> GetDbSetProduct()
        {
            return _contextDB.Product;
        }

        public async Task<ProductEntity?> GetByName(string Name, long UserId)
        {
            return await _contextDB.Product.Where(e => e.Name == Name && e.UserId == UserId).FirstOrDefaultAsync();
        }

        public async Task<ProductEntity?> GetById(long ProductId,long UserId)
        {
            return await _contextDB.Product.Where(e => e.Id == ProductId &&  e.UserId == UserId).FirstOrDefaultAsync();
        }

        public async Task<List<ProductEntity>> GetList(long UserId)
        {
            int size = 20;
            int page = 1;
            int indiceInicial = (page - 1) * size;

            return await _contextDB.Product.Where(e => e.UserId == UserId)
                                              .OrderBy(e => e.Id)
                                              .Skip(indiceInicial)
                                              .Take(size)
                                              .ToListAsync();
        }
    }
}
