using Signaturit.LobbyWars.Core.Specification.Operators.Visitor;
using System.Linq.Expressions;

namespace Signaturit.LobbyWars.Core.Specification.Operators.Binary
{
    /// <summary>
    /// 
    /// </summary>
    internal class ExpressionSpecificationAndOperator : IExpressionSpecificationBinaryLogicOperator
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
            Expression<Func<T, bool>> andExpression;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");

            var leftVisitor = new ReplaceExpressionVisitor(left.Expression.Parameters[0], param);
            Expression leftExpression = leftVisitor.Visit(left.Expression.Body);

            var rightVisitor = new ReplaceExpressionVisitor(right.Expression.Parameters[0], param);
            Expression rightExpression = rightVisitor.Visit(right.Expression.Body);

            andExpression = Expression.Lambda<Func<T, bool>>(
                                                Expression.AndAlso(leftExpression,
                                                                    rightExpression), param);

            var andSpecification = new DynamicExpressionSpecification<T>(andExpression);
            return andSpecification;
        }

    }
}