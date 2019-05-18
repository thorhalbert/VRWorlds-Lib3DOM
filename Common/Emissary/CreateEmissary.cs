using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace Common.Emissary
{
    public class CreateEmissary
    {
        string signingCert;
        string signingKey;
        public void procCertFile(string certFile)
        {
            signingCert = certFile;
        }
        public void procKeyFile(string certFile)
        {
            signingKey = certFile;
        }
        public void procCertStore(string dn) { }
        public void procEmissaryDir(string dirName)
        {
            EmissarySerialization.GenerateEmissary(signingKey, signingCert, dirName);
        }
    }
}
