using BuildingBlocks.EventBus.Abstractions;
using CoursePlanner.API.IntegrationEvents.Events;
using CoursePlanner.API.Models;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Threading.Tasks;

namespace CoursePlanner.API.IntegrationEvents.EventHandlers
{
    public class CourseRemovedFromPlannerIntegrationEventHandler : IIntegrationEventHandler<CourseRemovedFromPlannerIntegrationEvent>
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<CourseRemovedFromPlannerIntegrationEventHandler> _logger;

        public CourseRemovedFromPlannerIntegrationEventHandler(
            IBasketRepository repository,
            ILogger<CourseRemovedFromPlannerIntegrationEventHandler> logger
            )
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(CourseRemovedFromPlannerIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                await _repository.DeleteCoursePlannerAsync(@event.StudentId.ToString());
            }
        }
    }
}