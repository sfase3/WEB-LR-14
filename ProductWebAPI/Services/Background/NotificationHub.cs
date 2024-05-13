using Microsoft.AspNetCore.SignalR;

namespace ProductWebAPI.Services.Background
{

    public class NotificationHub : Hub
    {
        // Метод для прийому повідомлень від фонової служби
        public Task ReceiveMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
