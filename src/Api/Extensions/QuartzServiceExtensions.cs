using Livestock.Auth.Config;
using Livestock.Auth.Services;
using Livestock.Auth.Services.Config;
using Quartz;

namespace Livestock.Auth.Extensions;

public static class QuartzServiceExtensions
{
    public static IServiceCollection AddQuartzServices(this IServiceCollection sc, IConfigurationRoot config)
    {
        sc.AddTransient<IKrdsService, KrdsSyncService>();
        sc.AddTransient<IAzureB2CService, AzureB2CSyncService>();

        sc.AddOptions<KrdsSyncServiceConfiguration>($"PolledServices:{nameof(KrdsSyncService)}");
        sc.AddOptions<AzureB2CSyncServiceConfiguration>($"PolledServices:{nameof(AzureB2CSyncService)}");
        
        var polledServices = config.GetSection("PolledServices");
        Requires.NotNull(polledServices);

        sc.AddQuartz(q =>
        {
            foreach (var serviceConfig in polledServices.GetChildren())
            {
                var baseService = serviceConfig.Get<BasePollingServiceConfiguration>();
                q.AddLocalJob(baseService, "PolledServices", serviceConfig.Key);
            }
        });

        sc.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        
        return sc;
    }

    private static void AddLocalJob(
        this IServiceCollectionQuartzConfigurator q,
        BasePollingServiceConfiguration baseService,
        string jobGroup,
        string jobName)
    {
        var jobKey = new JobKey(jobName, jobGroup);
        
        var serviceType = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(asm => asm.FullName?.StartsWith("Livestock.Auth", StringComparison.OrdinalIgnoreCase) == true)
            .SelectMany(asm => asm.GetTypes())
            .FirstOrDefault(t => string.Equals(t.FullName, baseService.Type, StringComparison.OrdinalIgnoreCase));

        if (serviceType == null)
        {
            throw new InvalidOperationException($"Service type '{baseService.Type}' not found in Livestock.Auth assemblies");
        }

        q.AddJob(serviceType, jobKey, j => j
            .WithDescription(baseService.Description)
        );

        q.AddTrigger(t => t
            .WithIdentity($"{jobName} Cron Trigger")
            .ForJob(jobKey)
            .StartNow()
            .WithCronSchedule(baseService.CronSchedule)
        );
    }
}