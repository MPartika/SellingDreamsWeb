using SellingDreamsService.Contracts;
using SellingDreamsService.ContractsDto;

namespace SellingDreamsCommandHandler.Authenticate;

public class PatchLoginCommandHandler : ICommandHandlerAsync<PatchLoginCommand>
{
    private readonly IAuthenticationRepository _repository;

    public PatchLoginCommandHandler(IAuthenticationRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(PatchLoginCommand command)
    {
        var dto = new PatchLoginDto { Id = command.Id, UserName = command.UserName, UserId = command.UserId };
        if (command.Password is not null)
        {
            dto.Password = AuthenticationHelper.HashPassword(command.Password, out byte[] salt);
            dto.Salt = salt;
        }

        await _repository.PatchLogin(dto);
    }
}

internal class PatchLoginDto : IUserLoginPatchDto
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public byte[]? Password { get; set; }
    public byte[]? Salt { get; set; }
    public int? UserId { get; set; }
}

