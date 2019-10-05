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

            [Option("uuid", Required = true, HelpText = "")]
            public  Guid OurUuid { get; private set; } = Guid.Empty;

            [Option("role", Required =true, HelpText ="")]
            public string Role { get; set;}

            [Option("pipename", Required = true, HelpText ="")]
            public  string PipeName { get; private set; }
            public  NamedPipeClientStream Pipe { get; private set; }

            [Option("logpipe", Required = true, HelpText = "")]
            public  string LogPipeName { get; private set; }
            public  NamedPipeClientStream LogPipe { get; private set; }
        }

      

        static void Main(string[] args)
        {

            Parser.Default.ParseArguments<Options>(args);



            //if (OurUuid.Equals(Guid.Empty))
            //    Logger.Assert(new Guid("2FB64CE1-9661-45F1-8A6D-E0420809D9BC"),"UUID Not Assigned");

            //if (Pipe == null)
            //    Logger.Assert(new Guid("64857011-3BF5-4218-9BBE-E4EE8711D5FB"),"Pipe Not Assigned");

            //if (LogPipe == null)
            //    Logger.Assert(new Guid("DAB01626-B125-4D17-AD9D-3EF60BF7778F"),"Log Pipe Not Assigned");
        }

        private static void procIdent(string arg, string hdr)
        {
            var value = arg.Substring(hdr.Length);

            OurUuid = new Guid(value);
        }

        private static void procPipe(string arg, string hdr)
        {
            var value = arg.Substring(hdr.Length);

            PipeName = value;

            Pipe = new NamedPipeClientStream(
                ".",
                PipeName,
                PipeDirection.InOut,
                PipeOptions.Asynchronous,
                System.Security.Principal.TokenImpersonationLevel.Identification);
        }

        private static void procLogPipe(string arg, string hdr)
        {
            var value = arg.Substring(hdr.Length);

            LogPipeName = value;

            LogPipe = new NamedPipeClientStream(
                ".",
                LogPipeName,
                PipeDirection.Out,
                PipeOptions.None,
                System.Security.Principal.TokenImpersonationLevel.Identification);

            Logger.Bind(LogPipe);
        }

        private static void procRole(string arg, string hdr)
        {
            var value = arg.Substring(hdr.Length);
        }
    }
}
