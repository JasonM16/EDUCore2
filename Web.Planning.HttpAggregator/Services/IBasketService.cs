using System.Threading.Tasks;
using Web.Shopping.HttpAggregator.Models;

namespace Web.Shopping.HttpAggregator.Services
{
    public interface IBasketService
    {
        Task<BasketData> GetByIdAsync(string id);

        Task UpdateAsync(BasketData currentCoursePlanner);
    }
}
