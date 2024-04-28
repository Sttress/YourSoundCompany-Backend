using Microsoft.EntityFrameworkCore;
using YourSoundCompnay.RelationalData.Entities;

namespace YourSoundCompnay.RelationalData
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options) { }
        public DbSet<UserEntity> User { get; set; }

    }
}
