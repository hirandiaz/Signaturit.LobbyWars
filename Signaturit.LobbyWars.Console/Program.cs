// See https://aka.ms/new-console-template for more information
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Signaturit.LobbyWars.Application.Extensions;
using Signaturit.LobbyWars.Application.Services;
using System.Reflection;




var assemblies = LoadAssemblies();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
        .AddMediatR(assemblies.ToArray(), conf => conf.AsScoped())
        .AddSignaturitServices())
    .Build();


Console.WriteLine("Welcome to LobbyWars Console");
Console.ReadLine();

const string text1 = "Enter the first contract without including spaces between characters";
const string text2 = "Enter the second contract without including spaces between characters";

var case1 = () =>
{
    IEnumerable<char> winningContract = GetWinningContractCase(host.Services).Result;

    if (winningContract is { })
        Console.WriteLine($"The winner is: { new string(winningContract.ToArray())}\n");
    else
        Console.WriteLine("No winning contract found");
};

var case2 = () =>
{
    char? minimalSignature = GetMinimalSignatureCase(host.Services).Result;

    if (minimalSignature is { } && minimalSignature is not '#')
        Console.WriteLine($"The minimal signature is: {minimalSignature}\n");
    else
        Console.WriteLine("The minimal signature was not found.");
};

while (true)
{
    Console.WriteLine(@"Please,
    Press 1 to run 'Get Winning Contract' case,
    Press 2 to run 'Get Minimal Signature' case or any other key to exit ");

    string? key = Console.ReadLine();

    if (key is { })
    {
        switch (key)
        {
            case "1":
                case1();
                break;

            case "2":
                case2();
                break;
            default:
                return;
        }
    }
}



async Task<IEnumerable<char>> GetWinningContractCase(IServiceProvider serviceProvider)
{
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    ISignaturitService signaturitService = serviceProvider.GetService<ISignaturitService>();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    Console.WriteLine(text1);
    IEnumerable<char> contract1 = ReadContract();

    Console.WriteLine(text2);
    IEnumerable<char> contract2 = ReadContract();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
    IEnumerable<char> winningContract = await signaturitService.GetWinningContract(contract1, contract2);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

    return winningContract;
}

async Task<char?> GetMinimalSignatureCase(IServiceProvider serviceProvider)
{
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
    ISignaturitService signaturitService = serviceProvider.GetService<ISignaturitService>();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

    Console.WriteLine(text1);
    IEnumerable<char> contract1 = ReadContract();

    Console.WriteLine(text2);
    IEnumerable<char> contract2 = ReadContract();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
    char? minimalsignature = await signaturitService.GetMinimumSignature(contract1, contract2);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

    return minimalsignature;
}

IEnumerable<char> ReadContract()
{
    IEnumerable<char> contract = Console.ReadLine()
        .ToCharArray()
        .AsEnumerable();

    return contract;

}


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