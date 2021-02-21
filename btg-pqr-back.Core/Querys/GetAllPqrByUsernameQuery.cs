using AutoMapper;
using btg_pqr_back.Common.Interfaces.Globals;
using btg_pqr_back.Core.Entities;
using btg_pqr_back.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace btg_pqr_back.Core.Querys
{
    public class GetAllPqrByUsernameQuery : IRequest<IGlobalResponse<IEnumerable<GetAllPqrByUsernameQuery>>>
    {
        public int Id { get; set; }
        public string MessageUser { get; set; }

        public string ResponseAdmin { get; set; }

        public DateTime DateRequest { get; set; }

        public DateTime? DateResponse { get; set; }

        public string UserName { get; set; }

        public int Type { get; set; }

        public int? PqrId { get; set; }

        public bool Active { get; set; }
    }

    public partial class GetAllPqrByUsernameQueryHandler : IRequestHandler<GetAllPqrByUsernameQuery, IGlobalResponse<IEnumerable<GetAllPqrByUsernameQuery>>>
    {
        private readonly IGlobalResponse<IEnumerable<GetAllPqrByUsernameQuery>> globalResponse;
        private readonly IPqrRepository<PqrEntity> pqrRepository;
        private readonly IMapper mapper;

        public GetAllPqrByUsernameQueryHandler(IGlobalResponse<IEnumerable<GetAllPqrByUsernameQuery>> _globalResponse, IPqrRepository<PqrEntity> _pqrRepository, IMapper _mapper)
        {
            globalResponse = _globalResponse;
            pqrRepository = _pqrRepository;
            mapper = _mapper;
        }

        public async Task<IGlobalResponse<IEnumerable<GetAllPqrByUsernameQuery>>> Handle(GetAllPqrByUsernameQuery request, CancellationToken cancellationToken)
        {
            var pccsByUser = pqrRepository.GetAllPetitionsAndComplaintByUser(request.UserName);
            var listModel = mapper.Map<IEnumerable<GetAllPqrByUsernameQuery>>(pccsByUser);
            globalResponse.Data = listModel;
            globalResponse.Message = $"The request was Successfully.";
            globalResponse.StatusCode = 200;
            return await Task.FromResult(globalResponse);
        }
    }
}
