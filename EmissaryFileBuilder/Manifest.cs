using System;
using System.Collections.Generic;
using System.Text;

namespace EmissaryFileBuilder
{
    public class ManifestEntry
    { 
        public System.IO.FileInfo Path { get; set; }
        public Int64 Length { get; set; }
        public System.Security.Cryptography.SHA256Managed Sha256 { get; set; }
    }
    public class Manifest
    {
        public List<ManifestEntry> ManifestList;
    }
}
