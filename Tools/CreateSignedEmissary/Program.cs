﻿using System;
using Common.Emissary;

namespace CreateSignedEmissary
{
    class Program
    {
        static void Main(string[] args)
        {
            var haveCert = false;

            var emissaryCreator = new CreateEmissary();

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                if (arg == "--cert")
                {
                    var certFile = args[i + 1];
                    emissaryCreator.procCertFile(certFile);
                    i++;
                    haveCert = true;
                    continue;
                }

                if (arg == "--store")
                {
                    var storeDn = args[i + 1];
                    emissaryCreator.procCertStore(storeDn);
                    i++;
                    haveCert = true;
                    continue;
                }

                if (!haveCert)
                    throw new Exception("You must specify an cert before directories");

                emissaryCreator.procEmissaryDir(arg);
            }
        }
    }
}