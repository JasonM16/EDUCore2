namespace WebMVC.Infrastructure
{
    public class API
    {
        public static class CourseEnrollment //TODO What else to call this class besides Purchase??
        {
            public static string AddCourseToPlanner(string baseUri) => $"{baseUri}/planner/courses";
            public static string UpdateCourse(string baseUri) => $"{baseUri}/planner/courses";
            public static string GetCourseEnrollmentDraft(string baseUri, string plannerId) => $"{baseUri}/order/draft/{plannerId}";
        }


        public static class CoursePlanner
        {
            public static string GetCoursePlanner(string baseUri, string plannerId) => $"{baseUri}/{plannerId}";
            public static string UpdateCoursePlanner(string baseUri) => baseUri;
            public static string CheckoutCoursePlanner(string baseUri) => $"{baseUri}/checkout";
            public static string CleanCoursePlanner(string baseUri, string plannerId) => $"{baseUri}/{plannerId}";
        }


        public static class Enrollment // Registration or Enrollments
        {
            public static string GetEnrollment(string baseUri, string enrollmentId)
            {
                return $"{baseUri}/{enrollmentId}";
            }

            public static string GetAllMyEnrollments(string baseUri)
            {
                return baseUri;
            }

            public static string AddNewEnrollment(string baseUri)
            {
                return $"{baseUri}/new";
            }

            public static string CancelEnrollment(string baseUri)
            {
                return $"{baseUri}/cancel";
            }

            public static string RegisterEnrollments(string baseUri)
            {
                return $"{baseUri}/register";
            }
        }

        public static class Catalog
        {
            public static string GetAllCatalogItems(string baseUri, int page, int take, int? brand, int? type)
            {
                var filterQs = "";

                if (type.HasValue)
                {
                    var brandQs = (brand.HasValue) ? brand.Value.ToString() : string.Empty;
                    filterQs = $"/type/{type.Value}/brand/{brandQs}";

                }
                else if (brand.HasValue)
                {
                    var brandQs = (brand.HasValue) ? brand.Value.ToString() : string.Empty;
                    filterQs = $"/type/all/brand/{brandQs}";
                }
                else
                {
                    filterQs = string.Empty;
                }

                return $"{baseUri}items{filterQs}?pageIndex={page}&pageSize={take}";
            }

            public static string GetAllBrands(string baseUri)
            {
                return $"{baseUri}catalogBrands";
            }

            public static string GetAllTypes(string baseUri)
            {
                return $"{baseUri}catalogTypes";
            }
        }
    }
}
