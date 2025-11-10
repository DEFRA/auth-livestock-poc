using System.Linq.Expressions;
using Livestock.Auth.Database;
using Livestock.Auth.Database.Entities;

namespace Livestock.Auth.Services;

public class UsersDataService(AuthContext context): IDataService<User>
{
    public async Task<List<User>> GetAll()
    {
        var query = context.Users.AsQueryable();
        return await query.ToListAsync();
    }

    public async Task<User?> Get(Expression<Func<User, bool>> predicate)
    {
        var query = await context.Users.SingleOrDefaultAsync(predicate);
        return query ?? null;
    }

    public Task<User> Create(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> Update(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Func<User, bool> predicate)
    {
        throw new NotImplementedException();
    }
}