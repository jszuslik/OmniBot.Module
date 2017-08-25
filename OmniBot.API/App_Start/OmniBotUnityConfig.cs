using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using OmniBot.API.Unity;
using OmniBot.DataModel;
using OmniBot.Services;
using OmniBot.Services.Interfaces;
using System.Web.Http;
using System.Web.Mvc;

namespace OmniBot.API.App_Start
{
    public class OmniBotUnityConfig
    {
        public static void Configure()
        {
            var Container = new UnityContainer();

            Container.RegisterType<OmniBotContext, OmniBotContext>(new HierarchicalLifetimeManager());
            Container.RegisterType<IOmniBotService, OmniBotService>(new HierarchicalLifetimeManager());

            Container.RegisterType<IAiService, GoogleApiAiService>(new HierarchicalLifetimeManager());
            Container.RegisterType<ICommunicationService, CommunicationService>(new HierarchicalLifetimeManager());
            Container.RegisterType<IConversationService, ConversationService>(new HierarchicalLifetimeManager());
            Container.RegisterType<IIntentService, IntentService>(new HierarchicalLifetimeManager());
            Container.RegisterType<IMessageService, MessageService>(new HierarchicalLifetimeManager());
            Container.RegisterType<IPeopleService, PeopleService>(new HierarchicalLifetimeManager());
            Container.RegisterType<IScriptService, ScriptService>(new HierarchicalLifetimeManager());
            Container.RegisterType<IStatusService, StatusService>(new HierarchicalLifetimeManager());

            ApiUnityResolver Resolver = new ApiUnityResolver(Container);
            GlobalConfiguration.Configuration.DependencyResolver = Resolver;
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }
    }
}