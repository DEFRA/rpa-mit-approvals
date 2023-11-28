namespace EST.MIT.Approvals.Api;

public static class ConfigurationHelper
{
    public static bool IsLocalDatabase(this IConfiguration c, IConfiguration configuration)
    {
        var host = configuration["POSTGRES_HOST"];
        return string.IsNullOrEmpty(host) || host.ToLower() == "host.docker.internal" || host.ToLower() == "localhost" || host == "127.0.0.1";
    }
}