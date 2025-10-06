using AutoMapper;
using MediatR;
using Sentinel.Identity.Application.Commands;
using Sentinel.Identity.Application.DTOs.Auth;
using Sentinel.Identity.Domain.Repositories;

namespace Sentinel.Identity.Application.Queries.Users;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ApiResponse<IEnumerable<UserListDto>>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IEnumerable<UserListDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllAsync(cancellationToken);
        var dtos = _mapper.Map<IEnumerable<UserListDto>>(users);

        return ApiResponse<IEnumerable<UserListDto>>.SuccessResponse(dtos);
    }
}