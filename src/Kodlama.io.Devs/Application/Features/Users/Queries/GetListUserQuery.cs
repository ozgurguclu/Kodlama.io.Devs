using Application.Features.Users.Models;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetListUser
{
    public class GetListUserQuery : IRequest<UserListModel>, ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public string[] Roles => new[] { "Admin" };

        public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, UserListModel>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;

            public GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<UserListModel> Handle(GetListUserQuery request, CancellationToken cancellationToken)
            {
                IPaginate<User>? users = await _userRepository.GetListAsync(
                                                include: x => x.Include(u => u.UserOperationClaims).ThenInclude(o => o.OperationClaim),
                                                index: request.PageRequest.Page,
                                                size: request.PageRequest.PageSize,
                                                enableTracking: true);

                await _userBusinessRules.CheckIfAnyDataInUserList(users);

                UserListModel mappedUserDto = _mapper.Map<UserListModel>(users);
                return mappedUserDto;
            }
        }
    }
}
