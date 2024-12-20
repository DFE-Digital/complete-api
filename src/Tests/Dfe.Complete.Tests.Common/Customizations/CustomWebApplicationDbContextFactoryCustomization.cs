using AutoFixture;
using DfE.CoreLibs.Testing.Mocks.Authentication;
using DfE.CoreLibs.Testing.Mocks.WebApplicationFactory;
using Dfe.Complete.Api;
using Dfe.Complete.Api.Client.Extensions;
using Dfe.Complete.Client;
using Dfe.Complete.Client.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Security.Claims;
using Dfe.Complete.Infrastructure.Database;
using Dfe.Complete.Tests.Common.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Dfe.Complete.Tests.Common.Customizations
{
    public class CustomWebApplicationDbContextFactoryCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<CustomWebApplicationDbContextFactory<Program>>(composer => composer.FromFactory(() =>
            {
                var factory = new CustomWebApplicationDbContextFactory<Program>()
                {
                    //TODO: when needed, seed data for CompleteContext
                    SeedData = new Dictionary<Type, Action<DbContext>>
                    {
                        //TODO: add this but for CompleteContext when needed:
                        //typeof(CompleteContext), context => CompleteContextSeeder.Seed((CompleteContext)context)
                        { typeof(CompleteContext), context => {} },
                    },
                    ExternalServicesConfiguration = services =>
                    {
                        services.PostConfigure<AuthenticationOptions>(options =>
                        {
                            options.DefaultAuthenticateScheme = "TestScheme";
                            options.DefaultChallengeScheme = "TestScheme";
                        });

                        services.AddAuthentication("TestScheme")
                            .AddScheme<AuthenticationSchemeOptions, MockJwtBearerHandler>("TestScheme", options => { });
                    },
                    ExternalHttpClientConfiguration = client =>
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "external-mock-token");
                    }
                };

                var client = factory.CreateClient();

                var config = new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        { "CompleteApiClient:BaseUrl", client.BaseAddress!.ToString() }
                    })
                    .Build();

                var services = new ServiceCollection();
                services.AddSingleton<IConfiguration>(config);
                
                services.AddCompleteApiClient<ICreateProjectClient, CreateProjectClient>(config, client);
                
                var serviceProvider = services.BuildServiceProvider();
                
                fixture.Inject(factory);
                fixture.Inject(serviceProvider);
                fixture.Inject(client);
                fixture.Inject(serviceProvider.GetRequiredService<ICreateProjectClient>());

                fixture.Inject(new List<Claim>());

                return factory;
            }));
        }
    }
}
