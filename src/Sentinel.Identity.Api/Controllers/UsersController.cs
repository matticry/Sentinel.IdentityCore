using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sentinel.Identity.Application.Commands;
using Sentinel.Identity.Application.Commands.Users;
using Sentinel.Identity.Application.DTOs.Auth;
using Sentinel.Identity.Application.Queries.Users;

namespace Sentinel.Identity.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserListDto>>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("getUserById/{id}")]
    public async Task<ActionResult<ApiResponse<UserListDto>>> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPost("createUser")]
    public async Task<ActionResult<ApiResponse<UserListDto>>> Create([FromBody] UserWriteDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateUserCommand(dto), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    [HttpPut("updateUserById/{id}")]
    public async Task<ActionResult<ApiResponse<UserListDto>>> Update(int id, [FromBody] UserWriteDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new UpdateUserCommand(id, dto), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("deleteLogicUserById/{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id), cancellationToken);
        return Ok(result);
    }
    
    [HttpDelete("deleteForceUserById/{id}")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteForce(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteForceUserCommand(id), cancellationToken);
        return Ok(result);
    }
}