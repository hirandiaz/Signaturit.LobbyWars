namespace Signaturit.LobbyWars.Domain.Entities
{
    public class Contract
    {
        public Contract()
        {
            Signatures = new List<Signature>();
        }

        /// <summary>
        /// Returns the signatures corresponding to this contract
        /// </summary>
        public ICollection<Signature> Signatures { get; private set; }

        /// <summary>
        /// Add a new signature to the contract
        /// </summary>
        /// <param name="signature"></param>
        public void AddSignature(Signature signature)
        {
            Signatures.Add(signature);
        }
    }


}