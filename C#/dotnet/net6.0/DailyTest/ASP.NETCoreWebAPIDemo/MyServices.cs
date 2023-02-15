namespace ASP.NETCoreWebAPIDemo;

public class MyServices
{
    public int Minus(int i, int j)
    {
        return i - j;
    }
}

public class LongTimeServices
{
    public void DelayAction()
    {
        Thread.Sleep(10000);
    }
}

public record Book(long Id, string Name);

public class MyDbContext
{
    public static Task<Book?> GetBookByIdAsync(long id)
    {
        var result = GetById(id);
        return Task.FromResult(result);
    }

    public static Book? GetById(long id)
    {
        switch (id)
        {
            case 1:
                return new Book(1, "零基础趣学C语言");
            case 2:
                return new Book(2, "J2EE开发全程实录");
            case 3:
                return new Book(3, "程序员的SQL金典");
            default:
                return null;
        }
    }
}