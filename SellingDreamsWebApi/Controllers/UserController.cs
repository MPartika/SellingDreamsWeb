using Microsoft.AspNetCore.Mvc;
using SellingDreamsCommandHandler;
using SellingDreamsCommandHandler.Users.CreateUsers;
using SellingDreamsCommandHandler.Users.GetAllUsers;
using SellingDreamsCommandHandler.Users.GetUser;
using SellingDreamsCommandHandler.Users.UpdateUsers;

namespace SellingDreamsWebApi.Controllers;

[Route("[controller]")]
public class UsersController : Controller
{
    private readonly ICommandHandlerListAsync<
        GetAllUsersCommand,
        GetAllUsersCommandResponse
    > _getAllCommand;
    private readonly ICommandHandlerAsync<GetUserCommand, GetUserCommandResponse> _getUserCommand;
    private readonly ICommandHandlerAsync<CreateUsersCommand> _createCommand;
    private readonly ICommandHandlerAsync<UpdateUsersCommand> _updateCommand;

    public UsersController(
        ICommandHandlerListAsync<GetAllUsersCommand, GetAllUsersCommandResponse> getAllCommand,
        ICommandHandlerAsync<CreateUsersCommand> createCommand,
        ICommandHandlerAsync<UpdateUsersCommand> updateCommand,
        ICommandHandlerAsync<GetUserCommand, GetUserCommandResponse> getUserCommand
    )
    {
        _getAllCommand = getAllCommand;
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _getUserCommand = getUserCommand;
    }

    [HttpGet()]
    public async Task<IActionResult> Users()
    {
        return Ok(await _getAllCommand.ExecuteAsync(new GetAllUsersCommand()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Users(int id)
    {
        return Ok(await _getUserCommand.ExecuteAsync(new GetUserCommand { Id = id }));
    }

    [HttpPost()]
    public async Task<IActionResult> Users([FromBody] CreateUsersCommand command)
    {
        await _createCommand.Execute(command);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Users(int id, [FromBody] UpdateUsersCommand command)
    {
        command.UserId = id;
        await _updateCommand.Execute(command);
        return Ok();
    }
}
