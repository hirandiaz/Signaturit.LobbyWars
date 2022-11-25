using Signaturit.LobbyWars.Core.Specification.Operators.Visitor;
using System.Linq.Expressions;

namespace Signaturit.LobbyWars.Core.Specification.Operators.Binary
{
    internal class ExpressionSpecificationOrOperator : IExpressionSpecificationBinaryLogicOperator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public ExpressionSpecification<T> Operate<T>(ExpressionSpecification<T> left, ExpressionSpecification<T> right)
        {
            Expression<Func<T, bool>> orExpression;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");

            var leftVisitor = new ReplaceExpressionVisitor(left.Expression.Parameters[0], param);
            var leftExpression = leftVisitor.Visit(left.Expression.Body);

            var rightVisitor = new ReplaceExpressionVisitor(right.Expression.Parameters[0], param);
            var rightExpression = rightVisitor.Visit(right.Expression.Body);

            orExpression = Expression.Lambda<Func<T, bool>>(
                                                Expression.OrElse(leftExpression,
                                                                    rightExpression), param);

            var orSpecification = new DynamicExpressionSpecification<T>(orExpression);
            return orSpecification;
        }

    }
}