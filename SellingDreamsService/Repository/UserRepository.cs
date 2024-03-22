using Microsoft.EntityFrameworkCore;
using SellingDreamsInfrastructure.Model;
using SellingDreamsInfrastructure;
using SellingDreamsService.Contracts;

namespace SellingDreamsService.Repository;

class UserRepository : IUserRepository
{
    private readonly InfrastructureDbContext _context;

    public UserRepository(InfrastructureDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsers() => await _context.User.ToListAsync();
    public async Task CreateUser(User user) 
    {
        if (_context.User.Any(u => u.Name == user.Name && u.EmailAdress == user.EmailAdress))
            throw new Exception("User with the same Id and Email exits");
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
