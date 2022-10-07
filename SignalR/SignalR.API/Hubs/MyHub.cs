using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.API.Hubs
{
    public class MyHub : Hub
    {
        private readonly AppDbContext _context;
        public MyHub(AppDbContext context)
        {
            _context = context;
        }
        private static List<string> Names = new List<string>();
        public static int ClientCount { get; set; } = 0;
        public static int TeamCount { get; set; } = 7;

        //complex type kullanımı için method
        public async Task SendProduct(Product product)
        {
            await Clients.All.SendAsync("ReceiveProduct", product);
        }
        public async Task SendName(string name)
        {
            if (Names.Count >= TeamCount)
            {
                await Clients.Caller.SendAsync("Error", $"Takım en fazla {TeamCount} kişi olabilir.");
            }
            else
            {
                Names.Add(name);
                await Clients.All.SendAsync("ReceiveName", name);
            }

        }

        public async Task GetNames()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }

        public async Task AddToGroup(string teamName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, teamName);

        }
        public async Task RemoveToGroup(string teamName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, teamName);

        }
        public async Task SendNameByGroup(string userName, string teamName)
        {
            var team = _context.Teams.Where(x => x.Name == teamName).FirstOrDefault();
            if (team != null)
            {
                team.Users.Add(new User { Name = userName });
            }
            else
            {
                var newTeam = new Team { Name = teamName };

                newTeam.Users.Add(new User { Name = userName });

                _context.Teams.Add(newTeam);
            }
            await _context.SaveChangesAsync();

            await Clients.Groups(teamName).SendAsync("ReceiveMessageByGroup", userName, team.Id);

        }
        public async Task GetNamesByGroup()
        {
            var teams = _context.Teams.Include(x => x.Users).Select(x => new
            {
                teamId = x.Id,
                Users = x.Users.ToList()
            });

            await Clients.All.SendAsync("ReceiveNamesByGroup", teams);

        }
     
        public async override Task OnConnectedAsync()
        {
            ClientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            ClientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
