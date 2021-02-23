using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class DeferredDetails
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "field")]
        public string Field { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

    }
}