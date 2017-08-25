using Microsoft.Practices.Unity;
using OmniBot.APIModels;
using OmniBot.APIModels.Enums;
using OmniBot.Services.Interfaces;
using System.Web.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;

namespace OmniBot.API.Controllers
{
    public class OmniBotController : TwilioController
    {
        [Dependency]
        public IOmniBotService OmniBotService { private get; set; }

        [HttpPost]
        public ActionResult ProcessConversation(SmsRequest smsRequest)
        {
            System.Diagnostics.Debug.WriteLine(smsRequest.Body);

            UserRequest UserRequest = new UserRequest(UserRequestType.TEXT, smsRequest.From, smsRequest.Body);

            var twiML = new MessagingResponse();
            var message = twiML.Message(OmniBotService.MaintainConversation(UserRequest));

            return TwiML(message);
        }

    }
}