using System.Collections.Generic;

namespace CoursePlanner.API.Model
{
    public class CustomerBasket
    {
        public string StudentId { get; set; }
        public List<BasketItem> Courses { get; set; }

        public CustomerBasket(string studentId)
        {
            StudentId = studentId;
            Courses = new List<BasketItem>();
        }
    }
}
