using Microsoft.Practices.Unity;
using OmniBot.APIModels;
using OmniBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OmniBot.API.Controllers
{
    public class ConversationController : ApiController
    {
        [Dependency]
        public IOmniBotService OmniBotService { private get; set; }

        [HttpPost]
        [Route("api/start_conversation")]
        public string StartConversation([FromBody] ConversationStarter conversationStarter)
        {
            System.Diagnostics.Trace.WriteLine(conversationStarter.PhoneNumber);
            System.Diagnostics.Trace.WriteLine("Json: " + conversationStarter);

            // OmniBotService.StartConversation(conversationStarter);
            System.Diagnostics.Trace.WriteLine("Starting Conversation");

            return "??";
        }

        [HttpGet]
        [Route("api/test")]
        public string TestStartup()
        {
            System.Diagnostics.Trace.WriteLine("Starting Conversation");

            return "??";
        }
    }
}