using System.Text.Json;
using Serilog;

namespace SellingDreamsCommandHandler;

public class LoggingDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand
    where TResult : ICommandResponse
{
    private readonly ICommandHandler<TCommand, TResult> _commandHandler;

    public LoggingDecorator(ICommandHandler<TCommand, TResult> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public TResult Execute(TCommand command)
    {
        Log.Information(
            $"Command Executed: {typeof(TCommand)}: {JsonSerializer.Serialize(command)}"
        );
        try
        {
            var response = _commandHandler.Execute(command);
            Log.Information($"Command {typeof(TCommand)} executed successfully: {JsonSerializer.Serialize(response)}");
            return response;
        }
        catch (System.Exception ex)
        {
            Log.Error(ex, $"Command {typeof(TCommand)} failed during execution.");
            throw;
        }
    }
}

public class LoggingDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand> _commandHandler;

    public LoggingDecorator(ICommandHandler<TCommand> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public void Execute(TCommand command)
    {
        Log.Information(
            $"Command Executed: {typeof(TCommand)}: {JsonSerializer.Serialize(command)}"
        );
        try
        {
            _commandHandler.Execute(command);
            Log.Information($"Command {typeof(TCommand)} executed successfully.");
        }
        catch (System.Exception ex)
        {
            Log.Error(ex, $"Command {typeof(TCommand)} failed during execution.");
            throw;
        }
    }
}

public class LoggingDecoratorList<TCommand, TResult> : ICommandHandlerList<TCommand, TResult>
    where TCommand : ICommand
    where TResult : ICommandResponse
{
    private readonly ICommandHandlerList<TCommand, TResult> _commandHandler;

    public LoggingDecoratorList(ICommandHandlerList<TCommand, TResult> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public IEnumerable<TResult> Execute(TCommand command)
    {
        Log.Information(
            $"Command Executed: {typeof(TCommand)}: {JsonSerializer.Serialize(command)}"
        );
        try
        {
            var response = _commandHandler.Execute(command);
            Log.Information($"Command {typeof(TCommand)} executed successfully number of records {response.Count()}");
            return response;
        }
        catch (System.Exception ex)
        {
            Log.Error(ex, $"Command {typeof(TCommand)} failed during execution.");
            throw;
        }
    }
}

public class LoggingDecoratorAsync<TCommand, TResult> : ICommandHandlerAsync<TCommand, TResult>
    where TCommand : ICommand
    where TResult : ICommandResponse
{
    private readonly ICommandHandlerAsync<TCommand, TResult> _commandHandler;

    public LoggingDecoratorAsync(ICommandHandlerAsync<TCommand, TResult> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public async Task<TResult> ExecuteAsync(TCommand command)
    {
        Log.Information(
            $"Command Executed: {typeof(TCommand)}: {JsonSerializer.Serialize(command)}"
        );
        try
        {
            var response = await _commandHandler.ExecuteAsync(command);
            Log.Information($"Command {typeof(TCommand)} executed successfully: {JsonSerializer.Serialize(response)}");
            return response;
        }
        catch (System.Exception ex)
        {
            Log.Error(ex, $"Command {typeof(TCommand)} failed during execution.");
            throw;
        }
    }
}

public class LoggingDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand>
    where TCommand : ICommand
{
    private readonly ICommandHandlerAsync<TCommand> _commandHandler;

    public LoggingDecoratorAsync(ICommandHandlerAsync<TCommand> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public async Task ExecuteAsync(TCommand command)
    {
        Log.Information(
            $"Command Executed: {typeof(TCommand)}: {JsonSerializer.Serialize(command)}"
        );
        try
        {
            await _commandHandler.ExecuteAsync(command);
            Log.Information($"Command {typeof(TCommand)} executed successfully.");
        }
        catch (System.Exception ex)
        {
            Log.Error(ex, $"Command {typeof(TCommand)} failed during execution.");
            throw;
        }
    }
}

public class LoggingDecoratorListAsync<TCommand, TResult> : ICommandHandlerListAsync<TCommand, TResult>
    where TCommand : ICommand
    where TResult : ICommandResponse
{
    private readonly ICommandHandlerListAsync<TCommand, TResult> _commandHandler;

    public LoggingDecoratorListAsync(ICommandHandlerListAsync<TCommand, TResult> commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public async Task<IEnumerable<TResult>> ExecuteAsync(TCommand command)
    {
        Log.Information(
            $"Command Executed: {typeof(TCommand)}: {JsonSerializer.Serialize(command)}"
        );
        try
        {
            var response = await _commandHandler.ExecuteAsync(command);
            Log.Information($"Command {typeof(TCommand)} executed successfully number of records {response.Count()}");
            return response;
        }
        catch (System.Exception ex)
        {
            Log.Error(ex, $"Command {typeof(TCommand)} failed during execution.");
            throw;
        }
    }
}
