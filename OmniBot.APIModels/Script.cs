using OmniBot.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.APIModels
{
    public class Script
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; }
        public List<Message> AddMessage(Message message)
        {
            if(this.Messages != null)
            {
                this.Messages = new List<Message>();
            }
            this.Messages.Add(message);
            return this.Messages;
        }

        public Script(DataClasses.Script script)
        {
            if (script == null)
            {
                throw new NullReferenceException("script");
            }

            Id = script.Id;
            Name = script.Name;
            
            if (script.Messages != null)
            {
                foreach (DataClasses.Message message in script.Messages)
                {
                    this.AddMessage(new Message(message));
                }
            }
        }
    }
}
