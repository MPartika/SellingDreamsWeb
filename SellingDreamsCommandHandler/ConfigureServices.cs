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
        services.AddTransient<ICommandHandlerAsync<AuthenticateLoginCommand, AuthenticateLoginCommandResponse>, AuthenticateLoginCommandHandler>();
        services.AddTransient<ICommandHandlerAsync<CreateLoginCommand>, CreateLoginCommandHandler>()
        .Decorate<ICommandHandlerAsync<CreateLoginCommand>, CreateLoginCommand, CreateLoginValidator, ValidationDecoratorAsync<CreateLoginCommand>>();
        services.AddTransient<ICommandHandlerAsync<DeleteLoginCommand>, DeleteLoginCommandHandler>();
        services.AddTransient<ICommandHandlerAsync<PatchLoginCommand>, PatchLoginCommandHandler>()
        .Decorate<ICommandHandlerAsync<PatchLoginCommand>, PatchLoginCommand, PatchLoginValidator, ValidationDecoratorAsync<PatchLoginCommand>>();

        // Register User services
        services.AddTransient<ICommandHandlerAsync<GetUserCommand, GetUserCommandResponse>, GetUserCommandHandle>()
        .Decorate<ICommandHandlerAsync<GetUserCommand, GetUserCommandResponse>, LoggingDecoratorAsync<GetUserCommand, GetUserCommandResponse>>();

        services.AddTransient<ICommandHandlerAsync<CreateUsersCommand>, CreateUsersCommandHandler>()
        .Decorate<ICommandHandlerAsync<CreateUsersCommand>, CreateUsersCommand,CreateUsersValidator, ValidationDecoratorAsync<CreateUsersCommand>>()
        .Decorate<ICommandHandlerAsync<CreateUsersCommand>, LoggingDecoratorAsync<CreateUsersCommand>>();

        services.AddTransient<ICommandHandlerAsync<DeleteUsersCommand>, DeleteUsersCommandHandler>()
        .Decorate<ICommandHandlerAsync<DeleteUsersCommand>, LoggingDecoratorAsync<DeleteUsersCommand>>();

        services.AddTransient<ICommandHandlerListAsync<GetAllUsersCommand, GetAllUsersCommandResponse>, GetAllUsersCommandHandler>()
        .Decorate<ICommandHandlerListAsync<GetAllUsersCommand, GetAllUsersCommandResponse>, LoggingDecoratorListAsync<GetAllUsersCommand, GetAllUsersCommandResponse>>();
        
        services.AddTransient<ICommandHandlerAsync<PatchUsersCommand>, PatchUsersCommandHandler>()
        .Decorate<ICommandHandlerAsync<PatchUsersCommand>, PatchUsersCommand, PatchUsersValidator, ValidationDecoratorAsync<PatchUsersCommand>>()
        .Decorate<ICommandHandlerAsync<PatchUsersCommand>, LoggingDecoratorAsync<PatchUsersCommand>>();
        
        services.AddTransient<ICommandHandlerAsync<UpdateUsersCommand>, UpdateUsersCommandHandler>()
        .Decorate<ICommandHandlerAsync<UpdateUsersCommand>, UpdateUsersCommand, UpdateUsersValidator, ValidationDecoratorAsync<UpdateUsersCommand>>()
        .Decorate<ICommandHandlerAsync<UpdateUsersCommand>, LoggingDecoratorAsync<UpdateUsersCommand>>();


        return services;
    }

    private static IServiceCollection Decorate<TInterface, TDecorator>(this IServiceCollection services)
    where TInterface : class
    where TDecorator : class, TInterface
    {
        var objectFactory = ActivatorUtilities.CreateFactory(
            typeof(TDecorator),
            new[] { typeof(TInterface) });

        // Save all descriptors that needs to be decorated into a list.
        var descriptorsToDecorate = services
            .Where(s => s.ServiceType == typeof(TInterface))
            .ToList();

        if (descriptorsToDecorate.Count == 0)
        {
            throw new InvalidOperationException($"Attempted to Decorate services of type {typeof(TInterface)}, " +
                                                "but no such services are present in ServiceCollection");
        }

        foreach (var descriptor in descriptorsToDecorate)
        {
            // Create new descriptor with prepared object factory.
            var decorated = ServiceDescriptor.Describe(
                typeof(TInterface),
                s => (TInterface)objectFactory(s, new[] { s.CreateInstance(descriptor) }),
                descriptor.Lifetime);

            services.Remove(descriptor);
            services.Add(decorated);
        }

        return services;
    }

    private static IServiceCollection Decorate<TInterface, TCommand, TValidator, TDecorator >(this IServiceCollection services)
    where TInterface : class
    where TCommand: ICommand
    where TValidator : AbstractValidator<TCommand>
    where TDecorator : class, TInterface
    {
        services.AddTransient<IValidator<TCommand>, TValidator >();
        var objectFactory = ActivatorUtilities.CreateFactory(
            typeof(TDecorator),
            new[] { typeof(TInterface), typeof(TValidator) });

        // Save all descriptors that needs to be decorated into a list.
        var descriptorsToDecorate = services
            .Where(s => s.ServiceType == typeof(TInterface))
            .ToList();
        var validator = services.FirstOrDefault(s => s.ServiceType == typeof(IValidator<TCommand>));

        if (descriptorsToDecorate.Count == 0)
            throw new InvalidOperationException($"Attempted to Decorate services of type {typeof(TInterface)}, " +
                                                "but no such services are present in ServiceCollection");
        if (validator is null)
            throw new InvalidOperationException($"Attempted to find validator for command {typeof(TCommand)} but " +
                                                $"no such validator {typeof(TValidator)} exits");
        foreach (var descriptor in descriptorsToDecorate) 
        {
            // Create new descriptor with prepared object factory.
            var decorated = ServiceDescriptor.Describe(
                typeof(TInterface),
                s => (TInterface)objectFactory(s, new[] { s.CreateInstance(descriptor), s.CreateInstance(validator) }),
                descriptor.Lifetime);

            services.Remove(descriptor);
            services.Add(decorated);
        }

        return services;
    }

    private static object CreateInstance(this IServiceProvider services, ServiceDescriptor descriptor)
    {
        if (descriptor.ImplementationInstance != null)
        {
            return descriptor.ImplementationInstance;
        }

        if (descriptor.ImplementationFactory != null)
        {
            return descriptor.ImplementationFactory(services);
        }

        return ActivatorUtilities.GetServiceOrCreateInstance(services, descriptor.ImplementationType);
    }
}

#pragma warning restore CS8604
