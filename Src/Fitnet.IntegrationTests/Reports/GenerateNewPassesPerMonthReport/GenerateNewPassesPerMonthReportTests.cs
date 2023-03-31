namespace SuperSimpleArchitecture.Fitnet.IntegrationTests.Reports.GenerateNewPassesPerMonthReport;

using Fitnet.Reports;
using SuperSimpleArchitecture.Fitnet.IntegrationTests.Common.TestEngine;
using SuperSimpleArchitecture.Fitnet.IntegrationTests.Common.TestEngine.Configuration;

public sealed class GenerateNewPassesPerMonthReportTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<DatabaseContainer>
{
    private readonly HttpClient _applicationHttpClient;

    public GenerateNewPassesPerMonthReportTests(WebApplicationFactory<Program> applicationInMemoryFactory,
        DatabaseContainer database) =>
        _applicationHttpClient = applicationInMemoryFactory
            .WithContainerDatabaseConfigured(database.ConnectionString!)
            .CreateClient();

    [Fact]
    public async Task Given_valid_generate_new_report_request_Then_should_return_correct_pdf_report()
    {
        // Arrange
        
        // Act
        var resultReport = await _applicationHttpClient.GetAsync(ReportsApiPaths.GenerateNewReport);

        // Assert
        var stream = await resultReport.Content.ReadAsStreamAsync();
        await Verify(stream);
    }
}