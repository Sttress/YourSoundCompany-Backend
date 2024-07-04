using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourSoundCompnay.SesseionService
{
    public interface ISessionService
    {
        long? UserId { get; set; }
        string SpotifyToken {  get; set; }
        string SpotifyRefreshToken { get; set; }

        void SetSessionData(string key, object data);

        T GetSessionData<T>(string key, T @default = default(T));
    }
}
