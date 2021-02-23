using FileapiCli.Core;

namespace FileapiCli.Commands
{
    public class SetPasswordCmd : CliCommand<SetPasswordResult>
    {
        internal static SetPasswordCmd Create()
        {
            return new SetPasswordCmd();
        }
    }

    public class SetPasswordResult : IResult
    {
    }
}
