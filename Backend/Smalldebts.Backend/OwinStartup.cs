using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Smalldebts.Backend.OwinStartup))]

namespace Smalldebts.Backend
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // Para obtener más información acerca de cómo configurar su aplicación, visite http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
