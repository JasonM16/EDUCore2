namespace Web.Shopping.HttpAggregator.Models
{
    public class AddBasketItemRequest
    {
        public int CatalogItemId { get; set; }
        public string BasketId { get; set; }
    }
}
