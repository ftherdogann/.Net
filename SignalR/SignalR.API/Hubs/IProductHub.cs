using SignalR.API.Models;
using System.Threading.Tasks;

namespace SignalR.API.Hubs
{
    public interface IProductHub
    {
        Task ReceiveProduct(Product p);
    }
}
