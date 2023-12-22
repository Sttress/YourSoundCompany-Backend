

using Microsoft.EntityFrameworkCore;
using SystemStock.RelationalData.Entities;

namespace SystemStock.RelationalData.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly ContextDB _contextDB;

        public UserRepository(ContextDB contextDb)
        {
            _contextDB = contextDb;
        }
        public async Task<UserEntity?> GetUserById(long Id)
        {
            return await _contextDB.User.FindAsync(Id);
        }
        public async Task SaveChanges()
        {
            await _contextDB.SaveChangesAsync();
        }

        public DbSet<UserEntity> GetDbSetUser()
        {
            return _contextDB.User;
        }
        public async Task<List<UserEntity?>> GetUserByEmail(string? email)
        {
            return await _contextDB.User.Where(e => e.Email == email).ToListAsync();
        }
    }
}
