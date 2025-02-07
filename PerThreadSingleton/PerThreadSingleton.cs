namespace PerThreadSingleton;

public sealed class PerThreadSingletonClass
{
    private static ThreadLocal<PerThreadSingletonClass> _threadInstance
        = new ThreadLocal<PerThreadSingletonClass>(
            () => new PerThreadSingletonClass());

    public int Id;
    private PerThreadSingletonClass()
    {
        Id = Thread.CurrentThread.ManagedThreadId;
    }
    
    public static PerThreadSingletonClass Instance => _threadInstance.Value;
}