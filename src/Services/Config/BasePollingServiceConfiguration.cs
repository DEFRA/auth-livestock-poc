namespace Livestock.Auth.Services.Config;

public class BasePollingServiceConfiguration
{
    public required string CronSchedule { get; init; }
    
    public required string Type { get; init; }
    
    public required string Description { get; init; }
}