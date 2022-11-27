using System.Linq.Expressions;

namespace Signaturit.LobbyWars.Core.Data.Queries
{
    public class OrderBy
    {
        public Expression Field
        {
            get; private set;
        }

        public bool Ascending { get; private set; }

        public OrderBy(Expression field, bool ascending = true)
        {
            Field = field;
            Ascending = ascending;
        }
    }
}
