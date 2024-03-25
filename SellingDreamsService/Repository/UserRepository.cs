using Microsoft.EntityFrameworkCore;
using SellingDreamsInfrastructure;
using SellingDreamsInfrastructure.Model;
using SellingDreamsService.Contracts;

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
        if (_context.User.Any(u => u.Name == user.Name && u.EmailAdress == user.EmailAdress))
            throw new Exception("User with the same Id and Email exits");
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        if (!_context.User.Any(u => u.Id == user.Id))
            throw new Exception(UserNotExits(user.Id));
        var oldUser = await _context.User.Where(u => u.Id == user.Id).SingleAsync();
        if (oldUser.Name != user.Name)
            oldUser.Name = user.Name;
        if (oldUser.Adress != user.Adress)
            oldUser.Adress = user.Adress;
        if (oldUser.EmailAdress != user.EmailAdress)
            oldUser.EmailAdress = user.EmailAdress;
        if (oldUser.PhoneNumber != user.PhoneNumber)
            oldUser.PhoneNumber = user.PhoneNumber;
        await _context.SaveChangesAsync();
    }
}
