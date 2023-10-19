namespace EvolutionaryArchitecture.Fitnet.Passes.DataAccess.Repositories;

public interface IPassesRepository
{
    Task AddAsync(Pass pass, CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
}
