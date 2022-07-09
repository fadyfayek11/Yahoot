using Microsoft.AspNetCore.SignalR;

namespace Yahoot.Hubs;

public class YahootHub : Hub
{
    public async Task SendJoinMessageToAdmin(string message)
    {
        await Clients.All.SendAsync("SendJoinMessageToAdmin", message);
    }
    //public Task AdminSend(int questionId)
    //{
    //    await Clients.All.SendAsync("SendJoinMessageToAdmin", message);
    //}
    
}