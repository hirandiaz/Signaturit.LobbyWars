using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Core.Data.Queries.Handlers;
using Signaturit.LobbyWars.Domain.Data.Queries;
using Signaturit.LobbyWars.Domain.Entities;

namespace Signaturit.LobbyWars.Application.Handlers.Queries
{
    public class GetWinningContractQueryHandler : QueryHandler<GetWinningContractQuery, Contract>
    {
        public override Task<QueryResult<Contract>> Handle(GetWinningContractQuery query, CancellationToken cancellationToken)
        {
            if (query.Parameters is not { } || !query.Parameters.Any())
            {
                throw new InvalidOperationException("No parameters found");
            }

            throw new NotImplementedException();
        }
    }
}
