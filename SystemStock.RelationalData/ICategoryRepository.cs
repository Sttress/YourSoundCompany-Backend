using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.RelationalData.Entities;

namespace SystemStock.RelationalData
{
    public interface ICategoryRepository
    {
        Task<List<CategoryEntity>> GetList(long UserId);
        DbSet<CategoryEntity> GetDbSetCategory();
        Task SaveChanges();
        Task<CategoryEntity>? GetByName(string Name, long UserId);
        Task<CategoryEntity>? GetById(long CategoryId, long UserId);
    }
}
