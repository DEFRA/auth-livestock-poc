using Livestock.Auth.Database.Entities.Base;

namespace Livestock.Auth.Database.Entities;

public class Federation : BaseUpdateEntity
{
    public Guid UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; }
    public string B2cTenant { get; set; }
    public Guid B2cObjectId { get; set; }
    public string TrustLevel { get; set; }
    public string SyncStatus { get; set; }
    public DateTime LastSyncedAt { get; set; }

    
}