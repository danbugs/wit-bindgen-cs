using Add;

public class UserAdderImpl : IAdd 
{
    public uint Add(uint a, uint b) 
    {
        return a * b;
    }
}