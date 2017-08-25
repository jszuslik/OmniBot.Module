using OmniBot.API.App_Start;
using System.Web.Http;
using System.Web.Routing;

namespace OmniBot.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            OmniBotUnityConfig.Configure();
        }
    }
}
