using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Rules
{
    public class GitHubProfileBusinessRules
    {
        private readonly IGitHubProfileRepository _gitHubProfileRepository;

        public GitHubProfileBusinessRules(IGitHubProfileRepository gitHubProfileRepository)
        {
            _gitHubProfileRepository = gitHubProfileRepository;
        }

        public async Task GitHubProfileAddressCanNotBeDuplicatedWhenInserted(string address)
        {
            IPaginate<GitHubProfile> result = await _gitHubProfileRepository.GetListAsync(p => p.GitHubAddress == address);
            if (result.Items.Any()) throw new BusinessException("GitHub address exists.");
        }

        public void GitHubProfileShouldExistWhenRequested(GitHubProfile gitHubProfile)
        {
            if (gitHubProfile == null) throw new BusinessException("Requested gitHub profile does not exists.");
        }
    }
}
