using AutoMapper;
using MediatR;
using Sentinel.Identity.Application.Commands;
using Sentinel.Identity.Application.DTOs.Auth;
using Sentinel.Identity.Domain.Exceptions;
using Sentinel.Identity.Domain.Repositories;

namespace Sentinel.Identity.Application.Queries.Users;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserListDto>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UserListDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id, cancellationToken)?? throw new NotFoundException($"User with ID {request.Id} not found");

        var dto = _mapper.Map<UserListDto>(user);

        return ApiResponse<UserListDto>.SuccessResponse(dto);
    }
}