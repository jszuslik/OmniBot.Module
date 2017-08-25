using ApiAiSDK;
using ApiAiSDK.Model;
using OmniBot.APIModels;
using OmniBot.DataClasses;
using OmniBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.Services
{
    public class GoogleApiAiService : IAiService
    {
        private ApiAi apiAi;

        public GoogleApiAiService()
        {
            var config = new AIConfiguration("e86c104c08a44418937c258e6b30c3ae", SupportedLanguage.English);
            apiAi = new ApiAi(config);
        }

        public AiResponseExtract ProcessUserRequest(Conversation conversation, UserRequest userRequest)
        {
            AiResponseExtract response = new AiResponseExtract();

            AIRequest aiRequest = new AIRequest(userRequest.Body);
            AIContext aiContext = new AIContext()
            {
                Name = conversation.CurrentMessage.Name
            };

            aiRequest.Contexts = new List<AIContext>()
            {
                aiContext
            };

            AIResponse aiResponse = apiAi.TextRequest(aiRequest);

            response.Intent = aiResponse.Result.Action;

            if (aiResponse.Result.Action.Contains("smalltalk"))
            {
                response.SmallTalk = true;
            }

            response.Speech = aiResponse.Result.Fulfillment.Speech;

            return response;
        }
    }
}
