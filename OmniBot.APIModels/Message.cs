using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.APIModels
{
    public class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public List<Transition> Transitions { get; set; }
        public List<Transition> AddTransistion(Transition transition)
        {
            if(this.Transitions == null)
            {
                this.Transitions = new List<Transition>();
            }
            this.Transitions.Add(transition);
            return this.Transitions;
        }

        public Message(DataClasses.Message message)
        {
            Id = message.Id;
            Name = message.Name;
            Body = message.Body;
            IsStart = message.IsStart;
            IsEnd = message.IsEnd;
            
            if(message.Transitions != null)
            {
                foreach (DataClasses.Transition transition in message.Transitions)
                {
                    this.AddTransistion(new Transition(transition));
                }
            }
        }

    }
}
