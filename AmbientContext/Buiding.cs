using System.Text;

namespace AmbientContext;

public class Building
{
    public List<Wall> Walls = new();

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var wall in Walls)
            sb.AppendLine(wall.ToString());
        
        return sb.ToString();
    }
}