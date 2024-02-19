using CSharpBlazorCICDSkeleton.Frontend.Hubs.Interface;
using Microsoft.AspNetCore.SignalR;

namespace CSharpBlazorCICDSkeleton.Frontend.Hubs;

public class ChatHub : Hub, IClientContract
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
