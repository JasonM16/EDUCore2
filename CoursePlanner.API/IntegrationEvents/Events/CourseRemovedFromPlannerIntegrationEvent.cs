using BuildingBlocks.EventBus.Events;

namespace CoursePlanner.API.IntegrationEvents.Events
{
    public class CourseRemovedFromPlannerIntegrationEvent : IntegrationEvent
    {
        public string StudentId { get; set; }
    }
}