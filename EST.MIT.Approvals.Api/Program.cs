using Approvals.Api.Endpoints;
using Approvals.Api.Models;
using EST.MIT.Approvals.Api.Authentication;
using EST.MIT.Approvals.Api.endpoints;
using EST.MIT.Approvals.Api.SeedProvider.Provider;
using EST.MIT.Approvals.Data;
using Microsoft.EntityFrameworkCore;
using EST.MIT.Approvals.Api;

var builder = WebApplication.CreateBuilder(args);

SQLscriptWriter? _sqlScriptWriter = default;
CreateAndInsertSqlCommandInterceptor? createAndInsertSQLCommandInterceptor = default;

var isLocalDev = builder.Configuration.IsLocalDatabase(builder.Configuration);

if (isLocalDev)
{
    _sqlScriptWriter = new SQLscriptWriter($"MIT_Approvals_Seed_SQL_v{{version}}_{DateTime.Now.ToString("yyyyMMdd-HHmm")}.sql");
    createAndInsertSQLCommandInterceptor = new CreateAndInsertSqlCommandInterceptor(_sqlScriptWriter);
}

var aadAuthenticationInterceptor = new AadAuthenticationInterceptor(new TokenGenerator(), builder.Configuration);

builder.Services.AddDbContext<ApprovalsContext>(options =>
{
    var connStringTask = aadAuthenticationInterceptor.GetConnectionStringAsync();
    var connString = connStringTask.GetAwaiter().GetResult();

    options.AddInterceptors(aadAuthenticationInterceptor);
    if (isLocalDev && createAndInsertSQLCommandInterceptor is not null)
    {
        options.AddInterceptors(createAndInsertSQLCommandInterceptor);
    }

    options
        .UseNpgsql(
            connString,
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

if (isLocalDev)
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApprovalsContext>();
        SeedProvider.SeedReferenceData(db, builder.Configuration, _sqlScriptWriter, "1.0.2");
    }
}

app.Run();
