using Approvals.Api.Endpoints;
using EST.MIT.Approvals.Api.endpoints;
using EST.MIT.Approvals.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerServices();
builder.Services.AddApprovalsServices();

var app = builder.Build();

app.SwaggerEndpoints();
app.MapHealthCheckGetEndpoints();
app.MapInvoiceApproversGetEndpoints();

app.Run();
