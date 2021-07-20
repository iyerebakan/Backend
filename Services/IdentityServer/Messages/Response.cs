using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Messages
{
    public class Response
    {
        public Response(bool success,string message)
        {
            this.Success = success;
            this.Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ErrorResponse : Response
    {
        public ErrorResponse(string message) : base(false, message)
        {
        }
    }

    public class SuccessResponse : Response
    {
        public SuccessResponse() : base(true, null)
        {
        }
    }
}
