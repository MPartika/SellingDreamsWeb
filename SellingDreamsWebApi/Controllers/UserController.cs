using Microsoft.AspNetCore.Mvc;
using SellingDreamsCommandHandler;
using SellingDreamsCommandHandler.Users.UpdateUsers;
using SellingDreamsCommandHandler.Users.CreateUsers;
using SellingDreamsCommandHandler.Users.GetAllUsers;

namespace SellingDreamsWebApi.Controllers;

[Route("[controller]")]
public class UsersController : Controller
{
    private readonly ICommandHandlerListAsync<GetAllUsersCommand, GetAllUsersCommandResponse> _getAllCommand;
    private readonly ICommandHandlerAsync<CreateUsersCommand> _createCommand;
    private readonly ICommandHandlerAsync<UpdateUsersCommand> _updateCommand;
    public UsersController(ICommandHandlerListAsync<
        GetAllUsersCommand, GetAllUsersCommandResponse> getAllCommand,
        ICommandHandlerAsync<CreateUsersCommand> createCommand,
        ICommandHandlerAsync<UpdateUsersCommand> updateCommand)
    {
        _getAllCommand = getAllCommand;
        _createCommand = createCommand;
        _updateCommand = updateCommand;
    }

    [HttpGet()]
    public async Task<IActionResult> Users()
    {
        return Ok(await _getAllCommand.ExecuteAsync(new GetAllUsersCommand()));
    }

    [HttpPost()]
    public async Task<IActionResult> Users([FromBody] CreateUsersCommand command)
    {
        await _createCommand.Execute(command);
        return Ok();
    }

    [HttpPatch()]
    public async Task<IActionResult> Users([FromBody] UpdateUsersCommand command)
    {
        await _updateCommand.Execute(command);
        return Ok();
    }
}
