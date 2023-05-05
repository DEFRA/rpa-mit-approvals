using Approvals.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerServices();
builder.Services.AddApprovalsServices();

var app = builder.Build();

app.SwaggerEndpoints();
app.MapHealthCheckGetEndpoints();
app.MapInvoiceApproversGetEndpoints();

app.Run();
