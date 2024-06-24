using IMDB_Api.Models;
using Microsoft.AspNetCore.SignalR;

namespace IMDB_Api.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("notifyNewMessage", message);
        }
    }
}
