using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("setpassword", HelpText = "Set user password using encryption.")]
    public class SetPasswordOptions : IOptions
    {
    }
}
