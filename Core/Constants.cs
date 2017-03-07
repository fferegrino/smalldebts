using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smalldebts.Core.UI
{
    public class Constants
    {


#if DEBUG
        //public const string ApplicationUrl = "http://192.168.0.101/smalldebts";
        public const string ApplicationUrl = "https://test-smalldebts.azurewebsites.net";
#else
        public const string ApplicationUrl = "https://test-smalldebts.azurewebsites.net";
#endif

		public const string UserId = "UserId";
		public const string Token = "Token";
		public const string UserEmail = "UserEmail";
		public const string UserPassword = "UserPassword";
		public const string TokenExpirationDate = "TokenExpirationDate";

    }
}
