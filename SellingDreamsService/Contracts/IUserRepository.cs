using SellingDreamsInfrastructure.Model;
using SellingDreamsService.ContractsDto;

namespace SellingDreamsService.Contracts;

public interface IUserRepository : IDependency
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUser(int id);
    Task CreateUser(User user);
    Task UpdateUser(User user);
    Task UpdateUser(IUserPatchDto user);
    Task DeleteUser(int id);
}
