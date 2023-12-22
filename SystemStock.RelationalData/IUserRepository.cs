
using Microsoft.EntityFrameworkCore;
using SystemStock.RelationalData.Entities;

namespace SystemStock.RelationalData
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUserById(long Id);
        Task SaveChanges();
        DbSet<UserEntity> GetDbSetUser();
        Task<List<UserEntity?>> GetUserByEmail(string? email);

    }
}
