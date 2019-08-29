using BuildingBlocks.EventBus.Abstractions;
using CoursePlanner.API.IntegrationEvents.Events;
using CoursePlanner.API.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CoursePlanner.API.IntegrationEvents.EventHandlers
{
    public  class CourseTimesChangedIntegrationEventHandler : IIntegrationEventHandler<CourseTimesChangedIntegrationEvent>
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<CourseTimesChangedIntegrationEventHandler> _logger;

        public CourseTimesChangedIntegrationEventHandler(IBasketRepository repository,
            ILogger<CourseTimesChangedIntegrationEventHandler> logger
            )
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Handle(CourseTimesChangedIntegrationEvent @event)
        {

        }
            
    }
    
}