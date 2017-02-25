using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using Smalldebts.Backend.DataObjects;
using Smalldebts.Backend.ExposedModels;
using Smalldebts.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Smalldebts.Backend.Controllers
{
    [MobileAppController]
    public class DebtsController : ApiController
    {

        MobileServiceContext Context;
        public DebtsController()
        {
            Context = new MobileServiceContext();
        }
        // GET api/values
        public Debt Get()
        {
            return Context.Debts.FirstOrDefault();
        }

        // GET api/values
        public Debt Post(NewDebt newDebt)
        {
            var debt = Context.Debts.Create();
            debt.Id = Guid.NewGuid().ToString();
            debt.Name = newDebt.Name;
            debt.Balance = newDebt.Amount;

            var newMovement = Context.Movements.Create();
            newMovement.Id = Guid.NewGuid().ToString();
            newMovement.Amount = newDebt.Amount;
            newMovement.Date = DateTimeOffset.UtcNow;
            debt.Movements.Add(newMovement);
            Context.Debts.Add(debt);
            Context.SaveChanges();

            return debt;
        }
    }
}