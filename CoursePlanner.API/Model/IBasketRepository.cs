using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoursePlanner.API.Model
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetCoursePlannerAsync(string customerId);
        IEnumerable<string> GetStudents();
        Task<CustomerBasket> UpdateCoursePlannerAsync(CustomerBasket basket);
        Task<bool> DeleteCoursePlannerAsync(string id); //TODO clear courses from planner or delete???
    }
}

