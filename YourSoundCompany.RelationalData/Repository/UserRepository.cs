

using Microsoft.EntityFrameworkCore;
using YourSoundCompany.RelationalData.Repository;
using YourSoundCompnay.RelationalData.Entities;

namespace YourSoundCompnay.RelationalData.Repository
{
    public class UserRepository : RepositoryBase<UserEntity>,IUserRepository
    {
        public readonly ContextDB _contextDB;
        public UserRepository(ContextDB contextDB) : base(contextDB)
        {
            _contextDB = contextDB;
        }


        public async Task<List<UserEntity?>> GetUserByEmail(string? email)
        {
            return await _contextDB.User.Where(e => e.Email == email).ToListAsync();
        }

        public async Task<List<UserEntity>> GetUserListInactive()
        {
            return await _contextDB.User.Where(e => e.Active == true).ToListAsync();
        }
    }
}
