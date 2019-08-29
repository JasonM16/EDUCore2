using BuildingBlocks.EventBus.Abstractions;
using CoursePlanner.API.Controllers;
using CoursePlanner.API.IntegrationEvents.Events;
using CoursePlanner.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using ICoursePlannerIdentityService = CoursePlanner.API.Services.IIdentityService;

namespace CoursePlanner.UnitTests.Application
{
    public class CoursePlannerWebApiTest
    {
        private readonly Mock<IBasketRepository> _coursePlannerRepositoryMock;
        private readonly Mock<ICoursePlannerIdentityService> _identityServiceMock;
        private readonly Mock<IEventBus> _serviceBusMock;
        private readonly Mock<ILogger<BasketController>> _loggerMock;

        public CoursePlannerWebApiTest()
        {
            _coursePlannerRepositoryMock = new Mock<IBasketRepository>();
            _identityServiceMock = new Mock<ICoursePlannerIdentityService>();
            _serviceBusMock = new Mock<IEventBus>();
            _loggerMock = new Mock<ILogger<BasketController>>();
        }

        [Fact]
        public async Task Get_Student_Course_Planner_Success()
        {
            // Arrange
            var fakeStudentId = "1";
            var fakeStudentCoursePlanner = GetFakeStudentPlanner(fakeStudentId);

            _coursePlannerRepositoryMock.Setup(x => x.GetCoursePlannerAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeStudentCoursePlanner));

            _identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeStudentId);

            _serviceBusMock.Setup(x => x.Publish(It.IsAny<StudentCoursePlannerCheckoutAcceptedIntegrationEvent>()));

            // Act
            var coursePlannerController = new BasketController(
                _loggerMock.Object,
                _coursePlannerRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object
                );

            var actionResult = await coursePlannerController.GetCoursePlannerByIdAsync(fakeStudentId);

            // Assert
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal((((ObjectResult)actionResult.Result).Value as API.Models.CoursePlanner).StudentId, fakeStudentId);
        }



        [Fact]
        public async Task Post_Course_Planner_Success()
        {
            // Arrange
            var fakeStudentId = "1";
            var fakeStudentCoursePlanner = GetFakeStudentPlanner(fakeStudentId);

            _coursePlannerRepositoryMock.Setup(x => x.UpdateCoursePlannerAsync(It.IsAny<API.Models.CoursePlanner>()))
                .Returns(Task.FromResult(fakeStudentCoursePlanner));

            _identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeStudentId);

            _serviceBusMock.Setup(x => x.Publish(It.IsAny<StudentCoursePlannerCheckoutAcceptedIntegrationEvent>()));

            // Act
            var coursePlannerController = new BasketController(
                _loggerMock.Object,
                _coursePlannerRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object
                );

            var actionResult = await coursePlannerController.UpdateCoursePlannerAsync(fakeStudentCoursePlanner);

            // Assert
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal((((ObjectResult)actionResult.Result).Value as API.Models.CoursePlanner).StudentId, fakeStudentId);
        }


        [Fact]
        public async Task Doing_Checkout_Without_Course_Planner_Should_Return_Bad_Request()
        {
            // Arrange
            var fakeCustomerId = "2";
            _coursePlannerRepositoryMock.Setup(x => x.GetCoursePlannerAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((API.Models.CoursePlanner)null));
            _identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeCustomerId);

            //Act
            var basketController = new BasketController(
                _loggerMock.Object,
                _coursePlannerRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object);

            // Assert
            var result = await basketController.CheckoutAsync(new BasketCheckout(), Guid.NewGuid().ToString()) as BadRequestResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Doing_Checkout_With_CoursePlanner_Should_Publish_StudentCoursePlannerCheckoutAccepted_Integration_Event()
        {
            var fakeCustomerId = "1";
            var fakeCustomerBasket = GetFakeStudentPlanner(fakeCustomerId);

            _coursePlannerRepositoryMock.Setup(x => x.GetCoursePlannerAsync(It.IsAny<string>()))
                 .Returns(Task.FromResult(fakeCustomerBasket));

            _identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeCustomerId);

            var basketController = new BasketController(
                _loggerMock.Object,
                _coursePlannerRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object);

            basketController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(new Claim[] { new Claim("unique_name", "testuser") }))
                }
            };

            //Act
            var result = await basketController.CheckoutAsync(new BasketCheckout(), Guid.NewGuid().ToString()) as AcceptedResult;

            _serviceBusMock.Verify(mock => mock.Publish(It.IsAny<StudentCoursePlannerCheckoutAcceptedIntegrationEvent>()), Times.Once);

            Assert.NotNull(result);
        }





        private API.Models.CoursePlanner GetFakeStudentPlanner(string fakeStudentId) =>
            new API.Models.CoursePlanner(fakeStudentId)
            {
                Courses = new List<Course>
                {
                    new Course()
                }
            };
        
    }
}
