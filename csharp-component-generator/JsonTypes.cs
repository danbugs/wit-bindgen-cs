public class Param
{
    public required string Name { get; set; }
    public required string Type { get; set; }
}

public class Result
{
    public required string Type { get; set; }
}


public class Function {
    public required string Name { get; set; }
    public required List<Param> Params { get; set; }
    public required List<Result> Results { get; set; }
}

public class Interface
{
    public required string Name { get; set; }
    public required Dictionary<string, Function> Functions { get; set; }
}

public class Package
{
    public required string Name { get; set; }
}

public class Root
{
    public required List<Interface> Interfaces { get; set; }
    public required List<Package> Packages { get; set; }
}