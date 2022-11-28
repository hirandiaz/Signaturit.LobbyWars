using Signaturit.LobbyWars.Domain.Entities;

namespace Signaturit.LobbyWars.Domain.Services.Base
{
    public interface IGetMinimumSigntureService
    {
        SignatureRole GetSigntureRole(Contract contract);
    }

}
