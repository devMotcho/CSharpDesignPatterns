using MoreLinq;

namespace DesignPatterns;

public class SingletonDatabase : IDataBase
{
        
    private Dictionary<string, int> capitals;
    private static int instanceCount;
    public static int Count => instanceCount;
    private SingletonDatabase()
    {
        instanceCount++;
        Console.WriteLine("Initializing Database.");

        capitals = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(IDataBase).Assembly.Location).DirectoryName,
                    "capitals.txt")
            )
            .Batch(2)
            .ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1).Trim())
            );
    }

    public int GetPopulation(string name)
    {
        return capitals[name];
    }

    // Initialize and expose a single instance of the database
    private static Lazy<SingletonDatabase> _instance = 
        new Lazy<SingletonDatabase>(() => new SingletonDatabase());
    public static SingletonDatabase Instance => _instance.Value;
}

public class SingleRecordFinder
{
    public int GetTotalPopulation(IEnumerable<string> names)
    {
        int result = 0;
        foreach (var name in names)
        {
            result += SingletonDatabase.Instance.GetPopulation(name);
        }

        return result;
    }
}

public class DummyDatabase : IDataBase
{
    public int GetPopulation(string name)
    {
        return new Dictionary<string, int>
        {
            ["alpha"] = 1,
            ["beta"] = 2,
            ["gamma"] = 3
        }[name];
    }
}

public class ConfigurableRecordFinder
{
    private IDataBase database;

    public ConfigurableRecordFinder(IDataBase database)
    {
        this.database = database;
    }
    
    public int GetTotalPopulation(IEnumerable<string> names)
    {
        int result = 0;
        foreach (var name in names)
        {
            result += database.GetPopulation(name);
        }

        return result;
    }
}

public class OrdinaryDatabase : IDataBase
{
    private Dictionary<string, int> capitals;
    private OrdinaryDatabase()
    {
        Console.WriteLine("Initializing Database.");

        capitals = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(IDataBase).Assembly.Location).DirectoryName,
                    "capitals.txt")
            )
            .Batch(2)
            .ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1).Trim())
            );
    }

    public int GetPopulation(string name)
    {
        return capitals[name];
    }
}