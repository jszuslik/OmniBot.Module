using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.Services.Interfaces
{
    public interface IMessagingService
    {
        void SendMessage(string user, string message);
    }
}
