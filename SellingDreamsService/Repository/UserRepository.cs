using Microsoft.EntityFrameworkCore;
using SellingDreamsInfrastructure;
using SellingDreamsInfrastructure.Model;
using SellingDreamsService.Contracts;
using SellingDreamsService.ContractsDto;

namespace SellingDreamsService.Repository;

class UserRepository : IUserRepository
{
    private string UserNotExits(int id) => $"User with Id {id} does not exits";

    private readonly InfrastructureDbContext _context;

    public UserRepository(InfrastructureDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsers() => await _context.User.ToListAsync();

    public async Task<User> GetUser(int id)
    {
        if (!_context.User.Any(u => u.Id == id))
            throw new Exception(UserNotExits(id));
        return await _context.User.SingleAsync(u => u.Id == id);
    }

    public async Task CreateUser(User user)
    {
        if (_context.User.Any(u => u.Name == user.Name && u.EmailAddress == user.EmailAddress))
            throw new Exception("User with the same Id and Email exits");
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        if (!_context.User.Any(u => u.Id == user.Id))
            throw new Exception(UserNotExits(user.Id));
        var oldUser = await _context.User.Where(u => u.Id == user.Id).SingleAsync();
        if (oldUser.Address != user.Address)
            oldUser.Address = user.Address;
        if (oldUser.PhoneNumber != user.PhoneNumber)
            oldUser.PhoneNumber = user.PhoneNumber;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(IUserPatchDto user)
    {
        if (!_context.User.Any(u => u.Id == user.Id))
            throw new Exception(UserNotExits(user.Id));
        var oldUser = await _context.User.Where(u => u.Id == user.Id).SingleAsync();
        if (user.Address != null && oldUser.Address != user.Address)
            oldUser.Address = user.Address;
        if (user.PhoneNumber != null && oldUser.PhoneNumber != user.PhoneNumber)
            oldUser.PhoneNumber = user.PhoneNumber;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = await _context.User.SingleAsync(u => u.Id == id);
        if (user == null)
            throw new Exception(UserNotExits(id));

        _context.Remove(user);
        await _context.SaveChangesAsync();
    }
}
