using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Models;
using WebMVC.Services.ModelDTOs;
using WebMVC.ViewModels;

namespace WebMVC.Services
{
    public class CoursePlannerService : ICoursePlannerService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;
        private readonly string _coursePlannerByPassUrl;
        private readonly string _enrollmentUrl;

        public CoursePlannerService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _apiClient = httpClient;
            _settings = settings;

            _coursePlannerByPassUrl = $"{_settings.Value.CourseEnrollmentUrl}/api/v1/b/coursePlanner";
            //_purchaseUrl = $"{_settings.Value.PurchaseUrl}/api/v1";
        }

        public async Task<CoursePlanner> GetCoursePlannerAsync(Student student)
        {
            var uri = API.CoursePlanner.GetCoursePlanner(_coursePlannerByPassUrl, student.Id);

            var responseString = await _apiClient.GetStringAsync(uri);

            return string.IsNullOrEmpty(responseString) ?
                new CoursePlanner { StudentId = student.Id } :
                JsonConvert.DeserializeObject<CoursePlanner>(responseString);
        }


        public async Task<CoursePlanner> UpdateCoursePlanner(CoursePlanner planner)
        {
            var uri = API.CoursePlanner.UpdateCoursePlanner(_coursePlannerByPassUrl);

            var plannerContent = new StringContent(JsonConvert.SerializeObject(planner), Encoding.UTF8, "application/json");

            var response = await _apiClient.PostAsync(uri, plannerContent);

            response.EnsureSuccessStatusCode();

            return planner;
        }


        public async Task Checkout(CoursePlannerDTO planner)
        {
            var uri = API.CoursePlanner.CheckoutCoursePlanner(_coursePlannerByPassUrl);
            var plannerContent = new StringContent(JsonConvert.SerializeObject(planner), Encoding.UTF8, "application/json");

            var response = await _apiClient.PostAsync(uri, plannerContent);

            response.EnsureSuccessStatusCode();
        }


        public async Task AddCourseToPlanner(Student student, string courseId)
        {
            var uri = API.CourseEnrollment.GetCourseEnrollmentDraft(_enrollmentUrl, courseId);

            var newCourse = new
            {
                CourseId = courseId,
                CoursePlannerId = student.Id
            };

            var plannerContent = new StringContent(JsonConvert.SerializeObject(newCourse), Encoding.UTF8, "application/json");

            var response = await _apiClient.PostAsync(uri, plannerContent);
        }

      

        

        public async Task<CourseEnrollment> GetCourseEnrollmentDraft(string plannerId)
        {
            var uri = API.CourseEnrollment.GetCourseEnrollmentDraft(_enrollmentUrl, plannerId);

            var responseString = await _apiClient.GetStringAsync(uri);

            var response = JsonConvert.DeserializeObject<CourseEnrollment>(responseString);

            return response;
        }

        
    }
}
