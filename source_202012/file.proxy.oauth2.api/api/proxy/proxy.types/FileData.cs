using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class FileData
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "data")]
        public byte[] Data { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
