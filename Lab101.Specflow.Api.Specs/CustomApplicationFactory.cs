using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Lab101.Specflow.Api.Specs
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {

        private readonly Action<IServiceCollection> _overrideDependencies;

        public CustomWebApplicationFactory(Action<IServiceCollection> overrideDependencies = null) => _overrideDependencies = overrideDependencies;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => _overrideDependencies?.Invoke(services));
            builder.UseEnvironment("Development");
        }
    }
}