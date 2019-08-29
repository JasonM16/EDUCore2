using System.Collections.Generic;

namespace Web.Shopping.HttpAggregator.Models
{
    public class UpdateBasketItemsRequest
    {
        public string CoursePlannerId { get; set; }
        public ICollection<UpdateBasketItemData> Updates { get; set; }

        public UpdateBasketItemsRequest()
        {
            Updates = new List<UpdateBasketItemData>();
        }
    }

    public class UpdateBasketItemData
    {
        public string CourseId { get; set; }
    }
}
