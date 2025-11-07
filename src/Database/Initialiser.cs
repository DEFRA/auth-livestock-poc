using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Livestock.Auth.Database;

public static class Initialiser
{
    public static void UseAuthDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AuthContext>();
        
        if(context.Database.CanConnect()) 
            context.Database.OpenConnection();
    }
}