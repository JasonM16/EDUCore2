using BuildingBlocks.EventBus.Abstractions;
using CoursePlanner.API.IntegrationEvents.Events;
using CoursePlanner.API.Models;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Threading.Tasks;

namespace CoursePlanner.API.IntegrationEvents.EventHandlers
{
    public class CourseAvailablilityIntegrationEventHandler : IIntegrationEventHandler<CourseAvailablilityIntegrationEvent>
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<CourseAvailablilityIntegrationEventHandler> _logger;

        public CourseAvailablilityIntegrationEventHandler(
            IBasketRepository repository,
            ILogger<CourseAvailablilityIntegrationEventHandler> logger
            )
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(CourseAvailablilityIntegrationEvent @event)
        {
            //using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            //{
            //    _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            //    var userIds = _repository.GetUsers();

            //    foreach (var id in userIds)
            //    {
            //        var basket = await _repository.GetBasketAsync(id);

            //        await UpdatePriceInBasketItems(@event.ProductId, @event.NewPrice, @event.OldPrice, basket);
            //    }
            //}
        }


        //private async Task UpdatePriceInBasketItems(int productId, decimal newPrice, decimal oldPrice, CustomerBasket basket)
        //{
        //    string match = productId.ToString();
        //    var itemsToUpdate = basket?.Items?.Where(x => x.ProductId == match).ToList();

        //    if (itemsToUpdate != null)
        //    {
        //        _logger.LogInformation("----- ProductPriceChangedIntegrationEventHandler - Updating items in basket for user: {BuyerId} ({@Items})", basket.BuyerId, itemsToUpdate);

        //        foreach (var item in itemsToUpdate)
        //        {
        //            if (item.UnitPrice == oldPrice)
        //            {
        //                var originalPrice = item.UnitPrice;
        //                item.UnitPrice = newPrice;
        //                item.OldUnitPrice = originalPrice;
        //            }
        //        }
        //        await _repository.UpdateBasketAsync(basket);
        //    }
        //}
    }
}