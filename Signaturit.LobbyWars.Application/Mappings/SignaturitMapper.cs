using Signaturit.LobbyWars.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signaturit.LobbyWars.Application.Mappings
{
    public static class SignaturitMapper
    {
        public static SignatureRole MapToSignatureRole(char signatureStr)
        {
            return signatureStr switch
            {
                'N' => SignatureRole.Notary,
                'n' => SignatureRole.Notary,
                'V' => SignatureRole.Validator,
                'v' => SignatureRole.Validator,
                'K' => SignatureRole.King,
                'k' => SignatureRole.King,
                '#' => SignatureRole.Missing,
                _ => throw new ArgumentOutOfRangeException($"{signatureStr} is no valid value"),
            };
        }

        public static char MapToChar(SignatureRole signatureRole)
        {
            return signatureRole switch
            {
                SignatureRole.Missing => '#',
                SignatureRole.Validator => 'V',
                SignatureRole.Notary => 'N',
                SignatureRole.King => 'K',
                _ => throw new ArgumentOutOfRangeException($"{signatureRole} is no valid value"),
            };
        }

        public static IEnumerable<SignatureRole> MapToSignatureRole(IEnumerable<char> singnatures)
            => singnatures.Select(signature => MapToSignatureRole(signature));
        public static IEnumerable<char> MapToChar(IEnumerable<SignatureRole> signatureRoles)
            => signatureRoles.Select(signatureRole => MapToChar(signatureRole));

        public static IEnumerable<char> MapToChar(Contract contract)
            => MapToChar(contract.Signatures.Select(signature => signature.Role));
    }
}
