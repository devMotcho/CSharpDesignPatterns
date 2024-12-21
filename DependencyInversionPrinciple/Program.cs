namespace DesginPatterns
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibiling
    }

    public class Person
    {
        public string Name = string.Empty;
    }

    public interface IRelationShipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    public class Relationships : IRelationShipBrowser
    {
        private readonly List<(Person, Relationship, Person)> relations 
        = new List<(Person, Relationship, Person)>();
        
        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add ((parent, Relationship.Parent, child));
            relations.Add ((child, Relationship.Parent, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations
            .Where(x => x.Item1.Name == name 
            && x.Item2 == Relationship.Parent)
            .Select(x => x.Item3);
        }

        // public List<(Person, Relationship, Person)> Relations => relations;
    }
    public class Research
    {
        public Research(IRelationShipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            System.Console.WriteLine($"John has a child called {p.Name}");
        }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }

    }

}

