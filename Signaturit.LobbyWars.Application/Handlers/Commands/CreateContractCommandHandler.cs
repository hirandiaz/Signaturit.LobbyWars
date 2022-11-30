using Signaturit.LobbyWars.Core.Data.Commands.Handlers;
using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Domain.Data.Commands;
using Signaturit.LobbyWars.Domain.Data.Models;
using Signaturit.LobbyWars.Domain.Entities;
using Signaturit.LobbyWars.Domain.Factories;

namespace Signaturit.LobbyWars.Application.Handlers.Commands
{
    public class CreateContractCommandHandler : CommandHandler<CreateContractCommand, CommandResult<Contract>>
    {
        public CreateContractCommandHandler() : base()
        { }

        public override async Task<CommandResult<Contract>> Handle(CreateContractCommand command, CancellationToken cancellationToken)
        {
            SignatureCollectionDto? signatureDtos = command.Request;

            IEnumerable<Signature>? signatures = signatureDtos.Roles
                                .Select(role => new SignatureFactory(role).Create());

            Contract contract = new ContractFactory(signatures).Create();

            return await Task.FromResult(new CommandResult<Contract>(contract));
        }
    }
}
