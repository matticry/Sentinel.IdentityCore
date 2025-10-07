using AutoMapper;
using MediatR;
using Sentinel.Identity.Application.DTOs.Auth;
using Sentinel.Identity.Domain.Entities;
using Sentinel.Identity.Domain.Exceptions;
using Sentinel.Identity.Domain.Repositories;
using Sentinel.Identity.Domain.Services;

namespace Sentinel.Identity.Application.Commands.Users;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<UserListDto>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository repository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        
    }

    public async Task<ApiResponse<UserListDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.GetByEmailAsync(request.User.Email, cancellationToken) != null)
            throw new ValidationException("Email already exists");

        if (await _repository.GetByUsernameAsync(request.User.Username, cancellationToken) != null)
            throw new ValidationException("Username already exists");

        if (await _repository.GetByDniAsync(request.User.Dni, cancellationToken) != null) throw new ValidationException("DNI already exists");
        
        var passwordHash = _passwordHasher.Hash(request.User.Password);

        var user = User.Create(
            request.User.Name,
            request.User.LastName,
            request.User.Dni,
            request.User.Age,
            request.User.Username,
            request.User.Email, 
            passwordHash,
            request.User.Phone,
            request.User.Address
        );

        var created = await _repository.AddAsync(user, cancellationToken);
        var dto = _mapper.Map<UserListDto>(created);

        return ApiResponse<UserListDto>.SuccessResponse(dto, "User created successfully");
    }
}