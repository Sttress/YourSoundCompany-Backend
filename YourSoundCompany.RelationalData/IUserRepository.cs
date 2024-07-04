
using Microsoft.EntityFrameworkCore;
using YourSoundCompany.RelationalData;
using YourSoundCompnay.RelationalData.Entities;

namespace YourSoundCompnay.RelationalData
{
    public interface IUserRepository : IRepositoryBase<UserEntity>
    {
        Task<List<UserEntity?>> GetUserByEmail(string? email);
        Task<List<UserEntity>> GetUserListInactive();
    }
}
