using System;
using CommandLine;
using System.IO.Pipes;
using VRWorlds.Browser.Common;
using Serilog;
using System.Collections.Generic;
using System.Text;

namespace BrowserEmissaryProcess
{
    class Program
    {
        public static class ProcessorRoles
        {
            public static readonly Guid DedicatedAvatar = new Guid("195F9EAD-909B-485B-A671-0EC7E97B4CFE");
            public static readonly Guid DedicatedWorld = new Guid("7F334A80-289D-43AF-88F0-18BCC048DFDA");
            public static readonly Guid DedicatedEntity = new Guid("5C1650A4-CBC5-45DC-A8A5-0B33AE2583B4");
            public static readonly Guid MeldedEntity = new Guid("7FDA18F4-E06B-44DE-8E47-BAF82977F7BF");
            public static readonly Guid MeldedAvatar = new Guid("9EE5CB38-A3E3-4438-96E0-4FA4B0553AC4");
            public static readonly Guid Logger = new Guid("0F249026-B336-44A2-B136-7DF1B0840AFE");
        }

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

            [Option("logger-processor", Required = false, HelpText = "")]
            public string LoggerGuid { get; private set; }

            [Option("logger-uri", Required = false, HelpText = "")]
            public string LoggerUri { get; private set; }
        }

        private static Options _workingOptions;
        private static Guid _AccessToken;
        private static Guid _BrowserSession;

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(@"C:\Temp\BrowserEmissaryProcess.log")
                .MinimumLevel.Verbose()
                .CreateLogger();

            var sb = new StringBuilder();
            foreach (var a in args)
            {
                sb.Append(a); sb.Append("  ");
            }

            Log.Information("Starting Up: " + sb.ToString());

            try
            {

                Parser.Default.ParseArguments<Options>(args)
                     .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
                     .WithNotParsed<Options>(errs => HandleParserError(errs));

                // Get our auth token from the environment

                var access_token = Environment.GetEnvironmentVariable("VRWORLDS_GRPC_ACCESS_TOKEN");
                var browser_session = Environment.GetEnvironmentVariable("VRWORLDS_BROWSER_SESSION");

                if (access_token == null)
                {
                    Log.Error("VRWORLDS_GRPC_ACCESS_TOKEN not set");
                    return;
                }

                if (browser_session == null)
                {
                    Log.Error("VRWORLDS_BROWSER_SESSION not set");
                    return;
                }

                _AccessToken = new Guid(access_token);
                _BrowserSession = new Guid(browser_session);

                Log.Information("Access Token: " +_AccessToken.ToString());
                Log.Information("Browser Session: " + _BrowserSession.ToString());

                // Open the GRPC connection to the ingress-uri (which is the browser, or a server)
                // Authenticate

                // Register ourselves

                // Go into processing loop
                // Ping
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Uncaught error in main loop");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void HandleParserError(IEnumerable<Error> errs)
        {
            foreach (var e in errs)
            {
                var name = "";
                var ne = e as NamedError;
                if (ne != null)
                    name = ne.NameInfo.NameText;
                Log.Error("Command Line Parse Error: " + e.Tag.ToString() + " Name: " + name + " (stops: " + e.StopsProcessing.ToString() + ")");
            }
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            _workingOptions = opts;

            Log.Information("Startup Arguments");
            Log.Information("Processor Guid: " + opts.ProcessorGuid.ToString());
            Log.Information("Processor Role: " + opts.ProcessorRole.ToString());
            Log.Information("Ingress URI: " + opts.GrpcIngress);

            
        }
    }
}
