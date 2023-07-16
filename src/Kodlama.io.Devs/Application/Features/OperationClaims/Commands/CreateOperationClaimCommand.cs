﻿using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands
{
    public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimDto>, ISecuredRequest
    {
        public string Name { get; set; }
        public string[] Roles => new[] { "Admin" };

        public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

            public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _operationClaimBusinessRules = operationClaimBusinessRules;
            }

            public async Task<CreatedOperationClaimDto> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _operationClaimBusinessRules.OperationClaimCanNotBeDuplicatedWhenInserted(request.Name);

                OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim createOperationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);
                CreatedOperationClaimDto createOperationClaimDto = _mapper.Map<CreatedOperationClaimDto>(createOperationClaim);

                return createOperationClaimDto;
            }
        }
    }
}
