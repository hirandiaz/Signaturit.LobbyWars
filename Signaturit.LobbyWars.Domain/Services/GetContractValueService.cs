using Signaturit.LobbyWars.Domain.Entities;
using Signaturit.LobbyWars.Domain.Services.Base;
using Signaturit.LobbyWars.Domain.Specifications;

namespace Signaturit.LobbyWars.Domain.Services
{
    public class GetContractValueService : IGetContractValueService
    {
        /// <summary>
        /// Calculate the points that correspond to a contract based 
        /// on the different roles of the person who signed it.
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>
        ///    Return a positive integer value if is a valid contract,
        ///    otherwise return -1;
        /// </returns>
        public int GetValue(Contract contract)
        {
            IsValidContractSpecification specification = new();

            bool isValidContract = specification.IsSatisfiedBy(contract);

            if (!isValidContract)
                return -1;

            IEnumerable<Signature>? signatures = contract.Signatures;

            if (contract.SignedByKing)
            {
                signatures = signatures.Where(
                                signature => signature.Role != SignatureRole.Validator);
            }

            int contractValue = signatures.Sum(signature => (int)(signature.Role));

            return contractValue;


        }
    }

}
