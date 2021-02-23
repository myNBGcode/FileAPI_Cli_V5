namespace FileapiCli
{
    using System;

    namespace FileapiCli
    {
        public class SMSOTPException : Exception
        {
            public SMSOTPException()
            {
            }

            public SMSOTPException(string message)
                : base(message)
            {
            }

            public SMSOTPException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }


    }

}
