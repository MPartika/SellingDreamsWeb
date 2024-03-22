using Microsoft.AspNetCore.Mvc;
using SellingDreamsCommandHandler;
using SellingDreamsCommandHandler.Users.CreateUsers;
using SellingDreamsCommandHandler.Users.GetAllUsers;

namespace SellingDreamsWebApi.Controllers;

[Route("[controller]")]
public class UsersController : Controller
{
    private readonly ICommandHandlerListAsync<GetAllUsersCommand, GetAllUsersCommandResponse> _getAllCommand;
    private readonly ICommandHandlerAsync<CreateUsersCommand> _createCommand;
    public UsersController(ICommandHandlerListAsync<
        GetAllUsersCommand, GetAllUsersCommandResponse> getAllCommand,
        ICommandHandlerAsync<CreateUsersCommand> createCommand)
    {
        _getAllCommand = getAllCommand;
        _createCommand = createCommand;
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

}
