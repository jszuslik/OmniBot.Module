using OmniBot.APIModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniBot.APIModels
{
    public class UserRequest
    {
        public UserRequestType UserRequestType { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Body { get; set; }

        public UserRequest(UserRequestType UserRequestType, string PhoneNumber, string Body)
        {
            this.UserRequestType = UserRequestType;
            this.PhoneNumber = PhoneNumber;
            this.Body = Body;
        }
    }
}
