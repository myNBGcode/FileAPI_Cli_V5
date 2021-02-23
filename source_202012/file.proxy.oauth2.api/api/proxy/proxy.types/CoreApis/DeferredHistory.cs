using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class DeferredHistory
    {
        [DataMember(Name = "master")]
        public DeferredHistoryMaster Master;

        [DataMember(Name = "execution")]
        public DeferredHistoryExecution[] Execution;
    }
}