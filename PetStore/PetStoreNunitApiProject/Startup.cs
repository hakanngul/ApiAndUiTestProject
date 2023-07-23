using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace PetStoreNunitApiProject {
    public class Startup {

        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<IRestLibrary, RestLibrary>();
            services.AddScoped<IRestBuilder, RestBuilder>();
            services.AddScoped<IRestFactory, RestFactory>();
            services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options => {
                options.Authority = "https://petstore.swagger.io/oauth/authorize";
                options.ClientId = "test";
                options.ResponseType = OpenIdConnectResponseType.IdTokenToken;
                options.Scope.Add("read:pets");
                options.Scope.Add("write:pets");
                options.SaveTokens = true;
                options.CallbackPath = "/signin-oidc"; // Geri dönüş URL'si
            });
        }
    }
}
