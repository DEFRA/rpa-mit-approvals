using Approvals.Api.Endpoints;
using Approvals.Api.Models;
using EST.MIT.Approvals.Api.endpoints;
using EST.MIT.Approvals.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();

var host = config["POSTGRES_HOST"];
var db = config["POSTGRES_DB"];
var port = config["POSTGRES_PORT"];
var user = config["POSTGRES_USER"];
var pass = config["POSTGRES_PASSWORD"];

var postgres = string.Format(config["DbConnectionTemplate"]!, host, port, db, user, pass);

Console.WriteLine($"DBConnectionString: {postgres}");

builder.Services.AddDbContext<ApprovalsContext>(options =>
{
    options
        .UseNpgsql(
            postgres,
            x => x.MigrationsAssembly("EST.MIT.Approvals.Data")
        )
        .UseSnakeCaseNamingConvention();
});


builder.Services.AddSwaggerServices();
builder.Services.AddApprovalsServices();

builder.Services.AddOptions();
builder.Services.Configure<ValidationSettings>(
    builder.Configuration.GetSection("ValidationSettings"));

var app = builder.Build();


app.SwaggerEndpoints();
app.MapHealthCheckGetEndpoints();
app.MapInvoiceApprovalsEndpoints();

app.Run();
