namespace Signaturit.LobbyWars.Application.Services
{
    public interface ISignaturitService
    {
        Task<char?> GetMinimumSignature(IEnumerable<char> signatures1, IEnumerable<char> signatures2);
        Task<IEnumerable<char>> GetWinningContract(IEnumerable<char> signatures1, IEnumerable<char> signatures2);
    }
}