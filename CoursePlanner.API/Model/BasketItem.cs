using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoursePlanner.API.Model
{
    public class BasketItem //: IValidatableObject
    {
        public string Id { get; set; }
        public string CourseName { get; set; }
        //public int NumberOfCourses { get; set; }


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var results = new List<ValidationResult>();

        //    if (NumberOfCourses < 1)
        //    {
        //        results.Add(new ValidationResult("Invalid number of courses", new[] { "NumberOfCourses" }));
        //    }

        //    return results;
        //}
    }
}
