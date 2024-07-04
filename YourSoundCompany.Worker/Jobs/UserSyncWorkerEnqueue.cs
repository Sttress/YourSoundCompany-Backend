using Hangfire;
using Sync;

namespace YourSoundCompany.Worker.Jobs
{
    public class UserSyncWorkerEnqueue
    {
        private readonly IUserSyncService _userSyncService;

        public UserSyncWorkerEnqueue(IUserSyncService userSyncService)
        {
            _userSyncService = userSyncService;
        }

        public void Execute()
        {
            _userSyncService.DeleteUserDrash();
        }
    }
}
