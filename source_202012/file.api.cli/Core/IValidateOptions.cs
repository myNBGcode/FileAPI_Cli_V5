using FileapiCli.ConfigOptions;

namespace FileapiCli.Core
{
    public interface IValidateInput<TOptions> where TOptions:IOptions
    {
        bool IsValid(TOptions inputOptions);
    }
}
