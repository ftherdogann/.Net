using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalR.UseInMvcProject.Hubs
{
    public class MyHub : Hub
    {
        public async Task SendMessage(string name)
        {
            await Clients.All.SendAsync("ReceiveMessage", name);
        }
    }
}
