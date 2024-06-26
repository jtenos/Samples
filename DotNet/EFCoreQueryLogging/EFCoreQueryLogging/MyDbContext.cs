using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfCoreQueryLogging;

public class MyDbContext
	: DbContext
{
	private readonly ILogger _logger;

	public MyDbContext(ILogger<MyDbContext> logger, DbContextOptions<MyDbContext> options)
		: base(options)
	{
		_logger = logger;
		_logger.LogInformation("MyDbContext constructor");
	}

	public DbSet<Foo> Foos { get; set; }
	public DbSet<Bar> Bars { get; set; }
}

public class Foo
{
	public int ID { get; set; }
	public required string Name { get; set; } = default!;

	public ICollection<Bar> Bars { get; set; } = [];
}

public class Bar
{
	public int ID { get; set; }
	public required string Name { get; set; } = default!;

	[Required, ForeignKey("FooID")]
	public Foo Foo { get; set; } = default!;
}
