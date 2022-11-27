using Signaturit.LobbyWars.Core.Data.Commands;

namespace Signaturit.LobbyWars.Core.Data.Routers
{
    public interface ICommandRouter
    {
        /// <summary>
        /// Executes a command and waits until complete
        /// </summary>
        /// <typeparam name="TResponse">Command result</typeparam>
        /// <param name="command">Command</param>
        /// <returns>The command execution result</returns>
        Task<TResponse> ExecuteAsync<TResponse>(Command<TResponse> command);

        /// <summary>
        /// Executes a command and waits until complete, returning the command result
        /// </summary>
        /// <typeparam name="TRequest">Command body</typeparam>
        /// <typeparam name="TResponse">Command result</typeparam>
        /// <param name="command">Command</param>
        /// <returns>The command execution result</returns>
        Task<TResponse> ExecuteAsync<TRequest, TResponse>(Command<TRequest, TResponse> command);

        /// <summary>
        /// Fire and forget command (Do not wait, runs async)
        /// </summary>
        /// <typeparam name="TRequest">Command body</typeparam>
        /// <param name="command">Command</param>
        /// <returns></returns>
        Task SendAsync<TRequest>(Command<TRequest> command);

        /// <summary>
        /// Fire and forget command (Do not wait, runs async)
        /// </summary>
        /// <typeparam name="TRequest">Command body</typeparam>
        /// <param name="command">Command</param>
        /// <returns></returns>
        Task SendAsync<TRequest, TResponse>(Command<TRequest, TResponse> command);
    }
}
