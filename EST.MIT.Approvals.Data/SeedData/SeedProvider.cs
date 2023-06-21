using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using EST.MIT.Approvals.Data.Models;

namespace EST.MIT.Approvals.Data.SeedData;

[ExcludeFromCodeCoverage]
public static class SeedProvider
{
    private const string BaseDir = "Resources/SeedData";
    private static readonly string ExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

    public static void SeedReferenceData(ApprovalsContext context, ILogger logger)
    {
        var sw = Stopwatch.StartNew();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.SeedData(context.Schemes, ReadSeedData<SchemeEntity>($"{BaseDir}/schemes.json"));

        sw.Stop();

        logger.LogInformation("Seeding approvals data completed in {elapsed} seconds", sw.Elapsed.Seconds);
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