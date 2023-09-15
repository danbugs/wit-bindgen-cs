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
                    myProcess.StartInfo.FileName = "dotnet";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.EnvironmentVariables["DOTNET_ROOT"] = "$HOME/dotnet";
                    myProcess.StartInfo.Arguments = "workload install wasi-experimental";
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
                    myProcess.StartInfo.Arguments = "install --git https://github.com/bytecodealliance/wit-bindgen wit-bindgen-cli";
                    myProcess.Start();

                    myProcess.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Installing wasm-tools");
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = "cargo";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.Arguments = "install wasm-tools --git https://github.com/bytecodealliance/wasm-tools --branch main";
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

            System.IO.File.WriteAllText(files.First(), csprojXml.ToString());
        };
    } 
}