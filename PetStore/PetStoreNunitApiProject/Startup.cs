using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace PetStoreNunitApiProject
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRestLibrary, RestLibrary>();
            services.AddScoped<IRestBuilder, RestBuilder>();
            services.AddScoped<IRestFactory, RestFactory>();

        }
    }
}
