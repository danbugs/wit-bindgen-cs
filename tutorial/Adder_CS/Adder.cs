// <generated>
namespace Adder_CS;

using System;
using System.Linq;
using System.Reflection;

public interface IAdder 
{
    uint Add(uint a, uint b);
}

public static class AdderHelper
{
    private static IAdder _currentImplementation;

    public static void Main() { }

    static AdderHelper()
    {
        Type adderType = Assembly.GetExecutingAssembly().GetTypes()
                                  .FirstOrDefault(t => t.GetInterface("IAdder") != null && !t.IsInterface && !t.IsAbstract);

        if (adderType != null)
        {
            _currentImplementation = (IAdder)Activator.CreateInstance(adderType);
        }
    }

    public static uint Export_Add(uint a, uint b)
    {
        if (_currentImplementation == null)
            throw new InvalidOperationException("No implementation found for IAdder.");

        return _currentImplementation.Add(a, b);
    }
}
// </generated>

public class UserAdderImpl : IAdder 
{
    public uint Add(uint a, uint b) 
    {
        return a + b;
    }
}