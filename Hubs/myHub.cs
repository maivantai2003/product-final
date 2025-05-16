using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace QuanLySanPhamBasic.Hubs
{
    public class myHub:Hub
    {
        public override async Task OnConnectedAsync()
        {
           /* var userId = Context.UserIdentifier;
            var isAdmin = Context.User?.IsInRole("Admin");
            if (isAdmin == false)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }*/
            await base.OnConnectedAsync();
        }
        public async Task SendMessage(string id,string user, string mess)
        {
            //var senderId = Context.UserIdentifier;
            //var message = new Message()
            //{
            //    ReceiverId = id,
            //    SenderId = senderId,
            //    Content = mess,
            //    Timestamp = DateTime.UtcNow
            //};
            //await _context.Messages.AddAsync(message);
            //await _context.SaveChangesAsync();
            await Clients.User(id).SendAsync("ReceiveMessage",user, mess);
        }
    }
}
