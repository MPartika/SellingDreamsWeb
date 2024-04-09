using SellingDreamsService.Contracts;

namespace SellingDreamsCommandHandler.Users;

public class GetAllUsersCommandHandler : ICommandHandlerListAsync<GetAllUsersCommand, GetAllUsersCommandResponse>
{
    private readonly IUserRepository _repository;

    public GetAllUsersCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GetAllUsersCommandResponse>> ExecuteAsync(GetAllUsersCommand command)
    {
        var response = await _repository.GetUsers();
        return response.Select(result => new GetAllUsersCommandResponse
        {
             Id = result.Id,
             Name = result.Name,
             Address = result.Address,
             EmailAddress = result.EmailAddress,
             PhoneNumber = result.PhoneNumber
        }).ToList();
    }
}
