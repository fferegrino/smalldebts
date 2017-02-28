﻿using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using Smalldebts.Backend.DataObjects;
using Smalldebts.ItermediateObjects;
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
        
        [Route("{id}")]
        public ItermediateObjects.Debt Get(string id)
        {
            var debt = Context.Debts.FirstOrDefault(d => d.Id == id);
            return AutoMapper.Mapper.Map<ItermediateObjects.Debt>(debt);
        }

        public List<ItermediateObjects.Debt> Get()
        {
            var debt = Context.Debts;
            return AutoMapper.Mapper.Map<List<ItermediateObjects.Debt>>(debt);
        }

        // GET api/values
        public ItermediateObjects.Debt Post(ItermediateObjects.Debt newDebt)
        {
            var debt = Context.Debts.Create();
            debt.Id = Guid.NewGuid().ToString();
            debt.Name = newDebt.Name;
            debt.Balance = newDebt.Balance;

            var newMovement = Context.Movements.Create();
            newMovement.Id = Guid.NewGuid().ToString();
            newMovement.Amount = newDebt.Balance;
            newMovement.Date = DateTimeOffset.UtcNow;
            newMovement.Reason = newDebt.Reason;

            debt.Movements.Add(newMovement);
            Context.Debts.Add(debt);
            Context.SaveChanges();
            return AutoMapper.Mapper.Map<ItermediateObjects.Debt>(debt);
        }

        public ItermediateObjects.Debt Put(ItermediateObjects.Debt newDebt)
        {
            var debt = Context.Debts.FirstOrDefault(d => d.Id == newDebt.Id);
            debt.Balance += newDebt.Balance;

            var newMovement = Context.Movements.Create();
            newMovement.Id = Guid.NewGuid().ToString();
            newMovement.Amount = newDebt.Balance;
            newMovement.Reason = newDebt.Reason;
            newMovement.Date = DateTimeOffset.UtcNow;
            debt.Movements.Add(newMovement);

            Context.SaveChanges();
            return AutoMapper.Mapper.Map<ItermediateObjects.Debt>(debt);
        }
        
        public ItermediateObjects.Debt Delete(ItermediateObjects.Debt newDebt)
        {
            var debt = Context.Debts.FirstOrDefault(d => d.Id == newDebt.Id);
            var amount = debt.Balance;
            debt.Balance = 0;

            var newMovement = Context.Movements.Create();
            newMovement.Id = Guid.NewGuid().ToString();
            newMovement.Amount = -amount;
            newMovement.Date = DateTimeOffset.UtcNow;

            debt.Movements.Add(newMovement);
            Context.Debts.Remove(debt);
            Context.SaveChanges();

            return AutoMapper.Mapper.Map<ItermediateObjects.Debt>(debt);
        }
    }
}