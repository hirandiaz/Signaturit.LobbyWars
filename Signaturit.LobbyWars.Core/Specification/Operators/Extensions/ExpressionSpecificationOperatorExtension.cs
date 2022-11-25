using Signaturit.LobbyWars.Core.Specification.Operators.Binary;
using Signaturit.LobbyWars.Core.Specification.Operators.Unary;

namespace Signaturit.LobbyWars.Core.Specification.Operators.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExpressionSpecificationOperatorExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="specificationLeft"></param>
        /// <param name="specificationRight"></param>
        /// <returns></returns>
        public static ExpressionSpecification<T> And<T>(this ExpressionSpecification<T> specificationLeft, ExpressionSpecification<T> specificationRight)
        {
            var specificationAndOperator = new ExpressionSpecificationAndOperator();
            ExpressionSpecification<T> andSpecification = specificationAndOperator.Operate(specificationLeft, specificationRight);
            return andSpecification;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="specificationLeft"></param>
        /// <param name="specificationRight"></param>
        /// <returns></returns>
        public static ExpressionSpecification<T> Or<T>(this ExpressionSpecification<T> specificationLeft, ExpressionSpecification<T> specificationRight)
        {
            var specificationOrOperator = new ExpressionSpecificationOrOperator();
            ExpressionSpecification<T> orSpecification = specificationOrOperator.Operate(specificationLeft, specificationRight);
            return orSpecification;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        public static ExpressionSpecification<T> Not<T>(this ExpressionSpecification<T> specification)
        {
            var specificationNotOperator = new ExpressionSpecificationNotOperator();
            ExpressionSpecification<T> notSpecification = specificationNotOperator.Operate(specification);
            return notSpecification;

        }
    }
}
