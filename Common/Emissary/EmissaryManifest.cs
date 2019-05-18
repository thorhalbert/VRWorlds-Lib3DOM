using ICSharpCode.SharpZipLib.Tar;
using Newtonsoft.Json;
using PemUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Vlingo.UUID;

namespace Common.Emissary
{
    public static class EmissarySerialization
    {
        /*
         * Algorithm:
         *      Collect private key guid and directory for emissary dir
         *      Remove output files like signature 
         *      Read existing manifest if exists
         *      Do the file Assay, supplying old list/dirtystates if they exist
         *      Write out updated manifest  
         *      Check for status/proper files/sanity of all fields
         *      If there are errors, such as unfilled in entries or missing files, report and exit
         *      Start list of manifest items
         *          Add Manifest to manifest items
         *      Compute sha256 of manifest
         *      Generate a guid from the sha256 
         *      Generate public key from full certificate
         *          Add cert to manifest items
         *      Sign this via our private cert
         *      Write out signature
         *          Add signature to manifest
         *      Generate tar file from main manifest and manifest items
         *      Rename to final filename
         *      Store manifest keyed by the guid
         *          Write manifest summary file
         *          
         *  Probably need some way to handle the differences between windows and linux.  Though
         *  initially we'll just utilize a full der file.   Windows has a cert-store.
         *  
         *  ISSUES:
         *      We really need to validate the certicate chain, the manufacturer and developer ID
         *      
         *      We need to support microsofts certificate store
         *      We also need to make sure the private key is at least passphrased 
         *      Other possibility is that we call the kudo server to do the signing so we
         *         keep the signing key in the secure environment (similarly we might get the
         *         cert from there rather than expecting the developer to have the right one).
         */

        private const string manifestFile = "Manifest.json";
        private const string publicCertFile = "PublicCert.crt";
        private const string manifestSignature = "Manifest.sig";
        private const string manifestSummary = "ManifestSummary.json";

        static internal SHA256 _genSHA256 = SHA256Managed.Create();
        static internal NameBasedGenerator _uuid5Gen = new NameBasedGenerator(HashType.SHA1);

        public static bool GenerateEmissary(string signingKey, string certFile, string emissaryDir)
        {
            // Do some contract asserts
            if (!File.Exists(signingKey))
                throw new ArgumentException("Signing Key doesn't exist: " + signingKey);
            if (!File.Exists(certFile))
                throw new ArgumentException("Signing Certificate doesn't exist: " + certFile);
            if (!Directory.Exists(emissaryDir))
                throw new ArgumentException("Emissary directory doesn't exist: " + emissaryDir);

            _checkCertificates(signingKey, certFile);

            // Map out to real files

            var fullManifestFile = Path.Combine(emissaryDir, manifestFile);
            var fullpublicCertFile = Path.Combine(emissaryDir, publicCertFile);
            var fullmanifestSignature = Path.Combine(emissaryDir, manifestSignature);
            var fullmanifestSummary = Path.Combine(emissaryDir, manifestSummary);

            // Clear out true outputs

            if (File.Exists(fullpublicCertFile))
                File.Delete(fullpublicCertFile);

            if (File.Exists(fullmanifestSignature))
                File.Delete(fullmanifestSignature);

            if (File.Exists(fullmanifestSummary))
                File.Delete(fullmanifestSummary);

            // Read in the manifest if it exists, or initialize it if not

            var projectManifest = _initializeEmptyManifest();
            if (File.Exists(fullManifestFile))
                try
                {
                    projectManifest = _readManifest(fullManifestFile);
                }
                catch(Exception ex)
                {
                    projectManifest = new EmissaryManifest();
                }

            if (projectManifest == null)
                projectManifest = new EmissaryManifest();

            // File assay - compute content items

            var manifestItems = new List<ContentItem>();
            var contentItems = _assayFiles(emissaryDir, projectManifest.Contents);
            projectManifest.Contents = contentItems.ToList();

            // Check for sanity

            var (check, errors) = _checkSanity(projectManifest);

            if (check)
            {
                // Dump the error list

                foreach (var e in errors)
                    Console.WriteLine(e);

                // Generate the manifest so the user can update it

                _generateManifestFile(projectManifest, manifestFile, fullManifestFile);

                return false;
            }

            // Generate the cert file

            _generatePublicCertFile(certFile, fullpublicCertFile);
            //_addManifestItem(manifestItems, publicCertFile, fullpublicCertFile);

            // Compute the guid for this run

            var emissaryGuid = Guid.NewGuid();

            projectManifest.EmissaryInstanceId = emissaryGuid;
            projectManifest.GenerateDate = DateTimeOffset.Now;

            // Generate the manifest - the manifest itself can't be in it, or the signature
            // Add the cert file

            contentItems = _assayFiles(emissaryDir, projectManifest.Contents);
            projectManifest.Contents = contentItems.ToList();

            _generateManifestFile(projectManifest, manifestFile, fullManifestFile);
            _addManifestItem(manifestItems, manifestFile, fullManifestFile);

            // Compute and sign the hash

            var manifestHash = _computeHash(fullManifestFile);

            _signAndWriteSignature(signingKey, manifestHash, fullmanifestSignature);
            _addManifestItem(manifestItems, manifestSignature, fullmanifestSignature);

            //_generateManifestSummary(projectManifest, manifestHash, signature);

            // Eventually this will have to talk to a database or a server - or we'll have some checkin feature

            // Create the actual manifest file (tar)

            var emissaryTmp = Path.Combine(emissaryDir, emissaryGuid.ToString() + ".tmp");
            var dateStamp = DateTime.UtcNow.ToString("yyyyMMddHHmm");
            var emissaryFile = Path.Combine(emissaryDir, projectManifest.EmissaryClassId + "-" + dateStamp + "-" + emissaryGuid.ToString() + ".emissary");


            _createEmissary(emissaryDir, projectManifest, manifestItems, emissaryFile, emissaryTmp);

            return true;
        }

        private static void _checkCertificates(string signingKey, string certFile)
        {
            // Ultimately we need to check to see if this follows policy - key big enough, etc
            // Also, it needs to be signed by our kudo server and be in the proper signing chain

            // Also, check to see if the private key matches the public key

            // We throw if invalid
        }

        //private static void _generateManifestSummary(EmissaryManifest projectManifest, byte[] manifestHash, byte[] signature)
        //{
        //    throw new NotImplementedException();
        //}

        private static void _signAndWriteSignature(string certFile, byte[] manifestHash, string manifestSignature)
        {
            using (var stream = File.OpenRead(certFile))
            using (var reader = new PemReader(stream))
            {

                var rsaParameters = reader.ReadRsaKey();

                var rsaMachine = new RSACryptoServiceProvider();
                rsaMachine.ImportParameters(rsaParameters);

                var certSig = rsaMachine.SignData(manifestHash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                using (var text = new StreamWriter(manifestSignature))
                {
                    text.WriteLine("-----BEGIN TRUSTED CERTIFICATE-----");
                    text.WriteLine(Convert.ToBase64String(certSig, Base64FormattingOptions.InsertLineBreaks));
                    text.WriteLine("-----END TRUSTED CERTIFICATE-----");
                }
            }
        }

        private static void _addManifestItem(List<ContentItem> manifestItems, string fileName, string fullFileName)
        {
            var item = new ContentItem(fileName, fullFileName);
            _computeHash(item, fullFileName);
            manifestItems.Add(item);
        }

        private static void _generateManifestFile(EmissaryManifest projectManifest, string manifestFile, string physicalFile)
        {
            using (StreamWriter file = File.CreateText(physicalFile))
            {
                var doSerial = new JsonSerializer();
                doSerial.Formatting = Formatting.Indented;

                doSerial.Serialize(file, projectManifest);
            }
        }

        private static (bool, List<string>) _checkSanity(EmissaryManifest projectManifest)
        {
            var errors = new List<string>();

            if (projectManifest.Version == "0.0")
                errors.Add("Version is not set");
            if (projectManifest.APIVersion == "0.0")
                errors.Add("APIVersion is not set");

            if (projectManifest.ManufacturerId == Guid.Empty)
                errors.Add("ManufacturerId is not set");
            if (projectManifest.SignerCertId == Guid.Empty)
                errors.Add("SignerCertId is not set");
            if (projectManifest.DeveloperId == Guid.Empty)
                errors.Add("DeveloperId is not set");
            if (projectManifest.EmissaryClassId == Guid.Empty)
                errors.Add("EmissaryClassId is not set");
            //if (projectManifest.EmissaryInstanceId == Guid.Empty)
            //    errors.Add("EmissaryInstanceId is not set");

            if (projectManifest.PrimaryRole == EmissaryRoles.Unset)
                errors.Add("PrimaryRole is not set");
            if (projectManifest.IndirectRole == EmissaryRoles.Unset)
                errors.Add("IndirectRole is not set");

            // Have to check for required files


            return (errors.Count != 0, errors);
        }

        private static void _generatePublicCertFile(string certFile, string publicCertFile)
        {
            // This probably needs to be more secure ultimately
            var cert = new X509Certificate2(certFile);

            var certOutput = cert.Export(X509ContentType.Cert);

            using (var stream = File.CreateText(publicCertFile))
            using (var writer = new PemWriter(stream.BaseStream))
            {
                writer.WritePublicKey(cert.GetRSAPublicKey());
            }

            //    using (var text = new StreamWriter(publicCertFile))
            //{
            //    text.WriteLine("-----BEGIN CERTIFICATE-----");
            //    text.WriteLine(Convert.ToBase64String(certOutput, Base64FormattingOptions.InsertLineBreaks));
            //    text.WriteLine("-----END CERTIFICATE-----");
            //}
        }

        private static EmissaryManifest _readManifest(string fullManifestFile)
        {
            using (var file = File.OpenText(fullManifestFile))
            {
                var doSerial = new JsonSerializer();
                var newManifest = (EmissaryManifest)doSerial.Deserialize(file, typeof(EmissaryManifest));
                return newManifest;
            }
        }

        private static EmissaryManifest _initializeEmptyManifest()
        {
            return new EmissaryManifest();
        }

        private static void _createEmissary(string manifestDir, EmissaryManifest manifest, List<ContentItem> manifestItems, string outFileName, string tmpFileName)
        {
            void writeEntries(IEnumerable<ContentItem> contents, TarArchive tarOutput)
            {
                foreach (var content in contents)
                {
                    var file = Path.Combine(manifestDir, content.Path);
                    var tarPath = content.Path;

                    // Perform de-windowification
                    if (Path.DirectorySeparatorChar != '/')
                        tarPath = tarPath.Replace(Path.DirectorySeparatorChar, '/');

                    var entry = TarEntry.CreateEntryFromFile(file);
                    entry.Name = tarPath;
                    entry.UserId = 0;
                    entry.GroupId = 0;
                    entry.UserName = manifest.DeveloperId.ToString();   // This might be too big
                    entry.GroupName = manifest.ManufacturerId.ToString();

                    tarOutput.WriteEntry(entry, false);
                }
            }

            // To write a strict tar file we really need to be creating directories too

            using (var binaryStream = new BinaryWriter(File.Open(tmpFileName, FileMode.Create)))
            using (var tarOutput = TarArchive.CreateOutputTarArchive(binaryStream.BaseStream))
            {
                // Write the main files

                writeEntries(manifest.Contents, tarOutput);
                writeEntries(manifestItems, tarOutput);  // Though these are all in the root diretory

                tarOutput.Close();
            }

            File.Move(tmpFileName, outFileName);
        }

        private static string _determinePathStub(string baseStub, string targetPath)
        {
            var path = targetPath.Substring(baseStub.Length);

            return path;
        }
        private static IEnumerable<ContentItem> _assayFiles(string emissaryDir, IEnumerable<ContentItem> originalContentList)
        {
            var newFiles = new Dictionary<Guid, ContentItem>();

            var dirInfo = new DirectoryInfo(emissaryDir);

            // We have to recurse - have a submethod
            void _pursueDirs(DirectoryInfo dir, int level)
            {
                bool allow;

                foreach (var d in dir.GetDirectories())
                {
                    var pathStub = _determinePathStub(dirInfo.FullName, d.FullName);

                    if (level == 0)
                    {
                        allow = false;

                        switch (pathStub)
                        {
                            case "_code":
                            case "_payload":
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
                    if (f.FullName.EndsWith("~")) continue;   // Emacs temp files

                    var pathStub = _determinePathStub(dirInfo.FullName, f.FullName);
                    var fileGuid = _uuid5Gen.GenerateGuid(UUIDNameSpace.URL, pathStub);

                    if (level == 0)
                    {
                        allow = false;

                        // No files in the root that aren't generated

                        switch (pathStub)
                        {
                            case publicCertFile:
                                allow = true;
                                break;
                        }

                        if (!allow)
                            continue;
                    }

                    newFiles.Add(fileGuid, new ContentItem(pathStub, f));

                }
            }

            _pursueDirs(dirInfo, 0);

            // Now we need to compare the files together 

            var newComposites = new SortedList<string, ContentItem>();

            foreach (var o in originalContentList)
            {
                var fileGuid = _uuid5Gen.GenerateGuid(UUIDNameSpace.URL, o.Path);
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
                    _computeHash(newFile, Path.Combine(emissaryDir, o.Path));

                newComposites.Add(o.Path, newFile);

                newFiles.Remove(fileGuid);   // Remove the common files
            }

            // Now we add new files not in the old list

            foreach (var n in newFiles)
            {
                var newFile = n.Value;

                _computeHash(newFile, Path.Combine(emissaryDir, newFile.Path));
                newComposites.Add(newFile.Path, newFile);
            }

            var outputList = new List<ContentItem>();
            foreach (var o in newComposites)
                outputList.Add(o.Value);    // Sorted

            return outputList;
        }

        private static byte[] _computeHash(string manifestFile)
        {
            using (var filestream = new FileStream(manifestFile, FileMode.Open))
            {
                return _genSHA256.ComputeHash(filestream);
            }
        }
        public static void _computeHash(ContentItem newFile, string v)
        {
            var hashValue = _computeHash(v);

            newFile.SHA256 = BitConverter.ToString(hashValue).Replace("-", String.Empty);

            newFile.Dirty = false;
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

        [JsonProperty(PropertyName = "Generate-Date")]
        public DateTimeOffset GenerateDate { get; set; }

        [JsonProperty(PropertyName = "Manufacturer-ID")]
        public Guid ManufacturerId { get; set; }

        [JsonProperty(PropertyName = "Signer-CERT-ID")]
        public Guid SignerCertId { get; set; }

        [JsonProperty(PropertyName = "Developer-ID")]
        public Guid DeveloperId { get; set; }

        [JsonProperty(PropertyName = "Emissary-Class-ID")]
        public Guid EmissaryClassId { get; set; }

        [JsonProperty(PropertyName = "Emissary-Instance-ID")]
        public Guid EmissaryInstanceId { get; set; }

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

            ManufacturerId = Guid.Empty;
            SignerCertId = Guid.Empty;
            DeveloperId = Guid.Empty;
            EmissaryClassId = Guid.Empty;
            EmissaryInstanceId = Guid.Empty;

            PrimaryRole = EmissaryRoles.Unset;
            IndirectRole = EmissaryRoles.Unset;

            Contents = new List<ContentItem>();
        }

    }
    [JsonObject(MemberSerialization.OptIn)]
    public class ContentItem
    {
        public ContentItem() { }  
        public ContentItem(string pathStub, FileInfo f)
        {
            Path = pathStub;
            Length = f.Length;
            var dirtyWrite = f.LastWriteTimeUtc;

            var dirtyString = dirtyWrite.ToLongDateString() + "|" + Length.ToString();

            DirtyState = EmissarySerialization._uuid5Gen.GenerateGuid(UUIDNameSpace.URL, dirtyString);
            SHA256 = null;
            Dirty = true;

            // Unixify the path if needed - might be other security scrubbing needed here on filenames
            Path = Path.Replace('\\', '/').Trim();
        }

        public ContentItem(string fileName, string fullFileName) : this(fileName, new FileInfo(fullFileName))
        {
        }

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
