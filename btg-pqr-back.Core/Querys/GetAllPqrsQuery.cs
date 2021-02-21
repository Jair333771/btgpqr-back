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
    public class GetAllPqrsQuery : IRequest<IGlobalResponse<IEnumerable<CreatePqrCommand>>> { }

    public partial class GetAllPqrsQueryHandler : IRequestHandler<GetAllPqrsQuery, IGlobalResponse<IEnumerable<CreatePqrCommand>>>
    {
        private readonly IGlobalResponse<IEnumerable<CreatePqrCommand>> globalResponse;
        private readonly IRepository<PqrEntity> repository;
        private readonly IMapper mapper;

        public GetAllPqrsQueryHandler(IGlobalResponse<IEnumerable<CreatePqrCommand>> _globalResponse, IRepository<PqrEntity> _repository, IMapper _mapper)
        {
            globalResponse = _globalResponse;
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<IGlobalResponse<IEnumerable<CreatePqrCommand>>> Handle(GetAllPqrsQuery request, CancellationToken cancellationToken)
        {
            var list = repository.GetAll();

            ValidateSave(list);

            var listModel = mapper.Map<IEnumerable<CreatePqrCommand>>(list);
            globalResponse.Data = listModel;
            globalResponse.Message = $"The request was Successfully.";
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
