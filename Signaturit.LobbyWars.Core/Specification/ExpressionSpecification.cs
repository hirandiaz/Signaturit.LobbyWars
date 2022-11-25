using System.Linq.Expressions;

namespace Signaturit.LobbyWars.Core.Specification
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ExpressionSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// 
        /// </summary>
        private Func<T, bool> _expressionFunc;
        private Func<T, bool> ExpressionFunc => _expressionFunc ?? (_expressionFunc = Expression.Compile());

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        protected ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<T, bool>> Expression { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsSatisfiedBy(T obj)
        {
            bool result = ExpressionFunc(obj);
            return result;
        }
    }
}
