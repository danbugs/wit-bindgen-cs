using System.Diagnostics;
using System.Xml.Linq;

public static class InitProject {
    public static Action Command()
    {
        return () =>
        {
            Console.WriteLine("Initializing project");

            Console.WriteLine("Installing dotnet wasi-experimental tools");
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.CreateNoWindow = true;
                    var dotnetRoot = Environment.GetEnvironmentVariable("DOTNET_ROOT");
                    if (dotnetRoot == null) {
                        dotnetRoot = "$HOME/dotnet";
                    }
                    myProcess.StartInfo.EnvironmentVariables["DOTNET_ROOT"] = dotnetRoot;
                    myProcess.StartInfo.FileName = "dotnet";
                    myProcess.StartInfo.Arguments = "workload install wasi-experimental";
                    if (dotnetRoot.StartsWith("/usr")) {
                        myProcess.StartInfo.Arguments = myProcess.StartInfo.FileName + " " + myProcess.StartInfo.Arguments;
                        myProcess.StartInfo.FileName = "sudo";
                    }
                    myProcess.Start();
                    myProcess.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("Installing wit-bindgen-cli");
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = "cargo";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.Arguments = "install --git https://github.com/bytecodealliance/wit-bindgen wit-bindgen-cli  --rev 15907098ddfb8332359d614bb391cbc0d9dd19e9";
                    myProcess.Start();

                    myProcess.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Installing wasm-tools");
            // requires https://github.com/bytecodealliance/wasm-tools/pull/1203 so pinned to a specific commit
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = "cargo";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.Arguments = "install wasm-tools --git https://github.com/bytecodealliance/wasm-tools --rev 49753602683a539b66d0a65ffa11acb402f148bb";
                    myProcess.Start();

                    myProcess.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Creating build Target");
            // create folder with xml in it
            System.IO.Directory.CreateDirectory("build");
            var target =
$@"
<Project>
	<ItemGroup>
		<_WasmNativeFileForLinking Include=""$(MSBuildThisFileDirectory)\..\native\*.c"" />
        <_WasmNativeFileForLinking Include=""$(MSBuildThisFileDirectory)\..\native\wit\*.c"" />
		<_WasiSdkClangArgs Include=""-I&quot;$([System.String]::Copy('$(MSBuildThisFileDirectory)..\native\wit\').Replace('\','/'))&quot;"" /> 
	</ItemGroup>
</Project>
";
            System.IO.File.WriteAllText(@"build/wasm.targets", target);


            Console.WriteLine("Updating .csproj for wasi");
            // read file *.csproj as xml and add <Import Project="build\adder.targets"/>
            var files = Directory.GetFiles(".", "*.csproj", SearchOption.TopDirectoryOnly);
            var csproj = System.IO.File.ReadAllText(files.First());
            var csprojXml = XElement.Parse(csproj);
            csprojXml.Add(new XElement("Import", new XAttribute("Project", @"build\wasm.targets")));
            var propertyGroup = csprojXml.Elements("PropertyGroup").First();
            propertyGroup.Add(new XElement("RuntimeIdentifier", "wasi-wasm"));
            propertyGroup.Add(new XElement("WasmSingleFileBundle", "true"));
            propertyGroup.Add(new XElement("PublishTrimmed", "true"));

            System.IO.File.WriteAllText(files.First(), csprojXml.ToString());
        };
    } 
}