using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConsoleApp1;

public class DbOptions : Dictionary<AppName, DbOption>
{
    
}

public record DbOption 
{
    public required string DbConnection { get; init; }
}

public static class DbOptionsExtension
{
    public static IServiceCollection AddDbOption(this IServiceCollection services, AppName appName, Func<DbOption, IServiceProvider, DbOption>? func = null)
    {
        services.AddOptions<DbOptions>()
                .BindConfiguration("")
                .PostConfigure<IServiceProvider>((option, sp) =>
                {
                    option[appName] = option[appName] with
                    {
                        DbConnection = option[appName].DbConnection ?? option[AppName.Default].DbConnection
                    };

                    option[appName] = func?.Invoke(option[appName], sp) ?? option[appName];
                });

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<DbOptions>>().Value[appName]);

        return services;
    }
}
