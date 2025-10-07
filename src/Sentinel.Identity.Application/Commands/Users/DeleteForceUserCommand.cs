using MediatR;

namespace Sentinel.Identity.Application.Commands.Users;

public record DeleteForceUserCommand(int Id) : IRequest<ApiResponse<object>>;
