using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;

namespace FileapiCli.Commands
{
    public class RequestStatusCmd : CliCommand<RequestStatusResult>
    {
        public string FileId { get; set; }

        protected RequestStatusCmd(IUserInfo userInfo)
           : base(userInfo) {}

        internal static RequestStatusCmd Create(RequestPaymentStatusOption opts, IUserInfo userInfo)
        {
            var cmd = new RequestStatusCmd(userInfo)
            { 
               FileId = opts.FileId
            };
            return cmd;
        }
        public static bool ValidateInput(RequestPaymentStatusOption opts)
        {
            if (!Guid.TryParse(opts.FileId, out Guid _))
            {
                throw new ArgumentException($"{nameof(opts.FileId)} is not a valid Guid.");
            }
            return true;
        }
    }

    public class RequestStatusResult : IResult
    {
        public bool ReceivedSuccessfully { get; set; }
    }
}



