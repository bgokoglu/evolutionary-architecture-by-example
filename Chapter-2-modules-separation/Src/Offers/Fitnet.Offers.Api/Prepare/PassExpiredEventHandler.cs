namespace EvolutionaryArchitecture.Fitnet.Offers.Api.Prepare;

using Common.Core.SystemClock;
using Common.Infrastructure.Events;
using Common.Infrastructure.Events.EventBus;
using DataAccess;
using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Passes.IntegrationEvents;

internal sealed class PassExpiredEventHandler : IIntegrationEventHandler<PassExpiredEvent>
{
    private readonly IEventBus _eventBus;
    private readonly IDbContextFactory<OffersPersistence> _dbContextFactory;
    private readonly ISystemClock _systemClock;

    public PassExpiredEventHandler(
        IEventBus eventBus,
        IDbContextFactory<OffersPersistence> dbContextFactory,
        ISystemClock systemClock)
    {
        _eventBus = eventBus;
        _dbContextFactory = dbContextFactory;
        _systemClock = systemClock;
    }

    public async Task Handle(PassExpiredEvent @event, CancellationToken cancellationToken)
    {
        var offer = Offer.PrepareStandardPassExtension(@event.CustomerId, _systemClock.Now);

        await using var persistence = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await persistence.Offers.AddAsync(offer, cancellationToken);
        await persistence.SaveChangesAsync(cancellationToken);

        var offerPreparedEvent = OfferPrepareEvent.Create(offer.Id, offer.CustomerId);
        await _eventBus.PublishAsync(offerPreparedEvent, cancellationToken);
    }
}
