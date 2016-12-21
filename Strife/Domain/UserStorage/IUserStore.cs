using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.Domain.UserStorage
{
    interface IUserStore
    {

        bool hasToken();

        void setToken(string token);

        string getToken();
    }
}
