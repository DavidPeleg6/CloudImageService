using ImageService.Modal.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);
        void OnLogChange(object sender, LogChangedEventArgs e);
    }
}
