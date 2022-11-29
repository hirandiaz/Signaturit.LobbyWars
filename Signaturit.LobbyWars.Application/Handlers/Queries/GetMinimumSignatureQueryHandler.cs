using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Core.Data.Queries.Handlers;
using Signaturit.LobbyWars.Domain.Data.Queries;
using Signaturit.LobbyWars.Domain.Entities;

namespace Signaturit.LobbyWars.Application.Handlers.Queries
{
    public class GetMinimumSignatureQueryHandler : QueryHandler<GetMinimumSignatureQuery, Signature>
    {
        public GetMinimumSignatureQueryHandler()
        {
        }

        public override async Task<QueryResult<Signature>> Handle(GetMinimumSignatureQuery query, CancellationToken cancellationToken)
        {
            if (query.Parameters is null || query.Parameters.Count < 2)
                throw new InvalidOperationException("parameters not found");

            throw new NotImplementedException();
        }
    }
}
