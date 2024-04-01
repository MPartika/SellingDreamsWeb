using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SellingDreamsCommandHandler;
using SellingDreamsCommandHandler.Users.CreateUsers;
using SellingDreamsCommandHandler.Users.DeleteUsers;
using SellingDreamsCommandHandler.Users.GetAllUsers;
using SellingDreamsCommandHandler.Users.GetUser;
using SellingDreamsCommandHandler.Users.PatchUsers;
using SellingDreamsCommandHandler.Users.UpdateUsers;

namespace SellingDreamsWebApi.Controllers;

[Route("[controller]")]
[Authorize]
public class UsersController : Controller
{
    private readonly ICommandHandlerListAsync<
        GetAllUsersCommand,
        GetAllUsersCommandResponse
    > _getAllCommand;
    private readonly ICommandHandlerAsync<GetUserCommand, GetUserCommandResponse> _getUserCommand;
    private readonly ICommandHandlerAsync<CreateUsersCommand> _createCommand;
    private readonly ICommandHandlerAsync<UpdateUsersCommand> _updateCommand;
    private readonly ICommandHandlerAsync<PatchUsersCommand> _patchCommand;
    private readonly ICommandHandlerAsync<DeleteUsersCommand> _deleteCommand;

    public UsersController(
        ICommandHandlerListAsync<GetAllUsersCommand, GetAllUsersCommandResponse> getAllCommand,
        ICommandHandlerAsync<CreateUsersCommand> createCommand,
        ICommandHandlerAsync<UpdateUsersCommand> updateCommand,
        ICommandHandlerAsync<GetUserCommand, GetUserCommandResponse> getUserCommand,
        ICommandHandlerAsync<PatchUsersCommand> patchCommand,
        ICommandHandlerAsync<DeleteUsersCommand> deleteCommand)
    {
        _getAllCommand = getAllCommand;
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _getUserCommand = getUserCommand;
        _patchCommand = patchCommand;
        _deleteCommand = deleteCommand;
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
        await _createCommand.ExecuteAsync(command);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Users(int id, [FromBody] UpdateUsersCommand command)
    {
        command.UserId = id;
        await _updateCommand.ExecuteAsync(command);
        return Ok();
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Users(int id, [FromBody] PatchUsersCommand command)
    {
        command.Id = id;
        await _patchCommand.ExecuteAsync(command);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUsers(int id)
    {
        await _deleteCommand.ExecuteAsync(new DeleteUsersCommand { Id = id});
        return Ok();
    }
}
