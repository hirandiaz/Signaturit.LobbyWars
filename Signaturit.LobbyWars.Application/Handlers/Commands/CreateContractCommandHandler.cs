using Signaturit.LobbyWars.Core.Data.Commands.Handlers;
using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Domain.Data.Commands;

namespace Signaturit.LobbyWars.Application.Handlers.Commands
{
    public class CreateContractCommandHandler : CommandHandler<CreateContractCommand, CommandResult>
    {
        public CreateContractCommandHandler() : base()
        { }

        public override Task<CommandResult> Handle(CreateContractCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
