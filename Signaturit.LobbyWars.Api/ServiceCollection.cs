using Signaturit.LobbyWars.Application.Services;
using Signaturit.LobbyWars.Core.Data.Routers;
using Signaturit.LobbyWars.Domain.Services;
using Signaturit.LobbyWars.Domain.Services.Base;

namespace Signaturit.LobbyWars.Api
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddSignaturitServices(this IServiceCollection services)
        {
            services
                .AddScoped<ICommandRouter, CommandRouter>()
                .AddScoped<IQueryRouter, QueryRouter>()
                .AddScoped<IGetContractValueService, GetContractValueService>()
                .AddScoped<IGetMinimumSigntureService, GetMinimumSignatureService>()
                .AddScoped<ISignaturitService, SignaturitService>();

            return services;
        }
    }
}
