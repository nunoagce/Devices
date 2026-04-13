using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Application;

namespace Application.Tests;

public abstract class TestBase
{
    protected IMediator Mediator { get; private set; }
    private AppDbContext _Context;
    private ServiceProvider _serviceProvider;

    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("DataSource=:memory:"));

        services.AddLogging();
        services.AddApplication();
        services.AddInfrastructureForTests();

        _serviceProvider = services.BuildServiceProvider();

        Mediator = _serviceProvider.GetRequiredService<IMediator>();
        _Context = _serviceProvider.GetRequiredService<AppDbContext>();

        _Context.Database.OpenConnection();
        _Context.Database.EnsureCreated();
    }

    [TearDown]
    public void TearDown()
    {
        _Context.Database.CloseConnection();
        _Context.Dispose();
        _serviceProvider.Dispose();
    }
}