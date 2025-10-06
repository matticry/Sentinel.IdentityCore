using MediatR;
using Sentinel.Identity.Application.Commands;
using Sentinel.Identity.Application.DTOs.Auth;

namespace Sentinel.Identity.Application.Queries.Users;

public record GetUserByIdQuery(int Id) : IRequest<ApiResponse<UserListDto>>;
