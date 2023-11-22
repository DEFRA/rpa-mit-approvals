using Approvals.Api.Endpoints;
using Approvals.Api.Models;
using EST.MIT.Approvals.Api.Authentication;
using EST.MIT.Approvals.Api.endpoints;
using EST.MIT.Approvals.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var interceptor = new AadAuthenticationInterceptor(new TokenGenerator(), builder.Configuration, builder.Environment.IsProduction());

builder.Services.AddDbContext<ApprovalsContext>(options =>
{
    var connStringTask = interceptor.GetConnectionStringAsync();
    var connString = connStringTask.GetAwaiter().GetResult();

    options.AddInterceptors(interceptor);

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

//if (interceptor.IsLocalDatabase())
//{
//    // Run migrations if your database is local
//    using (var scope = app.Services.CreateScope())
//    {
//        var db = scope.ServiceProvider.GetRequiredService<ApprovalsContext>();
//         db.Database.Migrate(); // Don't do Migration until SeedProvider and API migrations are synced (else this step might break)
//    }
//}

app.Run();
