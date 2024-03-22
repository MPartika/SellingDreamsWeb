namespace SellingDreamsService.Repository;

public class UserRepository {
    private readonly InfrastructureDbContext _dbContext;

    public UserRepository(InfrastructureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task GetUsers()
    {

    }
}
