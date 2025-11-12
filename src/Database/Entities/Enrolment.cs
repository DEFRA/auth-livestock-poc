using Livestock.Auth.Database.Entities.Base;

namespace Livestock.Auth.Database.Entities;

public class Enrolment : BaseUpdateEntity
{
    public Guid B2cObjectId { get; set; }
    public Guid ApplicationId { get; set; }
    public string CphId { get; set; }
    public string Role { get; set; }
    public string Scope { get; set; }
    
    public string Status { get; set; }
    public DateTime EnrolledAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}