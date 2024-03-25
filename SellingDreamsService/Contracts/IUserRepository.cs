using SellingDreamsInfrastructure.Model;

namespace SellingDreamsService.Contracts;

public interface IUserRepository : IDependency
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUser(int id);
    Task CreateUser(User user);
    Task UpdateUser(User user);
}
