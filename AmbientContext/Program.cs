using System.Diagnostics;
using AmbientContext;

var house = new Building();

using (new BuildingContext(3000))
{
    // gnd 3000
    house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
    house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 40000)));

    // 1st 3500
    using (new BuildingContext(3500))
    {
        house.Walls.Add(new Wall(new Point(0, 0), new Point(6000, 0)));
        house.Walls.Add(new Wall(new Point(0, 0), new Point(0, 40000)));
    }

    // 2nd 3000
    house.Walls.Add(new Wall(new Point(0, 0), new Point(5000, 0)));
    house.Walls.Add(new Wall(new Point(5000, 0), new Point(0, 40000)));
}
Console.WriteLine(house.ToString());
