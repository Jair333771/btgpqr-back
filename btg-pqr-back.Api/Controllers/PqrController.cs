using btg_pqr_back.Common.Globals;
using btg_pqr_back.Common.Interfaces.Globals;
using btg_pqr_back.Core.Commands;
using btg_pqr_back.Core.Querys;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IGlobalResponse<IEnumerable<GetAllPqrsQuery>>))]
        public async Task<IGlobalResponse<IEnumerable<GetAllPqrsQuery>>> GetAll()
        {
            return await mediatr.Send(new GetAllPqrsQuery());
        }

        [HttpGet("username")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IGlobalResponse<IEnumerable<GetAllPqrByUsernameQuery>>))]
        public async Task<IGlobalResponse<IEnumerable<GetAllPqrByUsernameQuery>>> GetByUsername(string username)
        {
            return await mediatr.Send(new GetAllPqrByUsernameQuery { UserName = username });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IGlobalResponse<CreatePqrCommand>))]
        public async Task<IGlobalResponse<CreatePqrCommand>> Post([FromBody] CreatePqrCommand command)
        {
            return await mediatr.Send(command);
        }
    }
}
