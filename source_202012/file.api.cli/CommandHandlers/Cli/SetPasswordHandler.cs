using FileapiCli.Commands;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Text;

namespace FileapiCli.CommandHandlers
{
    public class SetPasswordHandler : CliCommandHandler<SetPasswordCmd, SetPasswordResult>
    {
        public SetPasswordHandler(ILoggerFactory loggerFactory, ICliService cliService, ICommandValidator<SetPasswordCmd> validator)
              : base(loggerFactory, cliService, validator)
        { }

        protected override SetPasswordResult DoHandle(SetPasswordCmd setPasswordCmd)
        {
            var result = new SetPasswordResult();
            Console.WriteLine();
            Console.Write("Enter new password: ");
            StringBuilder passwordBuilder = new StringBuilder();
            bool continueReading = true;
            char newLineChar = '\r';
            while (continueReading)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                char passwordChar = consoleKeyInfo.KeyChar;
                if (passwordChar == newLineChar) continueReading = false;
                else passwordBuilder.Append(passwordChar.ToString());
            }
            Console.WriteLine();

            var password = passwordBuilder.ToString();
            _cliService.SetPassword(password);

            _logger.LogInformation($"Encrypted password is updated in app settings.");
            _logger.LogInformation($"!! ATTENTION: You must change the property 'safe_password' to true in the app settings, in order to use the encrypted password.");
            Console.WriteLine();
            return result;
        }
    }
}
