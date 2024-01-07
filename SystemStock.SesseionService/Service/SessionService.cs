namespace SystemStock.SesseionService.Service
{
    public class SessionService : ISessionService
    {
        private const string SESSION_USER_ID = "uid";
        private const string SESSION_USER_NAME = "uname";

        public long? UserId { get => GetSessionData<long?>(SESSION_USER_ID); set => SetSessionData(SESSION_USER_ID, value); }
        public string? UserName { get => GetSessionData<string?>(SESSION_USER_NAME); set => SetSessionData(SESSION_USER_NAME,value); }


        private Dictionary<string, object> _sessionData = new Dictionary<string, object>();

        public virtual T GetSessionData<T>(string key, T @default = default(T))
        {
            if (_sessionData.ContainsKey(key))
            {
                return (T)_sessionData[key];
            }

            return @default;
        }

        public virtual void SetSessionData(string key, object data)
        {
            if (_sessionData.ContainsKey(key))
            {
                _sessionData[key] = data;
            }
            else
            {
                _sessionData.Add(key, data);
            }
        }
    }
}