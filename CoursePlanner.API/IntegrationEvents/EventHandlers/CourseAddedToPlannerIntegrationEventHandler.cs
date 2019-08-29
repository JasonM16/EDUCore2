using BuildingBlocks.EventBus.Abstractions;
using CoursePlanner.API.IntegrationEvents.Events;
using CoursePlanner.API.Models;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace CoursePlanner.API.IntegrationEvents.EventHandlers
{
    public class CourseAddedToPlannerIntegrationEventHandler : IIntegrationEventHandler<CourseAddedToPlannerIntegrationEvent>
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<CourseAddedToPlannerIntegrationEventHandler> _logger;
        public CourseAddedToPlannerIntegrationEventHandler(
            IBasketRepository repository,
            ILogger<CourseAddedToPlannerIntegrationEventHandler> logger
            )
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task Handle(CourseAddedToPlannerIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

                await _repository.DeleteCoursePlannerAsync(@event.StudentId.ToString());
            }
        }
    }
}