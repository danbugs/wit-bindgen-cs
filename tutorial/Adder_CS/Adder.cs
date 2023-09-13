using System.Runtime.CompilerServices;

namespace Adder_CS;

public class Adder
{
    private static Func<uint, uint, uint> _adder;
    public static void AdderFunc(Func<uint, uint, uint> adder){
        _adder = adder;
    }

    private static uint Export_Adder(uint a, uint b) {
        return _adder(a, b);
    }
}