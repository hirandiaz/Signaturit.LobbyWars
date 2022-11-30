using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Core.Data.Queries.Handlers;
using Signaturit.LobbyWars.Domain.Data.Queries;
using Signaturit.LobbyWars.Domain.Entities;
using Signaturit.LobbyWars.Domain.Services.Base;
using Signaturit.LobbyWars.Domain.Specifications;

namespace Signaturit.LobbyWars.Application.Handlers.Queries
{
    public class GetWinningContractQueryHandler : QueryHandler<GetWinningContractQuery, Contract>
    {
        private readonly IGetContractValueService _getContractValueService;

        public GetWinningContractQueryHandler(IGetContractValueService getContractValueService) : base()
        {
            _getContractValueService = getContractValueService;
        }

        public override async Task<QueryResult<Contract>> Handle(GetWinningContractQuery query, CancellationToken cancellationToken)
        {
            if (query.Parameters is not { } || !query.Parameters.Any())
            {
                throw new InvalidOperationException("No parameters found");
            }

            if (query.Parameters.Count == 1 && query.Parameters.TryGetValue("contract1", out var obj) &&
                obj is Contract contract)
            {
                int contractValue = _getContractValueService.GetValue(contract);

                return contractValue < 0 ?
                    new QueryResult<Contract>(Array.Empty<Contract>()) :
                    new QueryResult<Contract>(new[] { contract });
            }

            if (query.Parameters.Count == 2 && query.Parameters.TryGetValue("contract1", out var obj1) &&
                obj1 is Contract contract1 && query.Parameters.TryGetValue("contract2", out var obj2) &&
                obj2 is Contract contract2)
            {
                int contractValue1 = _getContractValueService.GetValue(contract1);
                int contractValue2 = _getContractValueService.GetValue(contract2);

                if (contractValue1 == contractValue2)
                    return new QueryResult<Contract>(new[] { contract1, contract2 });

                return contractValue1 > contractValue2
                    ? new QueryResult<Contract>(new[] { contract1 })
                    : new QueryResult<Contract>(new[] { contract2 });
            }
            throw new InvalidOperationException("only two contracts can be analyzed at most at the same time");
        }
    }
}
