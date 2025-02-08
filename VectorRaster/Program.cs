﻿using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;
namespace DesignPatterns;

public class Point
{
    public int X, Y;
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Point point &&
               X == point.X &&
               Y == point.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}

public class Line
{
    public Point Start, End;

    public Line(Point start, Point end)
    {
        Start = start;
        End = end;
    }

    public override bool Equals(object? obj)
    {
        return obj is Line line &&
               EqualityComparer<Point>.Default.Equals(Start, line.Start) &&
               EqualityComparer<Point>.Default.Equals(End, line.End);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }
}

public class VectorObject : Collection<Line>
{

}

public class VectorRectangle : VectorObject
{
    public VectorRectangle(int x, int y, int width, int height)
    {
        Add(new Line(new Point(x, y), new Point(x + width, y)));
        Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
        Add(new Line(new Point(x, y), new Point(x, y + height)));
        Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
    }
}

public class LinewToPointAdapter : IEnumerable<Point>
{
    private static int count;
    static Dictionary<int, List<Point>> cache = new (); // key is the has code

    public LinewToPointAdapter(Line line)
    {
        var hash = line.GetHashCode();
        if (cache.ContainsKey(hash)) return;

        Console.WriteLine($"{++count}: Generating points for line [{line.Start.X},{line.Start.Y}] - [{line.End.X},{line.End.Y}]");

        var points = new List<Point>();

        int left = Math.Min(line.Start.X, line.End.X);
        int right = Math.Max(line.Start.X, line.End.X);
        int top = Math.Min(line.Start.Y, line.End.Y);
        int bottom = Math.Max(line.Start.Y, line.End.Y);
        int dx = right - left;
        int dy = line.End.Y - line.Start.Y;

        if (dx == 0)
        {
            for (int y = top; y <= bottom; ++y)
            {
                points.Add(new Point(left, y));
            }
        }
        else if (dy == 0)
        {
            for (int x = left; x <= right; ++x)
            {
                points.Add(new Point(x, top));
            }

        }

        cache.Add(hash, points);

    }

    public IEnumerator<Point> GetEnumerator()
    {
        return cache.Values.SelectMany(x => x).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

class Demo
{
    private static readonly List<VectorObject> vectorObjects 
        = new List<VectorObject>
    {
        new VectorRectangle(1,1, 10, 10),
        new VectorRectangle(3,3, 6, 6),
    };

    public static void DrawPoint(Point p)
    {
        Console.Write(".");
    }

    static void Main(string[] args)
    {
        Draw();
        Draw();

    }

    private static void Draw()
    {
        foreach (var vo in vectorObjects)
        {
            foreach (var line in vo)
            {
                var adapter = new LinewToPointAdapter(line);
                adapter.ToList().ForEach(DrawPoint);
            }
        }
    }
}