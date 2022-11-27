using Signaturit.LobbyWars.Domain.Entities;

namespace Signaturit.LobbyWars.Domain.Services.Base
{
    public interface IGetContractValueService
    {
        int GetValue(Contract contract);
    }

}
