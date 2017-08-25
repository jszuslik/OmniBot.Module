using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.APIModels.Enums
{
    public enum StatusType
    {
        STARTED,
        NOT_STARTED,
        IN_PROGRESS,
        COMPLETED_SUCCESSFUL,
        COMPLETED_FAILED,
        FILTERED_OUT,
        TIMED_OUT,
        OPTED_OUT
    }
}
