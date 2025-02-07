
using PerThreadSingleton;

var t1 = Task.Factory.StartNew(() =>
{
    Console.WriteLine("t1:" + PerThreadSingletonClass.Instance.Id);
});

var t2 = Task.Factory.StartNew(() =>
{
    Console.WriteLine("t1:" + PerThreadSingletonClass.Instance.Id);
    Console.WriteLine("t2:" + PerThreadSingletonClass.Instance.Id);
});

Task.WaitAll(t1, t2);