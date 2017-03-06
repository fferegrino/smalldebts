using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using Smalldebts.Backend.DataObjects;
using Smalldebts.ItermediateObjects;
using Smalldebts.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Smalldebts.Data;

namespace Smalldebts.Backend.Controllers
{
    [MobileAppController]
    public class MovementsController : ApiController
    {

        MobileServiceContext Context;
        public MovementsController()
        {
            Context = new MobileServiceContext();
        }

        [Route("{id}")]
        public List<ItermediateObjects.Movement> GetMovements(string id)
        {
            var debt = Context.Movements.Where(m => m.DebtId == id).OrderByDescending(m => m.UpdatedAt ?? m.CreatedAt);
            return AutoMapper.Mapper.Map<List<ItermediateObjects.Movement>>(debt);
        }

        [Route("{debtId}")]
        public ItermediateObjects.Movement GetMovement(string debtId, [FromUri] string movementId)
        {
            var debt = Context.Movements.FirstOrDefault(m => m.DebtId == debtId && m.Id == movementId);
            return AutoMapper.Mapper.Map<ItermediateObjects.Movement>(debt);
        }
    }
}