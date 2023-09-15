namespace Add;

public partial class Adder : IAdd
{
    public static uint Add(uint a, uint b)
    {
        return a + b;
    }
}