using Microsoft.AspNetCore.SignalR;
using SignalR.API.Models;
using System.Threading.Tasks;

namespace SignalR.API.Hubs
{
    public class ProductHub:Hub<IProductHub>
    { 
        // strongly type örnek
        public async Task SendProduct(Product p)
        {
            await Clients.All.ReceiveProduct(p);
        }
    }
}
