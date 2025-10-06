using MediatR;
using Sentinel.Identity.Application.DTOs.Auth;

namespace Sentinel.Identity.Application.Commands.Users;

public record UpdateUserCommand(int Id, UserWriteDto User) : IRequest<ApiResponse<UserListDto>>;
