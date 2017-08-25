using OmniBot.APIModels.Enums;
using OmniBot.DataClasses;
using OmniBot.DataModel;
using OmniBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.Services
{
    public class StatusService : IStatusService
    {
        public static StatusService statusService;

        public Dictionary<string, Status> conversationStatuses;

        private StatusService()
        {
            if (conversationStatuses == null)
            {
                conversationStatuses = new Dictionary<string, Status>();
                PopulateStatuses();
            }
        }

        public static StatusService getInstance()
        {
            if (statusService == null)
            {
                statusService = new StatusService();
            }

            return statusService;
        }

        public void PopulateStatuses()
        {
            using (var context = new OmniBotContext())
            {
                List<Status> statuses = context.Statuses.ToList();

                foreach (Status status in statuses)
                {
                    conversationStatuses.Add(status.Name, status);
                }

            }
        }
    }
}
