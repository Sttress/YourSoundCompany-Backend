
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
                //var usersInactive = await _userRepository.GetUserListInactive();

                //foreach(var user in usersInactive)
                //{
                //    var usersByEmail = await _userRepository.GetUserByEmail(user.Email);

                //    if (usersByEmail.Any(e => e.))
                //    {
                //        await _userRepository.Delete(user.Id);
                //    }
                //}
            }
            catch(Exception ex) 
            {
                throw new Exception("Erro on delete user drash");
            }
        }
    }
}
