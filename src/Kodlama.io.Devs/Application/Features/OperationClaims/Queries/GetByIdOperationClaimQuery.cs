using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Queries
{
    public class GetByIdOperationClaimQuery : IRequest<OperationClaimGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, OperationClaimGetByIdDto>
        {
            private readonly IOperationClaimRepository _operationClaimrepository;
            private readonly IMapper _mapper;
            OperationClaimBusinessRules _operationClaimBusinessRules;

            public GetByIdOperationClaimQueryHandler(IOperationClaimRepository operationClaimrepository, IMapper mapper)
            {
                _operationClaimrepository = operationClaimrepository;
                _mapper = mapper;
            }

            public async Task<OperationClaimGetByIdDto> Handle(GetByIdOperationClaimQuery request, CancellationToken cancellationToken)
            {
                OperationClaim? operationClaim = await _operationClaimrepository.GetAsync(x => x.Id == request.Id);
                _operationClaimBusinessRules.OperationClaimShouldExistWhenRequested(operationClaim);
                OperationClaimGetByIdDto mappedOperationClaim = _mapper.Map<OperationClaimGetByIdDto>(operationClaim);
                return mappedOperationClaim;
            }
        }
    }
}
