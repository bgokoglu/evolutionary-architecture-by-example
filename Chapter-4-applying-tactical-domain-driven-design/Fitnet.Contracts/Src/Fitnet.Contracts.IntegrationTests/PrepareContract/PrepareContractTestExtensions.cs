﻿namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.PrepareContract;

using Api;
using Api.Prepare;

internal static class PrepareContractTestExtensions
{
    public static async Task<Guid> PrepareContractAsync(this HttpClient httpClient)
    {
        var requestParameters = PrepareContractRequestParameters.GetValid();
        PrepareContractRequest prepareContractRequest = new PrepareContractRequestFaker(requestParameters.MinAge,
            requestParameters.MaxAge, requestParameters.MinHeight, requestParameters.MaxHeight);

        var prepareContractResponse = await httpClient.PostAsJsonAsync(ContractsApiPaths.Prepare, prepareContractRequest);

        prepareContractResponse.EnsureSuccessStatusCode();

        var preparedContractId = await prepareContractResponse.Content.ReadFromJsonAsync<Guid>();

        return preparedContractId;
    }
}
