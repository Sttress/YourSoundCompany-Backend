using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.RelationalData.Entities;

namespace SystemStock.RelationalData
{
    public interface IStoreProductRepository
    {
        Task SaveChanges();
        DbSet<StoreProductEntity> GetDbSetStoreProduct();
        Task<List<StoreProductEntity>> GetByStore(long storeId);
    }
}
