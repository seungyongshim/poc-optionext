using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = Host.CreateDefaultBuilder()
                  .ConfigureServices((context, services) =>
                  {
                      services.AddDbOption(AppName.ConsoleApp1);
                      services.AddDbOption(AppName.ConsoleApp2);
                  });


var app = builder.Build();

var dbOption = app.Services.GetRequiredService<DbOption>();

Console.WriteLine(dbOption.DbConnection);
await app.RunAsync();
