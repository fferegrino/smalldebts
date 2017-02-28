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
        //_serviceClient = new MobileServiceClient("http://192.168.0.100/smalldebts");
        public const string ApplicationUrl = "https://smalldebts-test.azurewebsites.net";
#else
        public const string ApplicationUrl = "https://smalldebts-test.azurewebsites.net";
#endif

    }
}
