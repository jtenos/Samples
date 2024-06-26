using EfCoreQueryLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

IServiceProvider serviceProvider = new ServiceCollection()
	.AddLogging(builder => builder.AddConsole())
	.AddDbContext<MyDbContext>((sp, options) =>
		options
			.UseLoggerFactory(sp.GetRequiredService<ILoggerFactory>())
			.EnableDetailedErrors()       // <-----------------------
			.EnableSensitiveDataLogging() // <-----------------------
			.UseSqlite("Data Source=mydb.sqlite3")
	).BuildServiceProvider();

using (IServiceScope scope = serviceProvider.CreateScope())
{
	ILogger logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

	logger.LogInformation("Getting MyDbContext:");
	MyDbContext dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

	logger.LogInformation("Ensuring database is deleted:");
	dbContext.Database.EnsureDeleted();
	logger.LogInformation("Ensuring database is created:");
	dbContext.Database.EnsureCreated();
}

using (IServiceScope scope = serviceProvider.CreateScope())
{
	ILogger logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

	logger.LogInformation("Getting MyDbContext:");
	MyDbContext dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

	logger.LogInformation("Creating objects:");
	Foo f1 = new() { Name = "Foo 1" };
	f1.Bars.Add(new() { Name = "Bar 1A" });
	f1.Bars.Add(new() { Name = "Bar 1B" });
	Foo f2 = new() { Name = "Foo 2" };
	f2.Bars.Add(new() { Name = "Bar 2A" });
	f2.Bars.Add(new() { Name = "Bar 2B" });
	dbContext.Foos.AddRange(f1, f2);
	dbContext.SaveChanges();
}

using (IServiceScope scope = serviceProvider.CreateScope())
{
	ILogger logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

	logger.LogInformation("Getting MyDbContext:");
	MyDbContext dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

	logger.LogInformation("Querying objects:");
	List<Bar> bars = (
		from bar in dbContext.Bars.Include(b => b.Foo)
		where bar.Name.EndsWith("A")
		where bar.Foo.Name.StartsWith("Foo 1")
		select bar
	).ToList();

	foreach (Bar bar in bars)
	{
		logger.LogInformation("Bar: {barID}|{barName}|FooID={fooID}", bar.ID, bar.Name, bar.Foo.ID);
	}
}
