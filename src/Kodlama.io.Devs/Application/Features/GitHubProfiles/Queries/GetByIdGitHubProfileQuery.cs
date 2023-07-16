using Application.Features.GitHubProfiles.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Queries
{
    public class GetByIdGitHubProfileQuery : IRequest<GitHubProfileGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdGitHubProfileQueryHandler : IRequestHandler<GetByIdGitHubProfileQuery, GitHubProfileGetByIdDto>
        {
            private readonly IGitHubProfileRepository _repository;
            private readonly IMapper _mapper;

            public GetByIdGitHubProfileQueryHandler(IGitHubProfileRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GitHubProfileGetByIdDto> Handle(GetByIdGitHubProfileQuery request, CancellationToken cancellationToken)
            {
                GitHubProfile? gitHubProfile = await _repository.GetAsync(x => x.Id == request.Id);
                GitHubProfileGetByIdDto mappedGitHubProfile = _mapper.Map<GitHubProfileGetByIdDto>(gitHubProfile);
                return mappedGitHubProfile;
            }
        }
    }
}
