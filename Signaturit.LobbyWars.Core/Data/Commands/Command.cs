using MediatR;

namespace Signaturit.LobbyWars.Core.Data.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class Command<TRequest, TResponse> : Command<TResponse>
    {
        public TRequest Request { get; }

        public Command() : base()
        {
            Request = default;
        }

        public Command(TRequest request) : this()
        {
            Request = request;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public class Command<TResponse> : IRequest<TResponse>
    {
        public virtual string Type { get; protected set; }

        public Command()
        {
            Type = GetType().Name;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Command : Command<Unit>
    {
        public Command() : base()
        {
        }
    }
}

