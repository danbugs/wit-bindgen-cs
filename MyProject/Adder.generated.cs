
// generated file
namespace Add;
using System;
using System.Linq;
using System.Reflection;

public interface IAdd
{
    static abstract uint Add(uint a, uint b);
}

public partial class Adder : IAdd
{
    public static void Main() { }

    public static uint Export_Add(uint a, uint b)
    {
        return Adder.Add(a, b);
    }
}
