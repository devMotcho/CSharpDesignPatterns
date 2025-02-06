using DesignPatterns;
using static System.Console;

var db = SingletonDatabase.Instance;
var city = "New Delhi";
WriteLine($"{city} has population {db.GetPopulation(city)}");

