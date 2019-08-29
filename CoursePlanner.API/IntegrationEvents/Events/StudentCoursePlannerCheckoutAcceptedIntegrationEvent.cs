using BuildingBlocks.EventBus.Events;
using System;

namespace CoursePlanner.API.IntegrationEvents.Events
{
    public class StudentCoursePlannerCheckoutAcceptedIntegrationEvent : IntegrationEvent
    {
        public string StudentId { get; }
        public string StudentName { get; }
        public Guid RequestId { get; set; }
        public Models.CoursePlanner Planner { get; }

        public StudentCoursePlannerCheckoutAcceptedIntegrationEvent(string studentId, string studentName, Guid requestId,
            Models.CoursePlanner planner)
        {
            StudentId = studentId;
            StudentName = studentName;
            RequestId = requestId;
            Planner = planner;
        }
    }
}
