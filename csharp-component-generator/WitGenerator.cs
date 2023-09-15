using System.Diagnostics;
using System.Text.Json;

public static class WitGenerator {
    
public static Action<string> Command()
{
    return (witFile) =>
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

                // get world name from wit file name
                var worldName = witFileName.Split(".")[0];

                // generate csharp
                var csharp = jsonToCSharp(json, worldName);
                System.IO.File.WriteAllText($"{worldName.ToUpperFirstLetter()}.generated.cs", csharp);

                // run command: wit-bindgen c <wit-path> --world {worldName}
                myProcess.StartInfo.FileName = "wit-bindgen";
                myProcess.StartInfo.Arguments = $"c {witFile} --world {worldName} --out-dir native/wit";
                myProcess.Start();
                myProcess.WaitForExit();

                // generate c
                var files = Directory.GetFiles(".", "*.csproj", SearchOption.TopDirectoryOnly);
                var projectName = files.First().Split("/").Last().Split(".").First();
                var c = jsonToC(json, worldName, projectName);
                System.IO.Directory.CreateDirectory("native");
                System.IO.File.WriteAllText($"native/{worldName}.generated.c", c);

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    };
}

// function that takes JSON output and generates C# code
// - for now, we'll parse interfaces – anything else is out of scope.
private static string jsonToCSharp(string json, string worldName)
{
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    var jsonObj = JsonSerializer.Deserialize<Root>(json, options);

    var interface0 = jsonObj.Interfaces[0];
    var function0 = interface0.Functions.GetValueOrDefault("add");
    var result0 = function0.Results[0];

    var interfaceName = interface0.Name.ToUpperFirstLetter();
    var functionName = function0.Name.ToUpperFirstLetter();
    var convertedTemplate =
$@"
// generated file
namespace {interfaceName};
using System;
using System.Linq;
using System.Reflection;

public interface I{interfaceName}
{{
    static abstract {witToCSharpType(result0.Type)} {functionName}({witToCSharpType(function0.Params[0].Type)} {function0.Params[0].Name}, {witToCSharpType(function0.Params[1].Type)} {function0.Params[1].Name});
}}

public partial class {worldName.ToUpperFirstLetter()} : I{interfaceName}
{{
    public static void Main() {{ }}

    public static {witToCSharpType(result0.Type)} Export_{functionName}({witToCSharpType(function0.Params[0].Type)} {function0.Params[0].Name}, {witToCSharpType(function0.Params[1].Type)} {function0.Params[1].Name})
    {{
        return {worldName.ToUpperFirstLetter()}.{functionName}({function0.Params[0].Name}, {function0.Params[1].Name});
    }}
}}
";

    return convertedTemplate;
}

// function that takes JSON output and generates C code
private static string jsonToC(string json, string worldName, string projectName)
{
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };

    var jsonObj = JsonSerializer.Deserialize<Root>(json, options);

    var interface0 = jsonObj.Interfaces[0];
    var function0 = interface0.Functions.GetValueOrDefault("add");
    var result0 = function0.Results[0];

    // package name will look like: "docs:calculator@0.1.0", need to get it as "docs_calculator"
    var packageName = jsonObj.Packages[0].Name;
    var packageNameSplit = packageName.Split(":");
    var packageNameSplit1 = packageNameSplit[1].Split("@");
    var packageNameSplit2 = packageNameSplit[0] + "_" + packageNameSplit1[0];

    var interfaceName = interface0.Name.ToUpperFirstLetter();
    var functionName = function0.Name.ToUpperFirstLetter();
    var convertedTemplate =
$@"
// generated file
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

	MonoMethod* method = lookup_dotnet_method(""{projectName}"", ""{interfaceName}"", ""{worldName.ToUpperFirstLetter()}"", ""Export_{functionName}"", -1);
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
private static string witToCSharpType(string witType)
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
private static string witToCType(string witType){
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

public static string ToUpperFirstLetter(this string source)
{
    if (string.IsNullOrEmpty(source))
        return string.Empty;
    // convert to char array of the string
    char[] letters = source.ToCharArray();
    // upper case the first char
    letters[0] = char.ToUpper(letters[0]);
    // return the array made of the new char array
    return new string(letters);
}
}

