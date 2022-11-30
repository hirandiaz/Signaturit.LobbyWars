using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Core.Data.Queries.Handlers;
using Signaturit.LobbyWars.Domain.Data.Queries;
using Signaturit.LobbyWars.Domain.Entities;
using Signaturit.LobbyWars.Domain.Services;
using Signaturit.LobbyWars.Domain.Services.Base;

namespace Signaturit.LobbyWars.Application.Handlers.Queries
{
    public class GetMinimumSignatureQueryHandler : QueryHandler<GetMinimumSignatureQuery, Signature>
    {
        private readonly IGetContractValueService _getContractValueService;
        private readonly IGetMinimumSigntureService _getMinimumSigntureService;

        public GetMinimumSignatureQueryHandler(
            IGetContractValueService getContractValueService,
            IGetMinimumSigntureService getMinimumSigntureService)
        {
            _getContractValueService = getContractValueService;
            _getMinimumSigntureService = getMinimumSigntureService;
        }

        public override async Task<QueryResult<Signature>> Handle(GetMinimumSignatureQuery query, CancellationToken cancellationToken)
        {
            if (query.Parameters is null || query.Parameters.Count < 2)
                throw new InvalidOperationException("parameters not found");

            if (query.Parameters.Count == 2 &&
                query.Parameters.TryGetValue("contract1", out var obj1) && obj1 is Contract contract1 &&
                query.Parameters.TryGetValue("contract2", out var obj2) && obj2 is Contract contract2)
            {

                (contract1, contract2) = Swap(contract1, contract2);

                int contractValue2 = _getContractValueService.GetValue(contract2);

                SignatureRole signatureRole = SignatureRole.Missing;
                try
                {
                    signatureRole = _getMinimumSigntureService.GetSigntureRole(contract1, contractValue2);
                }
                catch (InvalidCastException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return new QueryResult<Signature>(new[] { new Signature(signatureRole) });
            }

            throw new InvalidOperationException("only two contracts can be analyzed at most at the same time");
        }

        private (Contract, Contract) Swap(Contract contract1, Contract contract2)
        {
            if (contract1.MissingSignature)
                return (contract1, contract2);

            if (contract2.MissingSignature)
                return (contract2, contract1);

            throw new InvalidOperationException("No missing signature found");
        }
    }
}
