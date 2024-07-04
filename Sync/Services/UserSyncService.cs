using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourSoundCompnay.RelationalData;

namespace Sync.Services
{
    public class UserSyncService : IUserSyncService
    {
        private readonly IUserRepository _userRepository;
        public UserSyncService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task DeleteUserDrash()
        {
            try 
            {
                var usersInactive = await _userRepository.GetUserListInactive();
            }
            catch(Exception ex) 
            {
                throw new Exception("Erro on delete user drash");
            }
        }
    }
}
