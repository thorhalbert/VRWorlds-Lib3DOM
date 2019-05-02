using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Emissary
{
    public enum EmissaryRoles
    {
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

        [JsonProperty(PropertyName ="Manufacturer-ID")]
        public string ManufacturerId { get; set; }

        [JsonProperty(PropertyName = "Signer-CERT-ID")]
        public string SignerCertId { get; set; }

        [JsonProperty(PropertyName = "Developer-ID")]
        public string DeveloperId { get; set; }

        [JsonProperty(PropertyName = "Primay-Role")]
        public EmissaryRoles PrimaryRole { get; set; }

        [JsonProperty(PropertyName = "Indirect-Role")]
        public EmissaryRoles IndirectRole { get; set; }

        [JsonProperty]
        public List<MarshallItem> Marshalls { get; set; }

    }
    [JsonArray]
    public class MarshallItem
    {
        [JsonProperty]
        public string Path { get; set; }

        [JsonProperty]
        public int Length { get; set; }

        [JsonProperty]
        public string SHA256 { get; set; }
    }
}
