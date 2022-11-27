using Signaturit.LobbyWars.Core.Data.Commands;
using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Domain.Data.Models;
using Signaturit.LobbyWars.Domain.Entities;

namespace Signaturit.LobbyWars.Domain.Data.Commands
{
    public class CreateContractCommand : Command<SignatureCollectionDto, CommandResult<Contract>>
    {
        public CreateContractCommand(SignatureCollectionDto body) : base(body)
        { }
    }

}
