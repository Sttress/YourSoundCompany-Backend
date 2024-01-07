
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

        public async Task<List<CategoryEntity>> GetList(long UserId)
        {
            return await _contextDb.Category.Where(e => e.UserId == UserId).OrderByDescending(e => e.Name).ToListAsync();
        }

        public async Task<CategoryEntity>? GetByName(string Name, long UserId)
        {
            return await _contextDb.Category.Where(e => e.Name == Name && e.UserId == UserId).FirstOrDefaultAsync();
        }

        public async Task<CategoryEntity>? GetById(long CategoryId, long UserId)
        {
            return await _contextDb.Category.Where(e => e.Id == CategoryId && e.UserId == UserId).FirstOrDefaultAsync();
        }
    }
}
