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

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IUserRepository _userRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task UserOperationClaimCanNotBeDuplicatedWhenInserted(int userId, int operationClaimId)
        {
            IPaginate<UserOperationClaim> result = await _userOperationClaimRepository.GetListAsync(
                o => o.UserId == userId && o.OperationClaimId == operationClaimId, enableTracking: false);
            if (result.Items.Any()) throw new BusinessException("User operation claim exists");
        }

        public async Task OperationClaimShouldExistWhenRequestedAsync(int id)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(o => o.Id == id);
            if (operationClaim == null) throw new BusinessException("Operation claim not exists");
        }

        public async Task UserOperationClaimShouldExistWhenRequestedAsync(int id)
        {
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(o => o.Id == id);
            if (userOperationClaim == null) throw new BusinessException("User operation claim not exists");
        }

        public void UserOperationClaimShouldExistWhenRequestedAsync(UserOperationClaim userOperationClaim)
        {
            if (userOperationClaim == null) throw new BusinessException("User operation claim not exists");
        }
    }
}
