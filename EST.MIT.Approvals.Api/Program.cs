using Approvals.Api.Endpoints;
using Approvals.Api.Models;
using EST.MIT.Approvals.Api.endpoints;
using EST.MIT.Approvals.Api.Extensions;
using EST.MIT.Approvals.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"DBConnectionString: {builder.Configuration["DbConnectionString"]}");

builder.Services.AddDbContext<ApprovalsContext>(options =>
{
    options
        .UseNpgsql(
            builder.Configuration["DbConnectionString"],
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

if (app.Environment.IsDevelopment() && args.Contains("--seed-ref-data"))
{
    app.UseReferenceDataSeeding();
}

app.SwaggerEndpoints();
app.MapHealthCheckGetEndpoints();
app.MapInvoiceApprovalsEndpoints();

app.Run();
