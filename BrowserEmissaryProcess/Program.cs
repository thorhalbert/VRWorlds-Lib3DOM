using System;
using CommandLine;
using System.IO.Pipes;
using VRWorlds.Browser.Common;
using Serilog;
using System.Collections.Generic;
using System.Text;
using Grpc.Core;
using VRWorlds.Schemas.Browser.Common;
using System.Threading;
using Serilog.Core;
using Serilog.Events;

namespace BrowserEmissaryProcess
{
    class Program
    {
        public enum RoleTypes
        {
            Entity,
            World,
            Avatar
        }

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
            public Guid ProcessorRole { get; set; }

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

        private static RoleTypes EmissaryRole;
        private static bool EmissaryDedicated;
        private static string EmissaryLog;

        class EmissaryIdEnricher : ILogEventEnricher
        {
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("EmissaryId", _workingOptions.ProcessorGuid));
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("EmissaryCode", EmissaryLog));
            }
        }

        static void Main(string[] args)
        {
            // Log stuff up to the point where we know our id, then redo the logging format
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(@"C:\Temp\BrowserEmissaryProcess.log", shared: true)
                .MinimumLevel.Verbose()
                .CreateLogger();

            var sb = new StringBuilder();
            foreach (var a in args)
            {
                sb.Append(a); sb.Append("  ");
            }

            try
            {
                Parser.Default.ParseArguments<Options>(args)
                     .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
                     .WithNotParsed<Options>(errs => HandleParserError(errs));

                Log.CloseAndFlush();

                // Can't do this as a case statement -- irritating
                if (_workingOptions.ProcessorRole.Equals(ProcessorRoles.DedicatedAvatar))
                {
                    EmissaryRole = RoleTypes.Avatar;
                    EmissaryDedicated = true;
                    EmissaryLog = "D-AVATAR";
                }
                if (_workingOptions.ProcessorRole.Equals(ProcessorRoles.DedicatedWorld))
                {
                    EmissaryRole = RoleTypes.World;
                    EmissaryDedicated = true;
                    EmissaryLog = "D-WORLDS";
                }
                if (_workingOptions.ProcessorRole.Equals(ProcessorRoles.DedicatedEntity))
                {
                    EmissaryRole = RoleTypes.Entity;
                    EmissaryDedicated = true;
                    EmissaryLog = "D-ENTITY";
                }
                if (_workingOptions.ProcessorRole.Equals(ProcessorRoles.MeldedEntity))
                {
                    EmissaryRole = RoleTypes.Entity;
                    EmissaryDedicated = false;
                    EmissaryLog = "G-ENTITY";
                }

                // Reestablish the logger with the emissary id
                Log.Logger = new LoggerConfiguration()
                  .Enrich.With(new EmissaryIdEnricher())
                  .WriteTo.File(@"C:\Temp\BrowserEmissaryProcess.log", outputTemplate: "[{Timestamp:HH:mm:ss} {EmissaryId}-{EmissaryCode} {Level:u3}] {Message:lj}{NewLine}{Exception}", shared: true)
                  .MinimumLevel.Verbose()
                  .CreateLogger();

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

                Log.Information("Access Token: " + _AccessToken.ToString());
                Log.Information("Browser Session: " + _BrowserSession.ToString());

                // Open the GRPC connection to the ingress-uri (which is the browser, or a server)
                // Authenticate

                AttachToBrowser();

                // Register ourselves

                // Go into processing loop
                // Ping

                PingLoop();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Uncaught error in main loop - started as: "+sb.ToString());
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

        private static Channel _GRPCChannel;

        private static void AttachToBrowser()
        {
            // Ultimatley we want to use the shared token in ACCESS_TOKEN for authentication
            // Likely not how this works at all
            //var creds = ChannelCredentials.Create()

            _GRPCChannel = new Channel(_workingOptions.GrpcIngress, ChannelCredentials.Insecure);

        }

        private static void PingLoop()
        {
            var ping = new Ping.PingClient(_GRPCChannel);

            while (true)
            {
                var bef = DateTime.Now.Ticks;
                var reply = ping.Ping(new PingRequest());
                var aft = DateTime.Now.Ticks;
                
                Log.Information("Ping: ("+(aft-bef).ToString()+" ns)");

                Thread.Sleep(60000);
            }
        }
    }
}
