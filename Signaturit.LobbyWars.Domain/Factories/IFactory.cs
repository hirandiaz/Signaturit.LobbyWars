namespace Signaturit.LobbyWars.Domain.Factories
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
