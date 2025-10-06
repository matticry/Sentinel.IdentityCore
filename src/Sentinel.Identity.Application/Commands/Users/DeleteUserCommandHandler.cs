using MediatR;
using Sentinel.Identity.Domain.Exceptions;
using Sentinel.Identity.Domain.Repositories;

namespace Sentinel.Identity.Application.Commands.Users;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<object>>
{
    private readonly IUserRepository _repository;

    public DeleteUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _repository.ExistsAsync(request.Id, cancellationToken))
            throw new NotFoundException($"User with ID {request.Id} not found");

        await _repository.DeleteAsync(request.Id, cancellationToken);

        return ApiResponse<object>.SuccessResponse("User deleted successfully");
    }
}