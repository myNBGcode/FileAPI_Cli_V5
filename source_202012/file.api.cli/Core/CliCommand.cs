using Newtonsoft.Json.Serialization;
using System;

namespace FileapiCli.Core
{
    public class CliCommand<TResult> : ICommand<TResult>
       where TResult : IResult
    {
        public readonly IUserInfo UserInfo;
        public readonly string RequestId;

        protected CliCommand(IUserInfo userInfo)
        {
            UserInfo = userInfo;
            RequestId = Guid.NewGuid().ToString();
        }

        protected CliCommand()
        {
        }
    }
}