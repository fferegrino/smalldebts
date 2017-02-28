using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smalldebts.Core.UI.Services
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }
}
