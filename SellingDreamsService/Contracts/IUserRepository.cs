using SellingDreamsInfrastructure.Model;

namespace SellingDreamsService.Contracts;

public interface IUserRepository : IDependency
{
    Task<IEnumerable<User>> GetUsers();
    Task CreateUser(User user);
    Task UpdateUser(User user);
}
