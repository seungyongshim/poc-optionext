using System;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1;

public class DbOptions : Dictionary<AppName, DbOption> { }

public record DbOption(string DbConnection);


public static class DbOptionsExtension
{
    public static IServiceCollection AddDbOption(this IServiceCollection services, AppName appName, Func<DbOption, IServiceProvider, DbOption>? func = null)
    {
        services.AddOptions<DbOptions>()
                .BindConfiguration("")
                .PostConfigure<IServiceProvider>((option, sp) =>
                {
                    option[appName] = func?.Invoke(option[appName], sp) ?? option[appName];
                });

        return services;
    }
}
