using MediatR;
using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Core.Data.Queries;

namespace Signaturit.LobbyWars.Core.Data.Routers
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryRouter : IQueryRouter
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public QueryRouter(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<QueryResult<TProjection>> QueryAsync<TProjection>(Query<TProjection> query)
        {
            return await _mediator.Send(query).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProjection"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<TProjection> QueryOneAsync<TProjection>(Query<TProjection> query)
        {
            query.SetTake(2);
            var result = await QueryAsync(query).ConfigureAwait(false);
            if (result.Count > 1)
                throw new ArgumentException("Multiple entities where retrieved in the QueryOneAsync method. This seems to be a BUG in the query definition");
            return result.Items.FirstOrDefault();
        }
    }
}
