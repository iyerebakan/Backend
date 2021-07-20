using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Result
{
    public class Response<T>
    {
        public Response()
        {
            this.Header = new ResponseHeader();
            this.Body = default(T);
        }

        public Response(T body, bool isSuccess)
        {
            this.Body = body;
            this.Header = new ResponseHeader { IsSuccess = isSuccess };
        }

        public ResponseHeader Header { get; set; }

        public T Body { get; set; }

        // ReSharper disable once CA1000
#pragma warning disable CA1000 // Do not declare static members on generic types
        public static Response<TBody> Success<TBody>(TBody body) => new Response<TBody>(body, true);
#pragma warning restore CA1000 // Do not declare static members on generic types

        // ReSharper disable once CA1000
#pragma warning disable CA1000 // Do not declare static members on generic types
        public static Response<TBody> Fail<TBody>(TBody body) => new Response<TBody>(body, false);
#pragma warning restore CA1000 // Do not declare static members on generic types
    }
}
