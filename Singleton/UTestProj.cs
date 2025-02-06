using Autofac;
using DesignPatterns;
using NUnit.Framework;

namespace Testing;

[TestFixture]
public class UTestProj
{
    [Test]
    public void IsSingletonTest()
    {
        var db = SingletonDatabase.Instance;
        var db2 = SingletonDatabase.Instance;
        Assert.That(db, Is.SameAs(db2));
        Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
    }

    [Test]
    public void SingletonTotalPopulationTest()
    {
        var rf = new SingleRecordFinder();
        var names = new[] { "Tokyo", "London" };
        int tp = rf.GetTotalPopulation(names);
        Assert.That(tp, Is.EqualTo(37393000 + 9177000));
    }

    [Test]
    public void ConfigurablePopulationTest()
    {
        var rf = new ConfigurableRecordFinder(new DummyDatabase());
        var names = new[] { "alpha", "gamma" };
        var tp = rf.GetTotalPopulation(names);
        Assert.That(tp, Is.EqualTo(4));
    }

    [Test]
    public void DIPopulationTest()
    {
        var cb = new ContainerBuilder();
        cb.RegisterType<OrdinaryDatabase>()
            .As<IDataBase>()
            .SingleInstance();
        cb.RegisterType<ConfigurableRecordFinder>();
        
        using (var c = cb.Build())
        {
            var rf = c.Resolve<ConfigurableRecordFinder>();
        }
    }
}