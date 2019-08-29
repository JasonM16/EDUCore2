using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;
using WebMVC.Services.ModelDTOs;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public interface ICoursePlannerService
    {
        Task<CoursePlanner> GetCoursePlannerAsync(Student student);
        Task AddCourseToPlanner(Student student, string courseId);
        Task<CoursePlanner> UpdateCoursePlanner(CoursePlanner planner);
        Task Checkout(CoursePlannerDTO planner);
        //Task<Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities);
        Task<CourseEnrollment> GetCourseEnrollmentDraft(string plannerId);
    }
}
