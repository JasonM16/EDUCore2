using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebMVC.Services;

namespace CoursePlanner.UnitTests.Application
{
    public class CartControllerTest
    {
        private readonly Mock<ICourseService> _courseServiceMock;
        private readonly Mock<ICoursePlannerService> _coursePlannerServiceMock;
        private readonly Mock<IIdentityParser<Student>> _identityParserMock;
        private readonly Mock<HttpContext> _loggerMock;

        public CartControllerTest(
            Mock<ICourseService> courseServiceMock,
            Mock<ICoursePlannerService> coursePlannerServiceMock,
            Mock<IIdentityParser<Student>> identityParserMock,
            Mock<HttpContext> loggerMock
            )
        {

        }



    }
}
