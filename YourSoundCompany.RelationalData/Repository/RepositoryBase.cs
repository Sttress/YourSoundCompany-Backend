using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSoundCompnay.RelationalData;

namespace YourSoundCompany.RelationalData.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ContextDB _contextDB;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(ContextDB contextDB) 
        {
            _contextDB = contextDB;
            _dbSet = contextDB.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            var result = await _dbSet.AddAsync(entity);
            await _contextDB.SaveChangesAsync();
            return result.Entity;
        }

        public async Task Delete(long id)
        {
            var entity = await GetById(id);
            if(entity is not null)
            {
                _dbSet.Remove(entity);
                await SaveChanges();
            }
        }

        public async Task<T> GetById(long? Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task SaveChanges()
        {
            await _contextDB.SaveChangesAsync();
        }

        public async Task<T> Update(T entity)
        {
            var result = _dbSet.Update(entity);
            await SaveChanges();
            return result.Entity;
        }
    }
}
