using SoftwareSauna.CodeChallenge.Infrastructure;
using SoftwareSauna.CodeChallenge.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLogging();

builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseInfrastrcture();

app.Run();


namespace SoftwareSauna.CodeChallenge.Host
{
    public partial class Program
    {
        protected Program() { }
    }
}