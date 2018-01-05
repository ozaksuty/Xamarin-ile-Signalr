using Microsoft.AspNet.SignalR.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinSignalr
{
    public class SignalrClient
    {
        string url = "http://192.168.0.18/AAkademiSignalR2";
        HubConnection Connection;
        IHubProxy ChatHubProxy;

        public delegate void Error();
        public delegate void MessageReceived(SignalrUser user);

        public event Error ConnectionError;
        public event MessageReceived OnMessageReceived;

        public void Connect(string _username)
        {
            Connection = new HubConnection(url, new Dictionary
                <string, string>
            {
                { "username", _username }
            });

            Connection.StateChanged += Connection_StateChanged;

            ChatHubProxy = Connection.CreateHubProxy("ChatHub");

            ChatHubProxy.On<string, string>("MessageReceived",
                (username, message) =>
            {
                var user = new SignalrUser
                {
                    username = username,
                    message = message
                };
                OnMessageReceived?.Invoke(user);
            });

            Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                    ConnectionError?.Invoke();
            });
        }

        public void SendMessage(string username, string message)
        {
            ChatHubProxy.Invoke("SendMessage", username, message);
        }

        private Task Start()
        {
            return Connection.Start();
        }

        private void Connection_StateChanged(StateChange obj)
        {

        }
    }
}