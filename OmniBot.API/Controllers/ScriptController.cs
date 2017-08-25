using Microsoft.Practices.Unity;
using OmniBot.DataClasses;
using OmniBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace OmniBot.API.Controllers
{
    public class ScriptController : ApiController
    {
        [Dependency]
        public IScriptService ScriptService { private get; set; }

        [HttpPost]
        [Route("api/script")]
        public APIModels.Script UploadScript([FromBody] Script script)
        {
            System.Diagnostics.Trace.WriteLine(script.Name);

            script = ScriptService.FinalizeAndSaveScript(script);

            return new APIModels.Script(script);
        }
    }
}