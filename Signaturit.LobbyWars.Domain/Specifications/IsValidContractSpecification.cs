using Signaturit.LobbyWars.Core.Specification;
using Signaturit.LobbyWars.Domain.Entities;

namespace Signaturit.LobbyWars.Domain.Specifications
{
    public class IsValidContractSpecification : ISpecification<Contract>
    {

        /// <summary>
        ///  IsSatisfiedBy
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>
        /// Return true if a number of 'missing signature' is zero or one; 
        /// otherwise return false.
        /// </returns>
        public bool IsSatisfiedBy(Contract contract)
        {
            if (contract is not { } || !contract.Signatures.Any())
                return false;

            var count = contract.Signatures
                            .Where(signature => signature.Role == SignatureRole.Missing)
                            .Count();

            return count <= 1;
        }
    }
}
