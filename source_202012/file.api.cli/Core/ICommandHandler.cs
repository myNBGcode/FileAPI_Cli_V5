namespace FileapiCli.Core
{
    public interface ICommandHandler<in TCommand, out TResult>
        where TCommand : ICommand<TResult>
        where TResult : IResult, new()
    {
        TResult Handle(TCommand command);
    }
}


