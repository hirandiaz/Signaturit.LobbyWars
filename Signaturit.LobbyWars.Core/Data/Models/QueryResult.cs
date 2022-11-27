namespace Signaturit.LobbyWars.Core.Data.Models
{
    public class QueryResult<T>
    {
        public long Count { get; protected set; }

        public IEnumerable<T> Items { get; protected set; }

        public string ContinuationToken { get; protected set; }


        /// <summary>
        /// 
        /// </summary>
        protected QueryResult()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="continuationToken"></param>
        public QueryResult(IEnumerable<T> items, string continuationToken = null)
        {
            Items = items;
            Count = Items.Count();
            ContinuationToken = continuationToken;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="count"></param>
        /// <param name="continuationToken"></param>
        public QueryResult(IEnumerable<T> items, long count, string continuationToken = null)
        {
            Items = items;
            Count = count;
            ContinuationToken = continuationToken;
        }
    }
}
