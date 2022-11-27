using Signaturit.LobbyWars.Domain.Entities;

namespace Signaturit.LobbyWars.Domain.Data.Models
{
    public record SignatureCollectionDto(params SignatureRole[] Roles);

}
