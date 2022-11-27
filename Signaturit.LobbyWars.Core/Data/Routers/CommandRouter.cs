using MediatR;
using Signaturit.LobbyWars.Core.Data.Commands;
using Signaturit.LobbyWars.Core.Data.Routers;

namespace DhubSolutions.Core.Domain.Data.Routers
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandRouter : ICommandRouter
    {
        private readonly IMediator _mediator;

        public CommandRouter(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<TResponse> ExecuteAsync<TResponse>(Command<TResponse> command)
        {
            return await _mediator.Send(command).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<TResponse> ExecuteAsync<TRequest, TResponse>(Command<TRequest, TResponse> command)
        {
            return await _mediator.Send(command).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SendAsync<TRequest>(Command<TRequest> command)
        {
            var task = _mediator.Send(command).ConfigureAwait(false);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task SendAsync<TRequest, TResponse>(Command<TRequest, TResponse> command)
        {
            var task = _mediator.Send(command).ConfigureAwait(false);
            return Task.CompletedTask;
        }
    }
}
