using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Core.Data.Queries;

namespace Signaturit.LobbyWars.Core.Data.Routers
{
    public interface IQueryRouter
    {
        Task<QueryResult<TProjection>> QueryAsync<TProjection>(Query<TProjection> query);
        Task<TProjection> QueryOneAsync<TProjection>(Query<TProjection> query);
    }
}
