using OmniBot.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.Services.Interfaces
{
    public interface IIntentService
    {
        Intent GetIntentById(int id);

        Intent GetMisunderstoodIntent();

        Intent GetAcceptAnyIntent();

        Intent GetMoveOnIntent();

        List<Intent> GetAllIntents();
    }
}
