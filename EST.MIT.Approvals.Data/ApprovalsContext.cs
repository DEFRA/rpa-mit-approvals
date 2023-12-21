using EST.MIT.Approvals.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace EST.MIT.Approvals.Data;

[ExcludeFromCodeCoverage]
public class ApprovalsContext : DbContext
{
    public DbSet<ApproverEntity> Approvers => Set<ApproverEntity>();
    public DbSet<ApprovalGroupEntity> ApprovalGroups => Set<ApprovalGroupEntity>();
    public DbSet<ApproverApprovalGroupEntity> ApproverAprovalGroups => Set<ApproverApprovalGroupEntity>();

    public ApprovalsContext(DbContextOptions<ApprovalsContext> options) : base(options)
    {
    }

    //private readonly StreamWriter _logStream = new StreamWriter("MIT_Approvals_Seed_SQL.log", append: true);
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        ////optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Name });
        //optionsBuilder.LogTo(_logStream.WriteLine, new[] { DbLoggerCategory.Database.Name });
        //optionsBuilder.LogTo(_logStream.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        //optionsBuilder.EnableSensitiveDataLogging();

        
        optionsBuilder.AddInterceptors(new InsertCommandInterceptor());
}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApproverEntity>()
            .HasMany(e => e.ApprovalGroups)
            .WithMany(e => e.Approvers)
            .UsingEntity<ApproverApprovalGroupEntity>(
                l => l.HasOne<ApprovalGroupEntity>().WithMany().HasForeignKey(e => e.ApprovalGroupId),
                r => r.HasOne<ApproverEntity>().WithMany().HasForeignKey(e => e.ApproverId));
    }

    public class InsertCommandInterceptor : DbCommandInterceptor
    {
        public override DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
        {
            return base.CommandCreated(eventData, result);
        }
        public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        {
            return base.NonQueryExecuted(command, eventData, result);
        }

        public override ValueTask<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            return base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
        }

        public override object? ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object? result)
        {
            return base.ScalarExecuted(command, eventData, result);
        }

        public override ValueTask<object?> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData eventData, object? result, CancellationToken cancellationToken = default)
        {
            return base.ScalarExecutedAsync(command, eventData, result, cancellationToken);
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            var r = base.ReaderExecuted(command, eventData, result);
            if (eventData.Command.CommandText.Contains("INSERT")) {

                var commands = eventData.Command.CommandText.Replace("\r\n"," ").Split(";");
                foreach ( var c in commands)
                {
                    if (c.Contains("INSERT"))
                    {
                        var cr = ReplaceParams(c, command.Parameters);
                        System.Diagnostics.Debug.WriteLine($"==> {cr.Trim()}");
                    }
                }
            }
            return r;
        }

        private string ReplaceParams(string commandText, DbParameterCollection commandParams)
        {
            foreach (DbParameter param in commandParams)
            {
                commandText = commandText.Replace(param.ParameterName, ParamString(param));
            }
            return commandText;
        }

        private string ParamString(DbParameter commandParam)
        {
            switch (commandParam.DbType)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.String:
                    return $"'{commandParam.Value}'";

                case DbType.Date:
                case DbType.DateTime:
                case DbType.DateTime2:
                case DbType.Time:
                    return $"'{commandParam.Value}'";

                default:
                    return $"{commandParam.Value}";
            }
        }
        public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
        {
            return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }
    }
}