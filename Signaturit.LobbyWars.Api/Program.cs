using MediatR;
using Signaturit.LobbyWars.Api.Models;
using Signaturit.LobbyWars.Application.Extensions;
using Signaturit.LobbyWars.Application.Services;
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

app.MapPost("/winningContract", async (ContractInputVm inputVm, ISignaturitService signaturitService) =>
{
    var (signatures1, signatures2) = inputVm;

    IEnumerable<char> contractStr = await signaturitService.GetWinningContract(signatures1, signatures2);
    return contractStr is { } ?
    Results.Ok(contractStr) : Results.BadRequest();
})
.WithName("GeWinningContract");

app.MapPost("/minimumSignature", async (ContractInputVm inputVm, ISignaturitService signaturitService) =>
{
    var (signatures1, signatures2) = inputVm;

    char? signatureStr = await signaturitService.GetMinimumSignature(signatures1, signatures2);

    return signatureStr is { } ?
    Results.Ok(signatureStr) : Results.BadRequest();
})
.WithName("GetMinimumSignature");

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