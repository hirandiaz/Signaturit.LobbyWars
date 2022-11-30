using Signaturit.LobbyWars.Domain.Entities;

namespace Signaturit.LobbyWars.Domain.Factories
{
    public class ContractFactory : IFactory<Contract>
    {
        private readonly IEnumerable<Signature> _signatures;

        /// <summary>
        /// Create a new Contract Factory
        /// </summary>
        public ContractFactory(IEnumerable<Signature> signatures)
        {
            if (signatures == null)
                throw new ArgumentNullException(nameof(signatures));

            if (!signatures.Any())
                throw new ArgumentException("It is not possible to obtain a contract without at least one signature");

            _signatures = signatures;
        }

        /// <summary>
        /// Create a new Contract
        /// </summary>
        /// <returns></returns>
        public Contract Create()
        {
            Contract contract = new();
            foreach (var signature in _signatures)
            {
                contract.AddSignature(signature);
            }
            return contract;
        }
    }
}
