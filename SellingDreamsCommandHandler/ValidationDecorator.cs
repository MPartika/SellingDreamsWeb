using FluentValidation;

namespace SellingDreamsCommandHandler;

public class ValidationDecoratorAsync<TCommand, TResult> : ICommandHandlerAsync<TCommand, TResult>
    where TCommand : ICommand
    where TResult : ICommandResponse
{
    private readonly ICommandHandlerAsync<TCommand, TResult> _commandHandler;
    private readonly AbstractValidator<TCommand> _validator;

    public ValidationDecoratorAsync(
        ICommandHandlerAsync<TCommand, TResult> commandHandler,
        AbstractValidator<TCommand> validator
    )
    {
        _commandHandler = commandHandler;
        _validator = validator;
    }

    public async Task<TResult> ExecuteAsync(TCommand command)
    {
        _validator.Validate(
            command,
            options =>
            {
                options.ThrowOnFailures();
                options.IncludeAllRuleSets();
            }
        );

        var response = await _commandHandler.ExecuteAsync(command);
        return response;
    }
}

public class ValidationDecoratorAsync<TCommand> : ICommandHandlerAsync<TCommand>
    where TCommand : ICommand
{
    private readonly ICommandHandlerAsync<TCommand> _commandHandler;
    private readonly AbstractValidator<TCommand> _validator;

    public ValidationDecoratorAsync(
        ICommandHandlerAsync<TCommand> commandHandler,
        AbstractValidator<TCommand> validator
    )
    {
        _commandHandler = commandHandler;
        _validator = validator;
    }

    public async Task ExecuteAsync(TCommand command)
    {
        _validator.Validate(
            command,
            options => {
                options.ThrowOnFailures();
                options.IncludeAllRuleSets();
            }
        );

        await _commandHandler.ExecuteAsync(command);
    }
}

public class ValidationDecoratorListAsync<TCommand, TResult>
    : ICommandHandlerListAsync<TCommand, TResult>
    where TCommand : ICommand
    where TResult : ICommandResponse
{
    private readonly ICommandHandlerListAsync<TCommand, TResult> _commandHandler;
    private readonly AbstractValidator<TCommand> _validator;

    public ValidationDecoratorListAsync(
        ICommandHandlerListAsync<TCommand, TResult> commandHandler,
        AbstractValidator<TCommand> validator
    )
    {
        _commandHandler = commandHandler;
        _validator = validator;
    }

    public async Task<IEnumerable<TResult>> ExecuteAsync(TCommand command)
    {
        _validator.Validate(
            command,
            options =>
            {
                options.ThrowOnFailures();
                options.IncludeAllRuleSets();
            }
        );
        var response = await _commandHandler.ExecuteAsync(command);
        return response;
    }
}
