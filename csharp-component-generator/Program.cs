using System.CommandLine;
using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;

// function that takes JSON output and generates C# code
// - for now, we'll parse interfaces – anything else is out of scope.
string jsonToCSharp(string json)
{
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    // go through json, only consider "interfaces".
    var jsonObj = JsonSerializer.Deserialize<Root>(json, options);

    // for each interface, convert types to C# types.
    var interface0 = jsonObj.Interfaces[0];
    var function0 = interface0.Functions.GetValueOrDefault("add");
    var result0 = function0.Results[0];

    var convertedTemplate = 
$@"
namespace {interface0.Name};
using System;
using System.Linq;
using System.Reflection;

public interface I{interface0.Name}
{{
    {witToCSharpType(result0.Type)} {function0.Name}({witToCSharpType(function0.Params[0].Type)} {function0.Params[0].Name}, {witToCSharpType(function0.Params[1].Type)} {function0.Params[1].Name});
}}

public static class {interface0.Name}Helper
{{
    private static I{interface0.Name} _currentImplementation;

    public static void Main() {{ }}

    static {interface0.Name}Helper()
    {{
        Type {interface0.Name}Type = Assembly.GetExecutingAssembly().GetTypes()
                                .FirstOrDefault(t => t.GetInterface(""I{interface0.Name}"") != null && !t.IsInterface && !t.IsAbstract);
        if ({interface0.Name}Type != null)
        {{
            _currentImplementation = (I{interface0.Name})Activator.CreateInstance({interface0.Name}Type);
        }}
    }}

    public static {witToCSharpType(result0.Type)} Export_{function0.Name}({witToCSharpType(function0.Params[0].Type)} {function0.Params[0].Name}, {witToCSharpType(function0.Params[1].Type)} {function0.Params[1].Name})
    {{
        if (_currentImplementation == null)
            throw new InvalidOperationException(""No implementation found for I{interface0.Name}."");

        return _currentImplementation.{function0.Name}({function0.Params[0].Name}, {function0.Params[1].Name});
    }}
}}
";

    return convertedTemplate;
}

// function that takes JSON output and generates C code
string jsonToC(string json, string worldName, string projectName)
{
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    // go through json, only consider "interfaces".
    var jsonObj = JsonSerializer.Deserialize<Root>(json, options);

    // for each interface, convert types to C# types.
    var interface0 = jsonObj.Interfaces[0];
    var function0 = interface0.Functions.GetValueOrDefault("add");
    var result0 = function0.Results[0];

    // package name will look like: "docs:calculator@0.1.0", need to get it as "docs_calculator"
    var packageName = jsonObj.Packages[0].Name;
    Console.WriteLine(packageName);
    var packageNameSplit = packageName.Split(":");
    Console.WriteLine(packageNameSplit);
    var packageNameSplit1 = packageNameSplit[1].Split("@");
    Console.WriteLine(packageNameSplit1);
    var packageNameSplit2 = packageNameSplit[0] + "_" + packageNameSplit1[0];
    Console.WriteLine(packageNameSplit2);
    
    var convertedTemplate =
$@"
#include <wasm/driver.h>
#include ""{worldName}.h""
#include <assert.h>


void mono_wasm_invoke_method_ref(MonoMethod* method, MonoObject** this_arg_in, void* params[], MonoObject** _out_exc, MonoObject** out_result);

int dotnet_started = 0;
void _start();
void ensure_dotnet_started() {{
    if (!dotnet_started) {{
        _start();
        dotnet_started = 1;
    }}
}}

{witToCType(result0.Type)} exports_{packageNameSplit2}_{interface0.Name}_{function0.Name}({witToCType(function0.Params[0].Type)} {function0.Params[0].Name}, {witToCType(function0.Params[1].Type)} {function0.Params[1].Name})
{{
    ensure_dotnet_started();

	MonoMethod* method = lookup_dotnet_method(""{projectName}"", ""{interface0.Name}"", ""{interface0.Name}Helper"", ""Export_{function0.Name}"", -1);
    void* method_params[] = {{ &{function0.Params[0].Name}, &{function0.Params[1].Name} }};
    MonoObject* exception;
    MonoObject* result;
    mono_wasm_invoke_method_ref(method, NULL, method_params, &exception, &result);	
    assert(!exception);

	{witToCType(result0.Type)} res = *({witToCType(result0.Type)}*)mono_object_unbox(result);
    return res;
}}
";

    return convertedTemplate;
}


// function to convert wit type to C# type
string witToCSharpType(string witType)
{
    // wit types:
    // bool
    // s8, s16, s32, s64
    // u8, u16, u32, u64
    // float32, float64
    // char
    // string

    switch (witType)
    {
        case "bool":
            return "bool";
        case "s8":
            return "sbyte";
        case "s16":
            return "short";
        case "s32":
            return "int";
        case "s64":
            return "long";
        case "u8":
            return "byte";
        case "u16":
            return "ushort";
        case "u32":
            return "uint";
        case "u64":
            return "ulong";
        case "float32":
            return "float";
        case "float64":
            return "double";
        case "char":
            return "char";
        case "string":
            return "string";
    }

    return "";
}

// function to convert wit type to C type
string witToCType(string witType)
{
    switch (witType)
    {
        case "bool":
            return "bool";
        case "s8":
            return "int8_t";
        case "s16":
            return "int16_t";
        case "s32":
            return "int32_t";
        case "s64":
            return "int64_t";
        case "u8":
            return "uint8_t";
        case "u16":
            return "uint16_t";
        case "u32":
            return "uint32_t";
        case "u64":
            return "uint64_t";
        case "float32":
            return "float";
        case "float64":
            return "double";
        case "char":
            return "char";
        case "string":
            return "char*";
    }

    return "";
}

var rootCommand = new RootCommand();
var generate = new Command("generate", "generate the bindings for a component");
var witFileOption = new Option<string>
    (name: "--wit",
    description: "path to wit file");
var projectNameOption = new Option<string>
    (name: "--project",
    description: "name of project");
generate.AddOption(witFileOption);
generate.AddOption(projectNameOption);

rootCommand.Add(generate);

rootCommand.SetHandler(() =>
{
    Console.WriteLine("Dotnet Component Generator.  Use --help for more information.");
});

generate.SetHandler((witFile, projectName) =>
{
    Console.WriteLine($"Generating bindings with file {witFile}");

    // get wit file name at the end of path
    var witFileName = witFile.Split("/").Last();

    try
    {
        using (Process myProcess = new Process())
        {
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "wasm-tools";
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.Arguments = $"component wit --json {witFile}";
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.Start();
            myProcess.WaitForExit();

            string json = myProcess.StandardOutput.ReadToEnd();
            
            // generate csharp
            var csharp = jsonToCSharp(json);
            System.IO.File.WriteAllText(@"Generated.cs", csharp);

            // get world name from wit file name
            var worldName = witFileName.Split(".")[0];

            // run command: wit-bindgen c ../tutorial/wit/calculator.wit --world {worldName}
            myProcess.StartInfo.FileName = "wit-bindgen";
            myProcess.StartInfo.Arguments = $"c ../tutorial/wit/calculator.wit --world {worldName}";
            myProcess.Start();
            myProcess.WaitForExit();

            // generate c
            var c = jsonToC(json, worldName, projectName);
            System.IO.File.WriteAllText(@"generated.c", c);

        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}, witFileOption, projectNameOption);

await rootCommand.InvokeAsync(args);