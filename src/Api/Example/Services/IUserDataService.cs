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
        return await context.Set<User>().ToListAsync();
    }
}