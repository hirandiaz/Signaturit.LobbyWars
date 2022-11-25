using System.Linq.Expressions;

namespace Signaturit.LobbyWars.Core.Specification.Operators.Unary
{
    /// <summary>
    /// 
    /// </summary>
    internal class ExpressionSpecificationNotOperator : IExpressionSpecificationUnaryLogicOperator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        public ExpressionSpecification<T> Operate<T>(ExpressionSpecification<T> specification)
        {
            Expression<Func<T, bool>> resultExpression = Expression.Lambda<Func<T, bool>>(
                                                Expression.Not(specification.Expression.Body),
                                                specification.Expression.Parameters[0]);

            var notSpecification = new DynamicExpressionSpecification<T>(resultExpression);
            return notSpecification;

        }
    }
}