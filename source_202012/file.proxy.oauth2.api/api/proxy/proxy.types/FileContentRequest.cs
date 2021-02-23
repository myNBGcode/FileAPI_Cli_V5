using System;

namespace proxy.types
{
    public class FileContentRequest
    {
        public string Requester { get; set; }

        public string Subject { get; set; }

        public Guid FileId { get; set; }

        public int ChunkPart { get; set; }
    }
}
