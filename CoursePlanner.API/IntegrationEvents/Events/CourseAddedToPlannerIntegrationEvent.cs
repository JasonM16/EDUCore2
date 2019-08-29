using BuildingBlocks.EventBus.Events;

namespace CoursePlanner.API.IntegrationEvents.Events
{
    public class CourseAddedToPlannerIntegrationEvent : IntegrationEvent
    {
        public string StudentId { get; }
        public CourseAddedToPlannerIntegrationEvent(string studentId) => StudentId = studentId;
    }
}