using btg_pqr_back.Common.Interfaces.Globals;
using btg_pqr_back.Core.Commands;
using btg_pqr_back.Core.Querys;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace btg_pqr_back.Api.Controllers
{
    [DataContract]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PqrController : ControllerBase
    {
        public readonly IMediator mediatr;
        public PqrController(IMediator _mediatr)
        {
            mediatr = _mediatr;
        }

        [HttpGet]
        public async Task<IGlobalResponse<IEnumerable<CreatePqrCommand>>> Get()
        {
            return await mediatr.Send(new GetAllPqrsQuery());
        }

        [HttpPost]
        public async Task<IGlobalResponse<CreatePqrCommand>> Post([FromBody] CreatePqrCommand command)
        {
            return await mediatr.Send(command);
        }
    }
}
