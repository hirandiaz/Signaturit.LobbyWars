using MediatR;

namespace Signaturit.LobbyWars.Core.Data.Commands.Handlers
{
    /// <summary>
    ///    Handles a command
    /// </summary>
    /// <typeparam name="TCommand"> the command</typeparam>
    public abstract class CommandHandler<TCommand> :
        CommandHandler<TCommand, Unit> where TCommand : Command<Unit>
    {
        protected CommandHandler() : base()
        { }
    }



    /// <summary>
    ///     Handles a command
    /// </summary>
    /// <typeparam name="TCommand"> the command</typeparam>
    /// <typeparam name="TResponse"> the response </typeparam>
    public abstract class CommandHandler<TCommand, TResponse> :
        IRequestHandler<TCommand, TResponse> where TCommand : Command<TResponse>
    {
        public CommandHandler()
        { }

        /// <summary>
        ///  Handle a command
        /// </summary>
        /// <param name="command"> the command</param>
        /// <param name="cancellationToken">the cancellationToken </param>
        /// <returns> Response from the request </returns>
        public abstract Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
