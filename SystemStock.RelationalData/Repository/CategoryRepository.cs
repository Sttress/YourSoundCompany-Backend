
using Microsoft.EntityFrameworkCore;
using SystemStock.RelationalData.Entities;

namespace SystemStock.RelationalData.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ContextDB _contextDb;

        public CategoryRepository(ContextDB contextDb)
        {
            _contextDb = contextDb;
        }

        public async Task SaveChanges()
        {
            await _contextDb.SaveChangesAsync();
        }

        public DbSet<CategoryEntity> GetDbSetCategory()
        {
            return _contextDb.Category;
        }

        public async Task<List<CategoryEntity>> GetList()
        {
            return await _contextDb.Category.OrderByDescending(e => e.Name).ToListAsync();
        }

        public async Task<CategoryEntity>? GetByName(string Name)
        {
            return await _contextDb.Category.Where(e => e.Name == Name).FirstOrDefaultAsync();
        }
    }
}
