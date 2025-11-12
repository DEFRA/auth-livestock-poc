using Livestock.Auth.Database.Entities.Base;

namespace Livestock.Auth.Database.Entities;

public class Application : BaseUpdateEntity
{
  
    public string Name { get; set; } = string.Empty;
    public Guid B2cClientId { get; set; }
    public string B2cTenant { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
  
}