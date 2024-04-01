namespace SellingDreamsCommandHandler;

public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand where TResponse : ICommandResponse
{
  TResponse Execute(TCommand command);
}

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    void Execute(TCommand command);
}

public interface ICommandHandlerList<TCommand, TResponse> where TCommand : ICommand where TResponse : ICommandResponse
{
   IEnumerable<TResponse> ExecuteAsync(TCommand command);
}

public interface ICommandHandlerAsync<TCommand, TResponse> where TCommand : ICommand where TResponse : ICommandResponse
{
   Task<TResponse> ExecuteAsync(TCommand command);
}

public interface ICommandHandlerAsync<TCommand> where TCommand : ICommand
{
    Task ExecuteAsync(TCommand command);
}

public interface ICommandHandlerListAsync<TCommand, TResponse> where TCommand : ICommand where TResponse : ICommandResponse
{
   Task<IEnumerable<TResponse>> ExecuteAsync(TCommand command);
}
