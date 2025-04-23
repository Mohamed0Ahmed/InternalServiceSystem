using Microsoft.AspNetCore.SignalR;

namespace MvcProject
{
    public class NotificationHub : Hub
    {
        public async Task SendOrderNotification(int orderId)
        {
            await Clients.Group("Owners").SendAsync("ReceiveOrderNotification", orderId);
        }

        public async Task SendHelpRequestNotification(int helpRequestId)
        {
            await Clients.Group("Owners").SendAsync("ReceiveHelpRequestNotification", helpRequestId);
        }

        public Task JoinOwnersGroup()
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, "Owners");
        }
    }
}