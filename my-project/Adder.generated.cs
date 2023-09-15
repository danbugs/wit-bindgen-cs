
// generated file
namespace Add;
using System;
using System.Linq;
using System.Reflection;

public interface IAdd
{
    uint Add(uint a, uint b);
}

public static class AddHelper
{
    private static IAdd _currentImplementation;

    public static void Main() { }

    static AddHelper()
    {
        Type AddType = Assembly.GetExecutingAssembly().GetTypes()
                                .FirstOrDefault(t => t.GetInterface("IAdd") != null && !t.IsInterface && !t.IsAbstract);
        if (AddType != null)
        {
            _currentImplementation = (IAdd)Activator.CreateInstance(AddType);
        }
    }

    public static uint Export_Add(uint a, uint b)
    {
        if (_currentImplementation == null)
            throw new InvalidOperationException("No implementation found for IAdd.");

        return _currentImplementation.Add(a, b);
    }
}
