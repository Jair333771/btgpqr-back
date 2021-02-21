using AutoMapper;
using btg_pqr_back.Common.Exceptions;
using btg_pqr_back.Common.Interfaces.Globals;
using btg_pqr_back.Core.Commands;
using btg_pqr_back.Core.Entities;
using btg_pqr_back.Core.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace btg_pqr_back.Core.Querys
{
    public class GetAllPqrsQuery : IRequest<IGlobalResponse<IEnumerable<GetAllPqrsQuery>>> {
        public int Id { get; set; }
        public string MessageUser { get; set; }

        public string ResponseAdmin { get; set; }

        public DateTime DateRequest { get; set; }

        public DateTime? DateResponse { get; set; }

        public string UserName { get; set; }

        public int Type { get; set; }

        public int? PqrId { get; set; }

        public bool Active { get; set; }

        public IEnumerable<GetAllPqrsQuery> ClaimList { get; set; }
    }

    public partial class GetAllPqrsQueryHandler : IRequestHandler<GetAllPqrsQuery, IGlobalResponse<IEnumerable<GetAllPqrsQuery>>>
    {
        private readonly IGlobalResponse<IEnumerable<GetAllPqrsQuery>> globalResponse;
        private readonly IPqrRepository<PqrEntity> pqrRepository;
        private readonly IClaimRepository<ClaimEntity> claimRepository;
        private readonly IMapper mapper;

        public GetAllPqrsQueryHandler(IGlobalResponse<IEnumerable<GetAllPqrsQuery>> _globalResponse, IPqrRepository<PqrEntity> _repository, IMapper _mapper, IClaimRepository<ClaimEntity> _claimRepository)
        {
            globalResponse = _globalResponse;
            pqrRepository = _repository;
            mapper = _mapper;
            claimRepository = _claimRepository;
        }

        public async Task<IGlobalResponse<IEnumerable<GetAllPqrsQuery>>> Handle(GetAllPqrsQuery request, CancellationToken cancellationToken)
        {
            var list = pqrRepository.GetAll();

            ValidateSave(list);

            var listModel = mapper.Map<IEnumerable<GetAllPqrsQuery>>(list).ToList();
            var claims = claimRepository.GetAll();

            listModel.ForEach(item => {
                var claim = claims.FirstOrDefault(x => x.PqrId == item.Id);
                item.ClaimList = mapper.Map<IEnumerable<GetAllPqrsQuery>>(listModel.Where(X => X.Id == claim?.ClaimId));
            });

            globalResponse.Data = listModel;
            globalResponse.Message = $"The request was Successfully.";
            globalResponse.StatusCode = 200;
            return await Task.FromResult(globalResponse);
        }
    }

    public partial class GetAllPqrsQueryHandler
    {
        public Action<IEnumerable<PqrEntity>> ValidateSave = (list) =>
        {
            if (!list.Any())
                throw new PqrException(204, $"The request not producess any result.");
        };
    }
}
