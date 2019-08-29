using System;

namespace CoursePlanner.API.Infrastructure.Middlewares
{
    public class PlannerDomainException : Exception
    {
        public PlannerDomainException()
        { }

        public PlannerDomainException(string message)
            : base(message)
        { }

        public PlannerDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
