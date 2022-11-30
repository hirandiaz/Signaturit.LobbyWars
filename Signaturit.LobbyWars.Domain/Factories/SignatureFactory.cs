using Signaturit.LobbyWars.Domain.Entities;

namespace Signaturit.LobbyWars.Domain.Factories
{
    public class SignatureFactory : IFactory<Signature>
    {
        private readonly SignatureRole _role;

        /// <summary>
        /// Create a new Siganture Factory
        /// </summary>
        /// <param name="role"></param>
        public SignatureFactory(SignatureRole role)
        {
            _role = role;
        }

        /// <summary>
        /// Create a new Signature
        /// </summary>
        /// <returns></returns>
        public Signature Create() => new(_role);
    }
}
