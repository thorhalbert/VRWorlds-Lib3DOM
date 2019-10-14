using System;
using CommandLine;
using System.IO.Pipes;
using VRWorlds.Browser.Common;

namespace BrowserEmissaryProcess
{
    class Program
    {
        public class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }

            [Option("processor-guid", Required = true, HelpText = "")]
            public Guid OurUuid { get; private set; } = Guid.Empty;

            [Option("processor-role", Required = true, HelpText = "")]
            public string Role { get; set; }

            [Option("logger-processor", Required = true, HelpText = "")]
            public string LogPipeName { get; private set; }
            public NamedPipeClientStream LogPipe { get; private set; }
        }



        static void Main(string[] args)
        {

            Parser.Default.ParseArguments<Options>(args);



        }


    }
}
