using Microsoft.Extensions.Caching.Memory;

namespace MiddleWareDemo;

public class TestController
{
    private IMemoryCache memoryCache;

	public TestController(IMemoryCache memoryCache)
	{
		this.memoryCache = memoryCache;
	}

	public Person IncAge(Person p)
	{
		p.Age++;
		return p;
	}

	public object[] Get2(HttpContext context)
	{
		DateTime now = memoryCache.GetOrCreate("Now", e => DateTime.Now);
		var name = context.Request.Query["name"];
		return new object[] { "hello " + name, now};
	}
}