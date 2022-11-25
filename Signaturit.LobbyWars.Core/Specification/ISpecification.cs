namespace Signaturit.LobbyWars.Core.Specification
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T obj);
    }
}
