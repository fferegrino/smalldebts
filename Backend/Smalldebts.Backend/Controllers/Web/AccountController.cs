using Smalldebts.IntermediateObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smalldebts.Backend.Controllers.Web
{
    public class AccountController : Controller
    {
        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Create(AccountModelBinding incomingUser)
        {
            try
            {
                return View();
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Forgotten()
        {
            return View();
        }
    }
}
