namespace Livestock.Auth.Endpoints.Users;

public static class UsersEndpoints
{
    public  static void UseUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("users", GetAll);
    }
    private static async Task<IResult> GetAll(
        IUserDataService service)
    {
        
        var matches = await service.GetAll();
        return Results.Ok(matches);
    }
}