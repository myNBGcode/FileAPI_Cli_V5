using System;

namespace proxy.types
{
    public class FileDetailsRequest
    {
        public string Requester { get; set; }

        public string Subject { get; set; }

        public Guid FileId { get; set; }
    }
}