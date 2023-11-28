using EST.MIT.Approvals.Data;
using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;

namespace EST.MIT.Approvals.Api.SeedProvider.Provider;

[ExcludeFromCodeCoverage]
public static class SeedProvider
{
    private const string BaseDir = "Resources/SeedData";
    private static readonly string ExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

    public static void SeedReferenceData(ApprovalsContext context, IConfiguration configuration)
    {
        if (configuration.IsLocalDatabase(configuration))
        {
            // If prod allow LiquiBase to perform schema setup.
            context.Database.EnsureCreated();
        }

        context.SeedData(context.Schemes, ReadSeedData<SchemeEntity>($"{BaseDir}/schemes.json"));
    }

    public static void SeedData<T>(this DbContext context, DbSet<T> entity, IEnumerable<T> data)
        where T : class
    {
        if (entity.Any()) return;

        entity.AddRange(data);

        context.SaveChanges();
    }

    private static IEnumerable<T> ReadSeedData<T>(string path)
    {
        var raw = File.ReadAllText(Path.Combine(ExecutionPath, path));

        return JsonSerializer.Deserialize<IEnumerable<T>>(raw)!;
    }
}