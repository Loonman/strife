using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.Domain.UserStorage
{
    class UserStore : IUserStore
    {
        private const string tokenIndex = "authToken"; 
        private string _token = null;
        private Windows.Storage.ApplicationDataContainer _settings;

        public UserStore ()
        {
            _settings = Windows.Storage.ApplicationData.Current.LocalSettings;
        }

        public string getToken()
        {
            if (_token != null)
            {
                return _token;
            }
            else
            {
                if (hasToken())
                {
                    return (string)_settings.Values[tokenIndex];
                }
                throw new NotAuthenticatedException();
            }
        }

        public bool hasToken()
        {
            return _settings.Values[tokenIndex] != null;
        }

        public void setToken(string token)
        {
            _settings.Values[tokenIndex] = token;
            _token = token;
        }
    }
}
