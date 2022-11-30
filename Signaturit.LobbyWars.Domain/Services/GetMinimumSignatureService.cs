using Signaturit.LobbyWars.Domain.Entities;
using Signaturit.LobbyWars.Domain.Factories;
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

            //load signature's contract
            IEnumerable<Signature> signatures = contract.Signatures
                        .Where(signature => signature.Role != SignatureRole.Missing);

            int minValue = int.MaxValue;

            foreach (SignatureRole signatureRole in signatureRoles)
            {
                // create dummy signature collection
                List<Signature> dummySignatures = new(signatures);

                Signature dummySignature = new SignatureFactory(signatureRole).Create();

                dummySignatures.Add(dummySignature);

                //create dummy contract 
                Contract dummyContract = new ContractFactory(dummySignatures).Create();

                int dummyContractValue = _getContractValueService.GetValue(dummyContract);

                int signatureRoleValue = (int)signatureRole;
                if (signatureRoleValue < minValue && dummyContractValue > opponentContractValue)
                {
                    minValue = signatureRoleValue;
                }
            }

            //if can't get the minimum signature, return the missing signature,
            // otherwise, return the minimal signature. 
            return minValue == int.MaxValue ?
                SignatureRole.Missing : (SignatureRole)minValue;
        }
    }


}
