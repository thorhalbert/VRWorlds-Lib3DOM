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
            public Guid ProcessorGuid { get; private set; } = Guid.Empty;

            [Option("processor-role", Required = true, HelpText = "")]
            public string ProcessorRole { get; set; }

            [Option("grpc-ingress-uri", Required = true, HelpText = "")]
            public string GrpcIngress { get; set; }

            [Option("logger-processor", Required = true, HelpText = "")]
            public string LoggerGuid { get; private set; }

            [Option("logger-uri", Required = true, HelpText = "")]
            public string LoggerUri { get; private set; }
        }

        static void Main(string[] args)
        {

            Parser.Default.ParseArguments<Options>(args);

            // Get our auth token from the environment

            // Open the GRPC connection to the ingress-uri (which is the browser, or a server)
            // Authenticate

            // Register ourselves

            // Go into processing loop
                // Ping
        }


    }
}
