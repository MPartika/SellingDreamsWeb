#pragma warning disable CS8604
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SellingDreamsCommandHandler.Authenticate;
using SellingDreamsCommandHandler.Users;
namespace SellingDreamsCommandHandler;

public static class ConfigureServices
{
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        // Register authentication without logging Decoration
        services.AddTransient(typeof(ICommandHandlerAsync<AuthenticateLoginCommand,AuthenticateLoginCommandResponse>), typeof(AuthenticateLoginCommandHandler));
        services.AddTransient(typeof(ICommandHandlerAsync<CreateLoginCommand>), typeof(CreateLoginCommandHandler));
        services.AddTransient(typeof(ICommandHandlerAsync<DeleteLoginCommand>), typeof(DeleteLoginCommandHandler));
        services.AddTransient(typeof(ICommandHandlerAsync<PatchLoginCommand>), typeof(PatchLoginCommandHandler));

        // Register Services with Validation
        services.RegisterCommandValidationAsync<CreateLoginCommandHandler, CreateLoginCommand, CreateLoginValidator>();

        // Register users with logging Decoration
        services.RegisterCommandHandlerAsync<GetUserCommandHandle, GetUserCommand, GetUserCommandResponse>();
        services.RegisterCommandHandlerAsync<CreateUsersCommandHandler, CreateUsersCommand>();
        services.RegisterCommandHandlerAsync<DeleteUsersCommandHandler, DeleteUsersCommand>();
        services.RegisterCommandHandlerListAsync<GetAllUsersCommandHandler, GetAllUsersCommand, GetAllUsersCommandResponse>();
        services.RegisterCommandHandlerAsync<PatchUsersCommandHandler, PatchUsersCommand>();
        services.RegisterCommandHandlerAsync<UpdateUsersCommandHandler, UpdateUsersCommand>();

        return services;
    }

    private static IServiceCollection RegisterCommandHandlerAsync<TCommandHandler, TCommand, TResult>(
            this IServiceCollection services)
            where TCommand : ICommand
            where TResult : ICommandResponse
            where TCommandHandler : class, ICommandHandlerAsync<TCommand, TResult>
        {

            services.AddTransient<TCommandHandler>();

            services.AddTransient<ICommandHandlerAsync<TCommand, TResult>>(x =>
                new LoggingDecoratorAsync<TCommand, TResult>(x.GetService<TCommandHandler>()));

            return services;
        }

    private static IServiceCollection RegisterCommandHandlerListAsync<TCommandHandler, TCommand, TResult>(
            this IServiceCollection services)
            where TCommand : ICommand
            where TResult : ICommandResponse
            where TCommandHandler : class, ICommandHandlerListAsync<TCommand, TResult>
        {

            services.AddTransient<TCommandHandler>();

            services.AddTransient<ICommandHandlerListAsync<TCommand, TResult>>(x =>
                new LoggingDecoratorListAsync<TCommand, TResult>(x.GetService<TCommandHandler>()));

            return services;
        }

    private static IServiceCollection RegisterCommandHandlerAsync<TCommandHandler, TCommand>(
            this IServiceCollection services)
            where TCommand : ICommand
            where TCommandHandler : class, ICommandHandlerAsync<TCommand>
        {

            services.AddTransient<TCommandHandler>();

            services.AddTransient<ICommandHandlerAsync<TCommand>>(x =>
                new LoggingDecoratorAsync<TCommand>(x.GetService<TCommandHandler>()));

            return services;
        }

    private static IServiceCollection RegisterCommandValidationAsync<TCommandHandler, TCommand, TValidator>(this IServiceCollection services)
        where TCommand : ICommand
        where TCommandHandler : class, ICommandHandlerAsync<TCommand>
        where TValidator: AbstractValidator<TCommand>
    {
        services.AddTransient<TValidator>();
        services.AddTransient<TCommandHandler>();
        services.AddTransient<ICommandHandlerAsync<TCommand>>(x =>
            new ValidationDecoratorAsync<TCommand>(x.GetService<TCommandHandler>(), x.GetService<TValidator>()));
        return services;
    }

    private static IServiceCollection RegisterCommandValidationAsync<TCommandHandler, TCommand, TResult, TValidator>(this IServiceCollection services)
        where TCommand : ICommand
        where TResult: ICommandResponse
        where TCommandHandler : class, ICommandHandlerAsync<TCommand, TResult>
        where TValidator: AbstractValidator<TCommand>
    {
        services.AddTransient<TValidator>();
        services.AddTransient<TCommandHandler>();
        services.AddTransient<ICommandHandlerAsync<TCommand, TResult>>(x =>
            new ValidationDecoratorAsync<TCommand, TResult>(x.GetService<TCommandHandler>(), x.GetService<TValidator>()));
        return services;
    }
}

#pragma warning restore CS8604
