using AutoMapper;
using MediatR;
using Sentinel.Identity.Application.DTOs.Auth;
using Sentinel.Identity.Domain.Exceptions;
using Sentinel.Identity.Domain.Repositories;

namespace Sentinel.Identity.Application.Commands.Users;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<UserListDto>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UserListDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException($"User with ID {request.Id} not found");

        var emailExists = await _repository.GetByEmailAsync(request.User.Email, cancellationToken);
        if (emailExists != null && emailExists.Id != request.Id)
            throw new ValidationException("Email already exists");

        var usernameExists = await _repository.GetByUsernameAsync(request.User.Username, cancellationToken);
        if (usernameExists != null && usernameExists.Id != request.Id)
            throw new ValidationException("Username already exists");

        var dniExists = await _repository.GetByDniAsync(request.User.Dni, cancellationToken);
        if (dniExists != null && dniExists.Id != request.Id)
            throw new ValidationException("DNI already exists");

        user.Update(
            request.User.Name,
            request.User.LastName,
            request.User.Dni,
            request.User.Age,
            request.User.Username,
            request.User.Email,
            request.User.Phone,
            request.User.Address
        );

        await _repository.UpdateAsync(user, cancellationToken);
        var dto = _mapper.Map<UserListDto>(user);

        return ApiResponse<UserListDto>.SuccessResponse(dto, "User updated successfully");
    }
}