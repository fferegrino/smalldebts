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
        public const string ApplicationUrl = "http://192.168.0.101//smalldebts";
        //public const string ApplicationUrl = "https://smalldebts-test.azurewebsites.net";
#else
        public const string ApplicationUrl = "https://smalldebts-test.azurewebsites.net";
#endif

    }
}
