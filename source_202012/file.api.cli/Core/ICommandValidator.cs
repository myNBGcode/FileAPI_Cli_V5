namespace FileapiCli.Core
{
    public interface ICommandValidator<TCommand>
    {
        bool IsCommandValid(TCommand command); 
    }
}