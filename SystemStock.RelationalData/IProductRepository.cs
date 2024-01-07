using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.RelationalData.Entities;

namespace SystemStock.RelationalData
{
    public interface IProductRepository
    {
        Task SaveChanges();
        DbSet<ProductEntity> GetDbSetProduct();
        Task<ProductEntity?> GetByName(string Name, long UserId);
        Task<ProductEntity?> GetById(long ProductId, long UserId);
        Task<List<ProductEntity>> GetList(long UserId);
    }
}
