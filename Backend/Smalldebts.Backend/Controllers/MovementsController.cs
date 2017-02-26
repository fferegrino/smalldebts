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
            var debt = Context.Movements.Where(m => m.DebtId == id);
            return AutoMapper.Mapper.Map<List<ItermediateObjects.Movement>>(debt);
        }

        [Route("{debtId}/{movementId}")]
        public ItermediateObjects.Movement GetMovement(string debtId, string movementId)
        {
            var debt = Context.Movements.FirstOrDefault(m => m.DebtId == debtId && m.Id == movementId);
            return AutoMapper.Mapper.Map<ItermediateObjects.Movement>(debt);
        }
    }
}