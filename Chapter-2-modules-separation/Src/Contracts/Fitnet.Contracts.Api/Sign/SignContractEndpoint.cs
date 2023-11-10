namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.Sign;

using Application;
using Common.Api.Validation.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;

internal static class SignContractEndpoint
{
    internal static void MapSignContract(this IEndpointRouteBuilder app) => app.MapPatch(ContractsApiPaths.Sign, async (
            Guid id,
            SignContractRequest request,
            IContractsModule contractsModule, CancellationToken cancellationToken) =>
        {
            var command = request.ToCommand(id);
            await contractsModule.ExecuteCommandAsync(command, cancellationToken);

            return Results.NoContent();
        })
        .ValidateRequest<SignContractRequest>()
        .WithOpenApi(operation => new OpenApiOperation(operation)
        {
            Summary = "Signs prepared contract",
            Description =
                "This endpoint is used to sign prepared contract by customer.",
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
