using Livestock.Auth.Database;
using Livestock.Auth.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livestock.Auth.Example.Services;

public interface IUserDataService
{
    Task<List<User>> GetAll();
}

public class UsersDataService(AuthContext context) : IUserDataService
{
    async Task<List<User>> IUserDataService.GetAll()
    {
        var query = await context.Set<User>().AsQueryable().ToListAsync();
        return await context.Set<User>().ToListAsync();
    }
}