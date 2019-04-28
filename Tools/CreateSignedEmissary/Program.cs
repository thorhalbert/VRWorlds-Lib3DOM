using System;

namespace CreateSignedEmissary
{
    class Program
    {
        static void procCertFile(string certFile) { }
        static void procCertStore(string dn) { }
        static void procEmissaryDir(string dirName) { }

        static void Main(string[] args)
        {
            var haveCert = false;

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                if (arg == "--cert")
                {
                    var certFile = args[i + 1];
                    procCertFile(certFile);
                    i++;
                    haveCert = true;
                    continue;
                }

                if (arg == "--store")
                {
                    var storeDn = args[i + 1];
                    procCertStore(storeDn);
                    i++;
                    haveCert = true;
                    continue;
                }

                if (!haveCert)
                    throw new Exception("You must specify an cert before directories");

                procEmissaryDir(arg);
            }
        }
    }
}
