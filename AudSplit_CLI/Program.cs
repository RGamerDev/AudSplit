using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
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
            Console.ReadLine();
        }

        private static void ExecuteNativePython(string args)
        {
            //TODO: 4)Execute process and get output
            using (Process process = Process.Start(new ProcessStartInfo
            {
                //TODO: 1)Create Process Info
                //FileName = @"D:\Program files\Python\Scripts\spleeter.exe",
                FileName = @"spleeter.exe",

                //TODO: 2)Provide arguments
                Arguments = args,

                //TODO: 3)Process configuration
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                RedirectStandardError = true
            }
            ))
            {
                //TODO: 5)Display output
                Console.WriteLine(process.StandardOutput.ReadToEnd());
                Console.WriteLine();
                Console.WriteLine(process.StandardError.ReadToEnd());
            }
        }

        #region Other test environments
        private static void ExecuteIronPython(string args)
        {
            //TODO: 1)Create ngine
            ScriptEngine engine = Python.CreateEngine();

            //TODO: 2)Provide script and arguments
            string script = "";
            ScriptSource source = engine.CreateScriptSourceFromFile(script);

            engine.GetSysModule().SetVariable("", "");

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

        private static string ExecutePowershell(string args)
        {
            InitialSessionState iss = InitialSessionState.CreateDefault();
            Runspace rs = RunspaceFactory.CreateRunspace(iss);
            rs.Open();
            PowerShell ps = PowerShell.Create(rs);
            ps.AddCommand("Get-Command");
            Collection<PSObject> results = ps.Invoke();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (PSObject pSObject in results)
            {
                stringBuilder.AppendLine(pSObject.ToString());
            }

            return stringBuilder.ToString();
        } 
        #endregion
    }
}
