using System.CommandLine;
using System;
using System.Diagnostics;

var rootCommand = new RootCommand();
var generate = new Command("generate", "generate the bindings for a component");
var witFileOption = new Option<string>
    (name: "--wit",
    description: "path to wit file");
generate.AddOption(witFileOption);

rootCommand.Add(generate);

rootCommand.SetHandler(() =>
{
    Console.WriteLine("Dotnet Component Generator.  Use --help for more information.");
});

generate.SetHandler((witFile) =>
{
    Console.WriteLine($"Generating bindings with file {witFile}");

    try
    {
        using (Process myProcess = new Process())
        {
            myProcess.StartInfo.UseShellExecute = false;
            // You can start any process, HelloWorld is a do-nothing example.
            myProcess.StartInfo.FileName = "wasm-tools";
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.Arguments = $"component wit --json {witFile}";
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.Start();

            myProcess.WaitForExit();
            
            string output = myProcess.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}, witFileOption);

await rootCommand.InvokeAsync(args);
