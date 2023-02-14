using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs;

public class UserHub : Hub
{
    public static int TotalViews { get; set; } = 0;

    public static int ConnectedUsers { get; set; } = 0;

    public override Task OnConnectedAsync()
    {
        ConnectedUsers++;
        // GetAwaiter instead of await keyword since method is not async
        Clients.All.SendAsync("updateConnectedUsers", ConnectedUsers).GetAwaiter().GetResult();
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        ConnectedUsers--;
        // GetAwaiter instead of await keyword since method is not async
        Clients.All.SendAsync("updateConnectedUsers", ConnectedUsers).GetAwaiter().GetResult();
        return base.OnDisconnectedAsync(exception); 
    }

    public async Task NewWindowLoaded()
    {
        TotalViews++;
        // Send update to all clients that total views have been updated.
        await Clients.All.SendAsync("updateTotalViews", TotalViews);
    }
}
