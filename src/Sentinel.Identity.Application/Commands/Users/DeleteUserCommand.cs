using MediatR;

namespace Sentinel.Identity.Application.Commands.Users;

public record DeleteUserCommand(int Id) : IRequest<ApiResponse<object>>;
