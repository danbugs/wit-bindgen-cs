
namespace add;
using System;
using System.Linq;
using System.Reflection;

public interface Iadd
{
    uint add(uint a, uint b);
}

public static class addHelper
{
    private static Iadd _currentImplementation;

    public static void Main() { }

    static addHelper()
    {
        Type addType = Assembly.GetExecutingAssembly().GetTypes()
                                .FirstOrDefault(t => t.GetInterface("Iadd") != null && !t.IsInterface && !t.IsAbstract);
        if (addType != null)
        {
            _currentImplementation = (Iadd)Activator.CreateInstance(addType);
        }
    }

    public static uint Export_add(uint a, uint b)
    {
        if (_currentImplementation == null)
            throw new InvalidOperationException("No implementation found for Iadd.");

        return _currentImplementation.add(a, b);
    }
}
