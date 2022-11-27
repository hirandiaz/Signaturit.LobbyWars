using MediatR;
using Signaturit.LobbyWars.Core.Data.Models;

namespace Signaturit.LobbyWars.Core.Data.Queries.Handlers
{
    public abstract class QueryHandler<TQuery, TProjection> : IRequestHandler<TQuery, QueryResult<TProjection>>
        where TQuery : Query<TProjection>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public abstract Task<QueryResult<TProjection>> Handle(TQuery query, CancellationToken cancellationToken);


    }
}
