using System.Collections.Generic;

namespace Web.Shopping.HttpAggregator.Models
{
    public class UpdateBasketRequest
    {
        public string StudentId { get; set; }

        public IEnumerable<UpdateBasketRequestItemData> Items { get; set; }
    }

    public class UpdateBasketRequestItemData
    {
        public string Id { get; set; }          // Basket id
        public int ProductId { get; set; }      // Catalog item id
        
    }
}
