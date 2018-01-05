using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace AAkademiSignalR2
{
    public class ChatHub : Hub
    {
        public void SendMessage(string username, string message)
        {
            Clients.All.MessageReceived(username, message);
        }

        public override Task OnConnected()
        {
            Clients.All.MessageReceived(Context.QueryString["username"]
                , Context.ConnectionId + " Connected!");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Clients.All.MessageReceived(Context.QueryString["username"]
                , Context.ConnectionId + " Disconnected!");
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            Clients.All.MessageReceived(Context.QueryString["username"]
                , Context.ConnectionId + " Reconnected!");
            return base.OnReconnected();
        }
    }
}