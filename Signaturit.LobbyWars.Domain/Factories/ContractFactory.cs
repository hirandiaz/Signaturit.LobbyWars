using Signaturit.LobbyWars.Domain.Entities;

namespace Signaturit.LobbyWars.Domain.Factories
{
    public class ContractFactory : IFactory<Contract>
    {
        /// <summary>
        /// Create a new Contract Factory
        /// </summary>
        public ContractFactory()
        { }

        /// <summary>
        /// Create a new Contract
        /// </summary>
        /// <returns></returns>
        public Contract Create() => new Contract();
    }
}
