namespace EvolutionaryArchitecture.Fitnet.Passes.DataAccess.Repositories;

using Database;

public class PassesRepository : IPassesRepository
{
    private readonly PassesPersistence _persistence;

    public PassesRepository(PassesPersistence persistence) =>
        _persistence = persistence;

    public async Task AddAsync(Pass pass, CancellationToken cancellationToken = default)
    {
        await _persistence.Passes.AddAsync(pass, cancellationToken);
        await _persistence.SaveChangesAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default) =>
        await _persistence.SaveChangesAsync(cancellationToken);
}
