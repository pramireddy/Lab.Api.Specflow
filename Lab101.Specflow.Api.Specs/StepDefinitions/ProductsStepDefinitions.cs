using Lab101.Specflow.Api.Models;
using Lab101.Specflow.Api.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace Lab101.Specflow.Api.Specs.StepDefinitions
{
    [Binding]
    public class ProductsStepDefinitions
    {
        private HttpClient _httpClient;
        private HttpResponseMessage _httpResponseMessage;
        private readonly ScenarioContext scenarioContext;

        public ProductsStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }


        [Given(@"product Details :")]
        public void GivenProductDetails(Table table)
        {
            var products = table.CreateSet<Product>();
            var appFactory = new CustomWebApplicationFactory(MockProductRepository(products));
            _httpClient = appFactory.CreateDefaultClient(new Uri($"https://localhost:5000/"));

        }

        [When(@"a call is made to '([^']*)'")]
        public async Task WhenACallIsMadeTo(string p0)
        {
            _httpResponseMessage = await _httpClient.GetAsync(p0);
            scenarioContext["httpResponse"] = await _httpResponseMessage?.Content?.ReadFromJsonAsync<List<Product>>();
        }

        [Then(@"display list of products :")]
        public void ThenDisplayListOfProducts(Table table)
        {
            var expectedResult = table.CreateSet<Product>();
            var actualResult = scenarioContext["httpResponse"] as List<Product>;
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        private static Action<IServiceCollection> MockProductRepository(IEnumerable<Product> products)
        {
            return services =>
            {
                services.Replace(ServiceDescriptor.Scoped(_ =>
                {
                    var productRepositoryMock = new Mock<IProductRepository>();
                    productRepositoryMock.Setup(x => x.GetAllProducts()).Returns(products);
                    return productRepositoryMock.Object;
                }));
            };

        }
    }
}
