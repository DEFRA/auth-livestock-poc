using System.Data;

namespace Livestock.Auth.Database.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime Created { get; set; }
    public DateTime Deleted { get; set; }
}