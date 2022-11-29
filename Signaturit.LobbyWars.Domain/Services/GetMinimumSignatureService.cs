using Signaturit.LobbyWars.Domain.Entities;
using Signaturit.LobbyWars.Domain.Services.Base;

namespace Signaturit.LobbyWars.Domain.Services
{
    public class GetMinimumSignatureService : IGetMinimumSigntureService
    {
        private readonly IGetContractValueService _getContractValueService;


        public GetMinimumSignatureService(IGetContractValueService getContractValueService)
        {
            _getContractValueService = getContractValueService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="opponentContractValue"></param>
        /// <returns></returns>
        public SignatureRole GetSigntureRole(Contract contract, int opponentContractValue)
        {
            if (!contract.MissingSignature)
                throw new ArgumentException("No missing signature found");

            IEnumerable<SignatureRole> signatureRoles = new[] {
                SignatureRole.King,
                SignatureRole.Notary,
                SignatureRole.Validator };

            int partialContractValue = _getContractValueService.GetValue(contract);

            int minValue = int.MaxValue;

            foreach (SignatureRole signatureRole in signatureRoles)
            {
                int signatureRoleValue = (int)signatureRole;
                if (signatureRoleValue < minValue && signatureRoleValue + partialContractValue > opponentContractValue)
                {
                    minValue = signatureRoleValue;
                }
            }

            return (SignatureRole)minValue;
        }
    }


}
