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