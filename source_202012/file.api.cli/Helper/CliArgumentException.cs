namespace FileapiCli
{
    using System;

    namespace FileapiCli
    {
        public class CliArgumentException : Exception
        {
            public CliArgumentException()
            {
            }

            public CliArgumentException(string message)
                : base(message)
            {
            }

            public CliArgumentException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }


    }

}
