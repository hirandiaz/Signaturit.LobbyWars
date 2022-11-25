namespace Signaturit.LobbyWars.Core.Specification.Operators.Binary
{
    public interface IExpressionSpecificationBinaryLogicOperator

    {
        ExpressionSpecification<T> Operate<T>(ExpressionSpecification<T> left, ExpressionSpecification<T> right);
    }
}
