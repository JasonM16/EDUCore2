using BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace Catalog.API.IntegratingEvents
{
    public interface ICatalogIntegrationEventService
    {
        Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent @event);
        Task PublishThroughEventBusAsync(IntegrationEvent @event);
    }
}
