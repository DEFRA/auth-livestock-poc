using Livestock.Auth.Database;
using Livestock.Auth.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livestock.Auth.Endpoints.Users;

public interface IUserDataService
{
    Task<List<User>> GetAll();
}

public class UsersDataService(AuthContext context) : IUserDataService
{
    async Task<List<User>> IUserDataService.GetAll()
    {
        var query = context.Users.AsQueryable();
        return await query.ToListAsync();
    }
}