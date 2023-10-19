namespace EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

using Common.Infrastructure.Events;
using Common.Infrastructure.Events.EventBus;
using Contracts.IntegrationEvents;
using DataAccess;
using DataAccess.Database;
using Microsoft.EntityFrameworkCore;

internal sealed class ContractSignedEventHandler : IIntegrationEventHandler<ContractSignedEvent>
{
    private readonly IDbContextFactory<PassesPersistence> _dbContextFactory;
    private readonly IEventBus _eventBus;

    public ContractSignedEventHandler(IDbContextFactory<PassesPersistence> dbContextFactory, IEventBus eventBus)
    {
        _dbContextFactory = dbContextFactory;
        _eventBus = eventBus;
    }

    public async Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken)
    {
        var pass = Pass.Register(@event.ContractCustomerId, @event.SignedAt, @event.ExpireAt);
        
        await using var persistence = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await persistence.Passes.AddAsync(pass, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);
        
        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        await _eventBus.PublishAsync(passRegisteredEvent, cancellationToken);
    }
}
