using ImageService.Infastructure.Event;
using ImageService.Infrastructure.Event;
using System;
using System.Net.Sockets;

namespace Communication
{
    public interface IClientHandler
    {
        event EventHandler<ClientCommandEventArgs> ClientCommandRecieved;

        void HandleClient(TcpClient client);
        void OnLogChange(object sender, LogChangedEventArgs e);
        void CommandDone(object sender, CommandDoneEventArgs e);
        void StopHandler();
    }
}
