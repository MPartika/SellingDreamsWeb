using SellingDreamsInfrastructure.Model;
using SellingDreamsService.ContractsDto;

namespace SellingDreamsService.Contracts;

public interface IAuthenticationRepository : IDependency
{
    Task<UserLogin?> GetLogin(int id);
    Task<UserLogin?> GetLogin(string name);
    Task CreateLogin(UserLogin login);
    Task UpdateLogin(UserLogin login);
    Task PatchLogin(IUserLoginPatchDto login);
    Task DeLeteLogin(int id);
}

