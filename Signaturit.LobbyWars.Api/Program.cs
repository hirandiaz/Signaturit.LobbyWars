using MediatR;
using Signaturit.LobbyWars.Api;
using Signaturit.LobbyWars.Api.Models;
using Signaturit.LobbyWars.Application.Mappings;
using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Core.Data.Routers;
using Signaturit.LobbyWars.Domain.Data.Commands;
using Signaturit.LobbyWars.Domain.Data.Models;
using Signaturit.LobbyWars.Domain.Data.Queries;
using Signaturit.LobbyWars.Domain.Entities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var assemblies = LoadAssemblies();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddMediatR(assemblies.ToArray(), conf => conf.AsScoped())
    .AddSignaturitServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/winningContract", async (ContractInputVm inputVm, ICommandRouter commandRouter, IQueryRouter queryRouter) =>
{
    var (signatures1, signatures2) = inputVm;

    IEnumerable<SignatureRole> signatureRoles1 = SignaturitMapper.MapToSignatureRole(signatures1);
    IEnumerable<SignatureRole> signaturesRoles2 = SignaturitMapper.MapToSignatureRole(signatures2);

    CreateContractCommand createContract1 = new(new SignatureCollectionDto(signatureRoles1));
    CreateContractCommand createContract2 = new(new SignatureCollectionDto(signaturesRoles2));

    CommandResult<Contract> commandResult1 = await commandRouter.ExecuteAsync(createContract1);
    CommandResult<Contract> commandResult2 = await commandRouter.ExecuteAsync(createContract2);

    if (commandResult1.IsSuccess && commandResult2.IsSuccess)
    {
        Contract contract1 = commandResult1.Item;
        Contract contract2 = commandResult2.Item;

        GetWinningContractQuery query = new();

        query
        .AddParameter("contract1", contract1)
        .AddParameter("contract2", contract2);

        Contract contract = await queryRouter.QueryOneAsync(query).ConfigureAwait(false);

        if (contract is { })
        {
            IEnumerable<char> contractStr = SignaturitMapper.MapToChar(contract);

            return Results.Ok(contractStr);
        }
    }
    return Results.BadRequest();
})
.WithName("GeWinningContract");

app.Run();


#region Load Assemblies

IEnumerable<Assembly> LoadAssemblies()
{
    Assembly entryAssembly = Assembly.GetEntryAssembly();
    List<Assembly> assemblies = new() { entryAssembly };
    assemblies.AddRange(entryAssembly.GetReferencedAssemblies()
           .Select(assemblyName => Assembly.Load(assemblyName)));
    return assemblies;

}

#endregion