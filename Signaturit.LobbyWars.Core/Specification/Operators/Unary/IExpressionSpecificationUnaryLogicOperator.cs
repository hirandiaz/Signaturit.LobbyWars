namespace Signaturit.LobbyWars.Core.Specification.Operators.Unary
{
    internal interface IExpressionSpecificationUnaryLogicOperator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        ExpressionSpecification<T> Operate<T>(ExpressionSpecification<T> specification);


    }
}