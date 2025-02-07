namespace AmbientContext;

public struct Point
{
    private int x, y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{nameof(x)} : {x}, {nameof(y)} : {y}";
    }
}