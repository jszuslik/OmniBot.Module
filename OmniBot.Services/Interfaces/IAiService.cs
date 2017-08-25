using OmniBot.APIModels;
using OmniBot.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.Services.Interfaces
{
    public interface IAiService
    {
        AiResponseExtract ProcessUserRequest(Conversation conversation, UserRequest userRequest);
    }
}
