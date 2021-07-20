using Core.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Core.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class ApiControllerBase : ControllerBase
    {
        protected Response<TBody> ProduceResponse<TBody>(TBody body)
        {
            // TODO: Header values
            return Response<TBody>.Success(body);
        }
    }
}
