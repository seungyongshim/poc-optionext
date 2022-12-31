using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateDefaultBuilder()
                  .ConfigureServices((context, services) =>
                  {
                      services.AddDbOption(AppName.ConsoleApp1, (option, sp) => option with
                      {
                          DbConnection = "AAA"
                      });
                      services.AddDbOption(AppName.ConsoleApp2);
                  });


var app = builder.Build();

var dbOption = app.Services.GetRequiredService<IOptions<DbOptions>>();

Console.WriteLine(dbOption.Value[AppName.ConsoleApp1].DbConnection);
await app.RunAsync();
