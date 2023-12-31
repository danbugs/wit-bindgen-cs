using System.Diagnostics;

public static class ComponentBuilder
{
    public static Action<string, string, string> Command()
    {
        return (world, witFile, previewfile) =>
        {
            Console.WriteLine("Building component");

            Console.WriteLine("Building dotnet wasm");
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = "dotnet";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.EnvironmentVariables["DOTNET_ROOT"] = "$HOME/dotnet";
                    myProcess.StartInfo.Arguments = "build";
                    myProcess.Start();
                    myProcess.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            
            var files = Directory.GetFiles(".", "*.csproj", SearchOption.TopDirectoryOnly);
            var projectName = files.First().Split("/").Last().Split(".").First();
            Console.WriteLine($"Embedding world {world} from {witFile} {projectName}.wasm");
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = "wasm-tools";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.Arguments = $"component embed --world {world} {witFile} bin/Debug/net8.0/wasi-wasm/AppBundle/{projectName}.wasm -o main.embed.wasm";
                    myProcess.Start();

                    myProcess.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Building component");
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = "wasm-tools";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.Arguments = $"component new main.embed.wasm --adapt {previewfile} -o main.component.wasm";
                    myProcess.Start();

                    myProcess.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        };
    }
}