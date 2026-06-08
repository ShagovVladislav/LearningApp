using NUnit.Framework;

namespace LearningApp.Tests;

[SetUpFixture]
public sealed class PostgreSqlTestEnvironment
{
    public static PostgreSqlFixture Fixture { get; } = new();

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        await Fixture.StartAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await Fixture.StopAsync();
    }
}
