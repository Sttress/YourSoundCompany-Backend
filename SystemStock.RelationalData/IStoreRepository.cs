using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.RelationalData.Entities;

namespace SystemStock.RelationalData
{
    public interface IStoreRepository
    {
        Task<StoreEntity>? GetByName(string name);
        Task<StoreEntity>? GetById(long Id);
        Task<List<StoreEntity>> GetByUser(long Id);
        DbSet<StoreEntity> GetDbSetStore();
        Task SaveChanges();
        Task<List<StoreEntity>> GetList(long UserId);
    }
}
