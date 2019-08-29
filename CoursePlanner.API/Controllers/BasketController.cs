using BuildingBlocks.EventBus.Abstractions;
using CoursePlanner.API.IntegrationEvents.Events;
using CoursePlanner.API.Model;
using CoursePlanner.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CoursePlanner.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IIdentityService _identityService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<BasketController> _logger;

        public BasketController(
            ILogger<BasketController> logger,
            IBasketRepository repository,
            IIdentityService identityService,
            IEventBus eventBus)
        {
            _logger = logger;
            _repository = repository;
            _identityService = identityService;
            _eventBus = eventBus;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Model.CoursePlanner), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Models.CoursePlanner>> GetCoursePlannerByIdAsync(string id)
        {
            var basket = await _repository.GetCoursePlannerAsync(id);

            return Ok(basket ?? new Models.CoursePlanner(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Models.CoursePlanner), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Models.CoursePlanner>> UpdateBasketAsync([FromBody]Models.CoursePlanner value)
        {
            return Ok(await _repository.UpdateCoursePlannerAsync(value));
        }

        [Route("checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync([FromBody]BasketCheckout coursePlannerCheckout, [FromHeader(Name = "x-requestid")] string requestId)
        {
            var userId = _identityService.GetUserIdentity();

            coursePlannerCheckout.RequestId = (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ?
                guid : coursePlannerCheckout.RequestId;

            var planner = await _repository.GetCoursePlannerAsync(userId);

            if (planner == null)
            {
                return BadRequest();
            }

            var userName = User.FindFirst(x => x.Type == "unique_name").Value;

            var eventMessage = new StudentCoursePlannerCheckoutAcceptedIntegrationEvent(
                userId, 
                userName, 
                //coursePlannerCheckout.City, 
                //coursePlannerCheckout.Street,
                //coursePlannerCheckout.State, 
                //coursePlannerCheckout.Country, 
                //coursePlannerCheckout.ZipCode, 
                //coursePlannerCheckout.CardNumber, 
                //coursePlannerCheckout.CardHolderName,
                //coursePlannerCheckout.CardExpiration, 
                //coursePlannerCheckout.CardSecurityNumber, 
                //coursePlannerCheckout.CardTypeId, 
                //coursePlannerCheckout.Buyer, 
                coursePlannerCheckout.RequestId, 
                planner);

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process
            try
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", eventMessage.Id, Program.AppName, eventMessage);

                _eventBus.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName}", eventMessage.Id, Program.AppName);

                throw;
            }

            return Accepted();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task DeleteBasketByIdAsync(string id)
        {
            await _repository.DeleteCoursePlannerAsync(id);
        }
    }
}
