using System.Net;
using Dfe.Complete.Client.Contracts;
using Dfe.Complete.Tests.Common.Customizations;
using Dfe.Complete.Tests.Common.Customizations.Commands;
using DfE.CoreLibs.Testing.AutoFixture.Attributes;
using DfE.CoreLibs.Testing.Mocks.WebApplicationFactory;

namespace Dfe.Complete.Api.Tests.Integration.Controllers;

public class ProjectsControllerTests
{
    [Theory]
    [CustomAutoData(typeof(CustomWebApplicationDbContextFactoryCustomization),
        typeof(CreateConversionProjectCommandCustomization))]
    public async Task CreateProject_Async_ShouldCreateConversionProject(
        CustomWebApplicationDbContextFactory<Program> factory,
        CreateConversionProjectCommand createConversionProjectCommand,
        ICreateProjectClient createProjectClient)
    {
        //todo: when auth is done, add this back in
        // factory.TestClaims = [new Claim(ClaimTypes.Role, "API.Write")];

        var result = await createProjectClient.Projects_CreateProject_Async(createConversionProjectCommand);

        Assert.NotNull(result);
        Assert.IsType<ProjectId>(result);
    }

    [Theory]
    [CustomAutoData(typeof(CustomWebApplicationDbContextFactoryCustomization))]
    public async Task CreateProject_WithNullRequest_ThrowsException(
        CustomWebApplicationDbContextFactory<Program> factory,
        CreateConversionProjectCommand createConversionProjectCommand,
        ICreateProjectClient createProjectClient)
    {
        //todo: when auth is done, add this back in
        // factory.TestClaims = [new Claim(ClaimTypes.Role, "API.Write")];

        createConversionProjectCommand.Urn = null;


        //todo: change exception type? 
        var exception = await Assert.ThrowsAsync<PersonsApiException>(async () =>
            await createProjectClient.Projects_CreateProject_Async(createConversionProjectCommand));
        
        Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)exception.StatusCode);
    }
}