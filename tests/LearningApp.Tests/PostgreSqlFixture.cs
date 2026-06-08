using LearningApp.api.Storage;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Testcontainers.PostgreSql;

namespace LearningApp.Tests;

public sealed class PostgreSqlFixture
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder("postgres:16-alpine")
        .Build();

    private NpgsqlDataSource? _dataSource;

    public async Task StartAsync()
    {
        await _postgres.StartAsync();

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(_postgres.GetConnectionString());
        dataSourceBuilder.EnableDynamicJson();
        _dataSource = dataSourceBuilder.Build();

        await using var dbContext = CreateDbContext();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public AppDbContext CreateDbContext()
    {
        if (_dataSource is null)
        {
            throw new InvalidOperationException("PostgreSQL fixture is not initialized.");
        }

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(_dataSource)
            .Options;

        return new AppDbContext(options);
    }

    public async Task ResetDatabaseAsync()
    {
        await using var dbContext = CreateDbContext();

        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Database.ExecuteSqlRawAsync("""
            TRUNCATE TABLE "Lessons", "Courses" RESTART IDENTITY CASCADE;
            """);
    }

    public async Task StopAsync()
    {
        if (_dataSource is not null)
        {
            await _dataSource.DisposeAsync();
        }

        await _postgres.DisposeAsync();
    }
}
