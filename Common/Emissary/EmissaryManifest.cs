using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Vlingo.UUID;

namespace Common.Emissary
{
    public static class EmissarySerialization
    {
        public static void ReadManifest()
        {
        }
        public static void WriteManifest()
        {
        }



        private static string determinePathStub(string baseStub, string targetPath)
        {
            return null;
        }
        public static IEnumerable<ContentItem> AssayFiles(string emissaryDir, IEnumerable<ContentItem> originalContentList)
        {

            var uuid5Gen = new NameBasedGenerator(HashType.SHA1);

            var newFiles = new Dictionary<Guid, ContentItem>();

            var dirInfo = new DirectoryInfo(emissaryDir);

            // We have to recurse - have a submethod
            void _pursueDirs(DirectoryInfo dir, int level)
            {
                bool allow;


                foreach (var d in dir.GetDirectories())
                {
                    var pathStub = determinePathStub(dirInfo.FullName, d.FullName);

                    if (level == 0)
                    {
                        allow = false;

                        switch (pathStub)
                        {
                            case "_code":
                            case "_payload";
                                allow = true;
                                break;
                        }

                        if (!allow)   // Skip any other directories at top level
                            continue;
                    }

                    _pursueDirs(d, level + 1);
                }

                foreach (var f in dir.GetFiles())
                {
                    var pathStub = determinePathStub(dirInfo.FullName, f.FullName);
                    var fileGuid = uuid5Gen.GenerateGuid(UUIDNameSpace.URL, pathStub);

                    if (level == 0)
                    {
                        allow = false;

                        switch (pathStub)
                        {
                            case "CERT.CA":
                                // case "Manifest.json":   // We're going to modify this  
                                // case "Manifest.pem":    // We'll generate this and overwrite
                                allow = true;
                                break;
                        }

                        if (!allow)
                            continue;
                    }

                    var dirtyLength = f.Length;
                    var dirtyWrite = f.LastWriteTimeUtc;

                    var dirtyString = dirtyWrite.ToLongDateString() + "|" + dirtyLength.ToString();

                    var dirtyState = uuid5Gen.GenerateGuid(UUIDNameSpace.URL, dirtyString);

                    newFiles.Add(fileGuid, new ContentItem
                    {
                        Path = pathStub,
                        DirtyState = dirtyState,
                        Dirty = false,
                        Length = dirtyLength,
                        SHA256 = null
                    });
                }

            }

            _pursueDirs(dirInfo, 0);

            // Now we need to compare the files together 

            var newComposites = new SortedList<string, ContentItem>();

            foreach (var o in originalContentList)
            {
                var fileGuid = uuid5Gen.GenerateGuid(UUIDNameSpace.URL, o.Path);
                o.Dirty = true;

                if (!newFiles.ContainsKey(fileGuid))
                    continue;   // This file no longer exists, so skip it

                var newFile = newFiles[fileGuid];

                newFile.Dirty = false;   // Assume the best

                if (o.SHA256 == null)
                    newFile.Dirty = true;
                if (newFile.DirtyState != o.DirtyState)
                    newFile.Dirty = true;

                if (newFile.Dirty)
                    ComputeHash(newFile, Path.Combine(emissaryDir, o.Path));

                newComposites.Add(o.Path, newFile);

                newFiles.Remove(fileGuid);   // Remove the common files
            }

            // Now we add new files not in the old list

            foreach (var n in newFiles)
            {
                var newFile = n.Value;

                ComputeHash(newFile, Path.Combine(emissaryDir, newFile.Path));
                newComposites.Add(newFile.Path, newFile);
            }

            var outputList = new List<ContentItem>();
            foreach (var o in newComposites)
                outputList.Add(o.Value);    // Sorted

            return outputList;
        }
        public static void ComputeHash(ContentItem newFile, string v)
        {
            SHA256 mySHA256 = SHA256Managed.Create();

            using (var filestream = new FileStream(v, FileMode.Open)
            {
                Position = 0
            })
            {
                byte[] hashValue = mySHA256.ComputeHash(filestream);

                newFile.SHA256 = BitConverter.ToString(hashValue).Replace("-", String.Empty);
            }
        }
    }
    public enum EmissaryRoles
    {
        Unset,
        World,
        Avatar,
        Entity
    }
    [JsonObject(MemberSerialization.OptIn)]
    public class EmissaryManifest
    {
        [JsonProperty]
        public string Version { get; set; }

        [JsonProperty]
        public string APIVersion { get; set; }

        [JsonProperty(PropertyName = "Manufacturer-ID")]
        public string ManufacturerId { get; set; }

        [JsonProperty(PropertyName = "Signer-CERT-ID")]
        public string SignerCertId { get; set; }

        [JsonProperty(PropertyName = "Developer-ID")]
        public string DeveloperId { get; set; }

        [JsonProperty(PropertyName = "Primary-Role")]
        public EmissaryRoles PrimaryRole { get; set; }

        [JsonProperty(PropertyName = "Indirect-Role")]
        public EmissaryRoles IndirectRole { get; set; }

        [JsonProperty]
        public List<ContentItem> Contents { get; set; }

        /*
         *  ** Marhsalling
         *  
         *  Version 0 - Everything needs to be self contained.  Later maybe there is a way
         *  to split this up into pieces, allowing something like a linker to put them all together.
         *  
         *  There would be a dependancy - like an Emissary id and a version.
         *  Also there probably needs to be an Emissary type since now you might have libraries which would be shared.
         *  
             *** Requires
             *** Exports
             *** Imports
             *** Startup
             ** Revision
             *** Package-ID:
             *** Name:
             *** Version:
             *** Repository:
         * 
         */

        public EmissaryManifest()
        {
            Version = "0.0";
            APIVersion = "0.0";

            ManufacturerId = "UNSET";
            SignerCertId = "UNSET";
            DeveloperId = "UNSET";

            PrimaryRole = EmissaryRoles.Unset;
            IndirectRole = EmissaryRoles.Unset;

            Contents = new List<ContentItem>();
        }

    }
    [JsonArray]
    public class ContentItem
    {
        [JsonProperty]
        public string Path { get; set; }

        [JsonProperty]
        public long Length { get; set; }

        [JsonProperty]
        public string SHA256 { get; set; }

        [JsonProperty]
        public Guid DirtyState { get; set; }

        // Hidden bookkeeping properties
        public bool Dirty { get; set; }
    }
}
