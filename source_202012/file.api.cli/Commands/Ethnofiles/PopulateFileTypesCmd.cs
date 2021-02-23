using FileapiCli.ConfigOptions;
using FileapiCli.Core;

namespace FileapiCli.Commands
{
    public class PopulateFileTypesCmd : CliCommand<PopulateFileTypesResult>
    {
        protected PopulateFileTypesCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static PopulateFileTypesCmd Create(PopulateFileTypesOptions opts, IUserInfo userInfo)
        {
            return new PopulateFileTypesCmd(userInfo);
        }
    }
    public class PopulateFileTypesResult : IResult
    {

    }
}
