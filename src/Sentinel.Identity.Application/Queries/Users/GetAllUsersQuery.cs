using MediatR;
using Sentinel.Identity.Application.Commands;
using Sentinel.Identity.Application.DTOs.Auth;

namespace Sentinel.Identity.Application.Queries.Users;

public record GetAllUsersQuery : IRequest<ApiResponse<IEnumerable<UserListDto>>>;
