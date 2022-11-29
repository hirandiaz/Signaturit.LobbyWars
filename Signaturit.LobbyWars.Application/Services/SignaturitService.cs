using Signaturit.LobbyWars.Application.Mappings;
using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Core.Data.Routers;
using Signaturit.LobbyWars.Domain.Data.Commands;
using Signaturit.LobbyWars.Domain.Data.Models;
using Signaturit.LobbyWars.Domain.Data.Queries;
using Signaturit.LobbyWars.Domain.Entities;

namespace Signaturit.LobbyWars.Application.Services
{
    public class SignaturitService : ISignaturitService
    {
        private readonly ICommandRouter commandRouter;
        private readonly IQueryRouter queryRouter;

        public SignaturitService(ICommandRouter commandRouter, IQueryRouter queryRouter)
        {
            this.commandRouter = commandRouter;
            this.queryRouter = queryRouter;
        }


        public async Task<IEnumerable<char>> GetWinningContract(IEnumerable<char> signatures1, IEnumerable<char> signatures2)
        {
            IEnumerable<SignatureRole> signatureRoles1 = SignaturitMapper.MapToSignatureRole(signatures1);
            IEnumerable<SignatureRole> signaturesRoles2 = SignaturitMapper.MapToSignatureRole(signatures2);

            CreateContractCommand createContract1 = new(new SignatureCollectionDto(signatureRoles1));
            CreateContractCommand createContract2 = new(new SignatureCollectionDto(signaturesRoles2));

            CommandResult<Contract> commandResult1 = await commandRouter.ExecuteAsync(createContract1);
            CommandResult<Contract> commandResult2 = await commandRouter.ExecuteAsync(createContract2);

            if (commandResult1.IsSuccess && commandResult2.IsSuccess)
            {
                Contract contract1 = commandResult1.Item;
                Contract contract2 = commandResult2.Item;

                GetWinningContractQuery query = new();

                query
                .AddParameter("contract1", contract1)
                .AddParameter("contract2", contract2);

                Contract contract = await queryRouter.QueryOneAsync(query).ConfigureAwait(false);

                if (contract is { })
                {
                    IEnumerable<char> signatureStr = SignaturitMapper.MapToChar(contract);

                    return signatureStr;
                }
            }
            return Enumerable.Empty<char>();
        }

        public async Task<char?> GetMinimumSignature(IEnumerable<char> signatures1, IEnumerable<char> signatures2)
        {
            IEnumerable<SignatureRole> signatureRoles1 = SignaturitMapper.MapToSignatureRole(signatures1);
            IEnumerable<SignatureRole> signaturesRoles2 = SignaturitMapper.MapToSignatureRole(signatures2);

            CreateContractCommand createContract1 = new(new SignatureCollectionDto(signatureRoles1));
            CreateContractCommand createContract2 = new(new SignatureCollectionDto(signaturesRoles2));

            CommandResult<Contract> commandResult1 = await commandRouter.ExecuteAsync(createContract1);
            CommandResult<Contract> commandResult2 = await commandRouter.ExecuteAsync(createContract2);

            if (commandResult1.IsSuccess && commandResult2.IsSuccess)
            {
                Contract contract1 = commandResult1.Item;
                Contract contract2 = commandResult2.Item;

                GetMinimumSignatureQuery query = new();

                query
                .AddParameter("contract1", contract1)
                .AddParameter("contract2", contract2);

                Signature signature = await queryRouter.QueryOneAsync(query).ConfigureAwait(false);

                if (signature is { })
                {
                    char signatureStr = SignaturitMapper.MapToChar(signature.Role);

                    return signatureStr;
                }
            }

            return null;
        }
    }
}
