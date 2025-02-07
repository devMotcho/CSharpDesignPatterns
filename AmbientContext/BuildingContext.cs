namespace AmbientContext;

public class BuildingContext : IDisposable
{
    public int WallHeight;
    private static Stack<BuildingContext> _stack = new();

    static BuildingContext()
    {
        _stack.Push(new BuildingContext(0));
    }
    
    public static BuildingContext Current => _stack.Peek();

    public BuildingContext(int wallHeight)
    {
        WallHeight = wallHeight;
        _stack.Push(this);
    }

    public void Dispose()
    {
        if (_stack.Count > 1)
            _stack.Pop();
    }
}