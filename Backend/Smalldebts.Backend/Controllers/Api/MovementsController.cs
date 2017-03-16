using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.Mobile.Server.Config;
using Smalldebts.Backend.Models;

namespace Smalldebts.Backend.Controllers.Api
{
    [MobileAppController]
    public class MovementsController : ApiController
    {

        MobileServiceContext Context;
        public MovementsController()
        {
            Context = new MobileServiceContext();
        }

        [Authorize]
        [Route("{id}")]
        public List<ItermediateObjects.Movement> GetMovements(string id)
        {
            var userId = User.Identity.GetUserId();
            var debt = Context.Debts.FirstOrDefault(m => m.Id == id && m.UserId == userId);
            if (debt == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var movements = Context.Movements.Where(m => m.DebtId == id).OrderByDescending(m => m.UpdatedAt ?? m.CreatedAt);
            return AutoMapper.Mapper.Map<List<ItermediateObjects.Movement>>(movements);
        }

        [Authorize]
        [Route("{debtId}")]
        public ItermediateObjects.Movement GetMovement(string debtId, [FromUri] string movementId)
        {
            var userId = User.Identity.GetUserId();
            var debt = Context.Debts.FirstOrDefault(m => m.Id == debtId && m.UserId == userId);
            if (debt == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var movement = Context.Movements.FirstOrDefault(m => m.DebtId == debtId && m.Id == movementId);
            return AutoMapper.Mapper.Map<ItermediateObjects.Movement>(movement);
        }

        [Authorize]
        public ItermediateObjects.Movement Put(ItermediateObjects.Movement modifiedMovement)
        {
            var userId = User.Identity.GetUserId();
            var debt = Context.Debts.FirstOrDefault(m => m.Id == modifiedMovement.DebtId && m.UserId == userId);
            if(debt == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var movement = Context.Movements.FirstOrDefault(m => m.DebtId == modifiedMovement.DebtId && m.Id == modifiedMovement.Id);
            movement.Reason = modifiedMovement.Reason;
            movement.UpdatedAt = DateTimeOffset.UtcNow;
            Context.SaveChanges();
            return AutoMapper.Mapper.Map<ItermediateObjects.Movement>(movement);
        }
    }
}