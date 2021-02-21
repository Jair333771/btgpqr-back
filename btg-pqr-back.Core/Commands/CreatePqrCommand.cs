using AutoMapper;
using btg_pqr_back.Common.Exceptions;
using btg_pqr_back.Common.Interfaces.Globals;
using btg_pqr_back.Core.Entities;
using btg_pqr_back.Core.Enums;
using btg_pqr_back.Core.Interfaces.Repository;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace btg_pqr_back.Core.Commands
{
    public class CreatePqrCommand : IRequest<IGlobalResponse<CreatePqrCommand>>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Mensaje")]
        public string MessageUser { get; set; }

        [Display(Name = "Respuesta")]
        public string ResponseAdmin { get; set; }

        public DateTime DateRequest { get; set; }

        public DateTime? DateResponse { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Tipo de PQR")]
        public int Type { get; set; }

        public int? PqrId { get; set; }

        public bool Active { get; set; }
    }

    public partial class CreatePqrCommandHandler : IRequestHandler<CreatePqrCommand, IGlobalResponse<CreatePqrCommand>>
    {
        private readonly IGlobalResponse<CreatePqrCommand> globalResponse;
        private readonly IPqrRepository<PqrEntity> pqrRepository;
        private readonly IClaimRepository<ClaimEntity> claimRepository;
        private readonly IMapper mapper;

        public CreatePqrCommandHandler(IGlobalResponse<CreatePqrCommand> _globalResponse, IPqrRepository<PqrEntity> _pqrRepository, IMapper _mapper, IClaimRepository<ClaimEntity> _claimRepository)
        {
            claimRepository = _claimRepository;
            globalResponse = _globalResponse;
            pqrRepository = _pqrRepository;
            mapper = _mapper;
        }

        public async Task<IGlobalResponse<CreatePqrCommand>> Handle(CreatePqrCommand request, CancellationToken cancellationToken)
        {
            var openPqr = await pqrRepository.GetByIdAsync(request.PqrId.GetValueOrDefault());

            ValidateClaimRequest(openPqr, request.Type);

            var activePqr = await pqrRepository.GetByUserAndActive(request.UserName);

            ValidateActivePqr(activePqr);

            var pqr = mapper.Map<PqrEntity>(request);
            var save = await pqrRepository.AddAsync(pqr);

            ValidateSave(save, request.UserName);

            await SaveClaim(pqr, request.PqrId.GetValueOrDefault());

            globalResponse.Message = $"Dear {request.UserName}, your PCC is created Successfully";
            globalResponse.Data = request;
            return globalResponse;
        }

        public async Task SaveClaim(PqrEntity obj, int pqrId)
        {
            if (obj.Type == (int)PqrTypeEnum.Claim && pqrId > 0)
            {
                var claim = new ClaimEntity
                {
                    PqrId = pqrId,
                    ClaimId = obj.Id
                };

                await claimRepository.AddAsync(claim);
                
                obj.Active = true;
                await pqrRepository.UpdateAsync(obj);
            }
        }
    }

    public partial class CreatePqrCommandHandler
    {

        public Action<PqrEntity> ValidateActivePqr = (pqr) =>
        {
            if (pqr != null && pqr.Active)
            {
                throw new PqrException(400,
                    $"Dear {pqr.UserName}, already have 1 claim pending, please wait {pqr.CountDays()} days for a response.");
            }
        };

        public Action<PqrEntity, int> ValidateClaimRequest = (pqr, typePqr) =>
        {
            if (typePqr == (int)PqrTypeEnum.Claim)
            {
                if (!pqr.CanClaim())
                {
                    throw new PqrException(400,
                        $"Dear {pqr.UserName}, can't request a claim at this time, you must wait at least {pqr.CountDays()} days for a response.");
                }
            }
        };

        public Action<int, string> ValidateSave = (save, userName) =>
        {
            if (save <= 0)
            {
                throw new PqrException(500, $"The request for user {userName} can't be processed successfully");
            }
        };
    }
}
