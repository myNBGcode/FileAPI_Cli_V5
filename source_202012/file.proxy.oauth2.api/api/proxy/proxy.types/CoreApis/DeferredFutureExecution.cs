using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class DeferredFutureExecution
    {
        [DataMember(Name = "executionTime")]
        public DateTime? ExecutionTime { get; set; }

        [DataMember(Name = "executionStatus")]
        public string ExecutionStatus { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "scheduleId")]
        public string ScheduleId { get; set; }

        [DataMember(Name = "externalId")]
        public string ExternalId { get; set; }

        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; set; }

    }
}