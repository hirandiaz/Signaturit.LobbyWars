namespace Signaturit.LobbyWars.Domain.Entities
{
    public class Contract
    {
        private readonly ICollection<Signature> _signatures;
        private bool _missingSignature;

        public Contract()
        {
            _missingSignature = false;
            _signatures = new List<Signature>();
        }

        /// <summary>
        /// Return true if this contract is signed by king role,
        /// otherwise return false.
        /// </summary>
        public bool SignedByKing { get; private set; }

        /// <summary>
        /// Returns the signatures corresponding to this contract
        /// </summary>
        public IEnumerable<Signature> Signatures => _signatures;

        /// <summary>
        /// Add a new signature to the contract
        /// </summary>
        /// <param name="signature"></param>
        public void AddSignature(Signature signature)
        {
            if (signature.Role == SignatureRole.Missing)
            {
                // check if the contract has already a missing signature
                if (_missingSignature)
                    throw new InvalidOperationException("Only one missing signature is allowed.");

                _missingSignature = true;
            }

            if (signature.Role == SignatureRole.King)
                SignedByKing = true;

            _signatures.Add(signature);
        }

    }


}