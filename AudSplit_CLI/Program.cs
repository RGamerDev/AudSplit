using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AudSplit_CLI
{
    class Program
    {
        static void Main(string[] args)
        {   
            StringBuilder Intro = new StringBuilder();
            Intro.AppendLine("Welcome to the CLI for Spleeter in the DOTNET environment!")
                 .AppendLine("**********************************************************")
                 .AppendLine("Type in the CLI arguments for Spleeter and I'll do the rest :)");

            Console.WriteLine(Intro);

            ExecuteNativePython(Console.ReadLine());
        }

        private static void ExecuteNativePython(string args)
        {
            //TODO: 1)Create Process Info
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = @"D:\Program files\Python\Scripts\spleeter.exe";

            //TODO: 2)Provide arguments
            psi.Argument = $"\"{args ?? ""}\"\"{}\"";

            //TODO: 3)Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            //TODO: 4)Execute process and get output
            string errors = "";
            string results = "";

            using (Process process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            //TODO: 5)Display output
            Console.WriteLine("ERRORS:");
            Console.WriteLine(errors);
            Console.WriteLine();
            Console.WriteLine("Results:");
            Console.WriteLine(results);
        }

        private static void ExecuteIronPython(string args)
        {
            //TODO: 1)Create ngine
            ScriptEngine engine = Python.CreateEngine();

            //TODO: 2)Provide script and arguments
            string script = "";
            ScriptSource source = engine.CreateScriptSourceFromFile(script);

            engine.GetSysModule().SetVariable("","");

            //TODO: 3)Output redirect
            ScriptIO eIO = engine.Runtime.IO;

            MemoryStream errors = new MemoryStream();
            eIO.SetErrorOutput(errors, Encoding.Default);

            var results = new MemoryStream();
            eIO.SetOutput(results, Encoding.Default);
            //TODO: 4)Execute script
            var scope = engine.CreateScope();
            source.Execute(scope);

            //TODO: 5)Display output
            string str(byte[] x) => Encoding.Default.GetString(x);

            Console.WriteLine("ERRORS:");
            Console.WriteLine(str(errors.ToArray()));
            Console.WriteLine();
            Console.WriteLine("Results:");
            Console.WriteLine(str(results.ToArray()));
        }
    }
}
