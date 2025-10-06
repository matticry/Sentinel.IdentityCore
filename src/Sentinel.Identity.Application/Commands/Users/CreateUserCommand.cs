using MediatR;
using Sentinel.Identity.Application.DTOs.Auth;

namespace Sentinel.Identity.Application.Commands.Users;

public record CreateUserCommand(UserWriteDto User) : IRequest<ApiResponse<UserListDto>>;
