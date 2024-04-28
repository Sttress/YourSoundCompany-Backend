
using Microsoft.EntityFrameworkCore;
using YourSoundCompnay.RelationalData.Entities;

namespace YourSoundCompnay.RelationalData
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUserById(long? Id);
        Task SaveChanges();
        DbSet<UserEntity> GetDbSetUser();
        Task<List<UserEntity?>> GetUserByEmail(string? email);

    }
}
