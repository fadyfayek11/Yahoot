using Microsoft.AspNetCore.SignalR;

namespace Yahoot.Hubs;

public class YahootHub : Hub
{
    public Task SendJoinMessageToAdmin(string message)
    {
        return Clients.All.SendAsync("SendJoinMessageToAdmin", message);
    }
    
}