using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Commands
{
    public class CreateUserEmailMessageCommand
    {
        public CreateUserEmailMessageCommand(string email,string token,string userid)
        {
            this.Timestamp = DateTime.Now;
            this.UserId = userid;
            this.Token = token;
            this.Email = email;
        }
        public DateTime Timestamp { get; private set; }
        public string Email { get; private set; }
        public string Token { get; private set; }
        public string UserId { get; private set; }
    }
}
