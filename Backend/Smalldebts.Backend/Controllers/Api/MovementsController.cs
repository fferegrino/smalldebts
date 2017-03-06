using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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