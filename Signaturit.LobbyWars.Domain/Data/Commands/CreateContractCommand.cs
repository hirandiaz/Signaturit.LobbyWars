using Signaturit.LobbyWars.Core.Data.Commands;
using Signaturit.LobbyWars.Domain.Data.Models;

namespace Signaturit.LobbyWars.Domain.Data.Commands
{
    public class CreateContractCommand : CreateCommand<SignatureCollectionDto>
    {
        public CreateContractCommand(SignatureCollectionDto body) : base(body)
        { }
    }

}
