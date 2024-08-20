using Microsoft.AspNetCore.SignalR;

namespace FundevServer.Helpers
{
    public class OrderHub : Hub
    {
        public async Task NotifyNewOrder(string userId)
        {
            await Clients.User(userId).SendAsync("RefreshOrders");
        }
    }
}
