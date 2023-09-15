using System.CommandLine;

var rootCommand = new RootCommand();
rootCommand.SetHandler(() =>
{
    Console.WriteLine("Dotnet Component Generator.  Use --help for more information.");
});

// Generate command and options
var generate = new Command("generate", "generate the bindings for a component");
var witFileOption = new Option<string>
    (name: "--wit",
    description: "path to wit file");
generate.AddOption(witFileOption);
rootCommand.Add(generate);
generate.SetHandler(WitGenerator.Command(), witFileOption);

// Build command and options
var build = new Command("build", "build a component");
var worldOption = new Option<string>
    (name: "--world",
    description: "world");
var previewOption = new Option<string>
    (name: "--preview",
    description: "preview");
build.AddOption(witFileOption);
build.AddOption(worldOption);
build.AddOption(previewOption);
rootCommand.Add(build);
build.SetHandler(ComponentBuilder.Command(), worldOption, witFileOption, previewOption);

// Init command and options
var init = new Command("init", "initialize a project");
rootCommand.Add(init);
init.SetHandler(InitProject.Command());

await rootCommand.InvokeAsync(args);
