using MediatR;
using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Core.Specification.Operators.Visitor;
using System.Linq.Expressions;

namespace Signaturit.LobbyWars.Core.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProjection"></typeparam>
    public class Query<TProjection> : IRequest<QueryResult<TProjection>>
    {
        protected List<string> _includes;

        protected string _entityType;

        protected Dictionary<string, object> _parameters;

        protected List<OrderBy> _orderBy;

        public int Skip { get; protected set; }

        public int Take { get; protected set; }

        public Type EntityType
        {
            get
            {
                if (_entityType == null)
                    return null;
                return Type.GetType(_entityType);
            }
            protected set
            {
                _entityType = value.AssemblyQualifiedName;
            }
        }

        public Expression Filter { get; protected set; }

        public Expression Projection { get; protected set; }

        public IReadOnlyList<string> Includes => _includes;

        public IReadOnlyList<OrderBy> OrderBy => _orderBy;

        public IReadOnlyDictionary<string, object> Parameters => _parameters;

        /// <summary>
        /// 
        /// </summary>
        public Query()
        {
            Take = 5000;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Query<TProjection> SetSkip(int value)
        {
            Skip = value;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Query<TProjection> SetTake(int value)
        {
            Take = value;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Query<TProjection> AddParameter(string name, object value)
        {
            if (_parameters == null)
                _parameters = new Dictionary<string, object>();
            _parameters.Add(name, value);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public Query<TProjection> AddInclude(string include)
        {
            if (_includes == null)
                _includes = new List<string>();
            _includes.Add(include);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        public Query<TProjection> AddIncludes(params string[] includes)
        {
            foreach (var include in includes)
            {
                AddInclude(include);
            }
            return this;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProjection"></typeparam>
    public class Query<TEntity, TProjection> : Query<TProjection>
        where TEntity : class
    {
        public Query() : base()
        {
            EntityType = typeof(TEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public new Query<TEntity, TProjection> AddParameter(string name, object value)
        {
            return (Query<TEntity, TProjection>)base.AddParameter(name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public new Query<TEntity, TProjection> AddInclude(string include)
        {
            return (Query<TEntity, TProjection>)base.AddInclude(include);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        public new Query<TEntity, TProjection> AddIncludes(params string[] includes)
        {
            return (Query<TEntity, TProjection>)base.AddIncludes(includes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Query<TEntity, TProjection> AddFilter(Expression<Func<TEntity, bool>> filter)
        {
            if (Filter == null)
            {
                Filter = filter;
                return this;
            }

            LambdaExpression lambda = BuildLambdaExpression();

            Filter = (Expression<Func<TEntity, bool>>)lambda;

            return this;

            LambdaExpression BuildLambdaExpression()
            {
                var filterExpression = (Expression<Func<TEntity, bool>>)Filter;

                var parameter = Expression.Parameter(typeof(TEntity), "t");

                var leftVisitor = new ReplaceExpressionVisitor(filterExpression.Parameters[0], parameter);
                var leftExpression = leftVisitor.Visit(filterExpression.Body);

                var rightVisitor = new ReplaceExpressionVisitor(filter.Parameters[0], parameter);
                Expression rightExpression = rightVisitor.Visit(filter.Body);

                var body = Expression.And(leftExpression, rightExpression);

                return Expression.Lambda(body, parameter);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public new Query<TEntity, TProjection> SetSkip(int value)
        {
            return (Query<TEntity, TProjection>)base.SetSkip(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public new Query<TEntity, TProjection> SetTake(int value)
        {
            return (Query<TEntity, TProjection>)base.SetTake(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projection"></param>
        /// <returns></returns>
        public Query<TEntity, TProjection> SetProjection(Expression<Func<TEntity, TProjection>> projection)
        {
            Projection = projection;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public Query<TEntity, TProjection> AddOrderBy(Expression<Func<TEntity, object>> orderBy, bool ascending = true)
        {
            if (_orderBy == null)
                _orderBy = new List<OrderBy>();
            _orderBy.Add(new OrderBy(orderBy, ascending));
            return this;
        }
    }


}

