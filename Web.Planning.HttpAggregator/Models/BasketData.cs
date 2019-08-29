using System.Collections.Generic;

namespace Web.Shopping.HttpAggregator.Models
{
    public class BasketData
    {
        public string StudentId { get; set; }
        public List<BasketDataItem> Items { get; set; }

        public BasketData(string studentId)
        {
            StudentId = studentId;
            Items = new List<BasketDataItem>();
        }
    }

    public class BasketDataItem
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
    }
}
