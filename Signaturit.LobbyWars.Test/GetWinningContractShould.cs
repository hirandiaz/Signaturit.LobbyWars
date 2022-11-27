using MediatR;
using Moq;
using Signaturit.LobbyWars.Application.Handlers.Queries;
using Signaturit.LobbyWars.Core.Data.Routers;
using Signaturit.LobbyWars.Domain.Data.Queries;
using System;
using System.Threading;
using Xunit;

namespace Signaturit.LobbyWars.Test
{
    public class GetWinningContractShould
    {
        [Fact]
        public async void Handle_IfNoPassAnyParameter_ThrowInvalidOperationException()
        {
            //Arrange
            Mock<IMediator>? mockMediator = new();

            GetWinningContractQueryHandler queryHandler = new();

            mockMediator
              .Setup(m => m.Send(It.IsAny<GetWinningContractQuery>(), It.IsAny<CancellationToken>()))

              .Returns<GetWinningContractQuery, CancellationToken>(
                      async (query, token) => await queryHandler.Handle(query, token));

            QueryRouter queryRouter = new(mockMediator.Object);

            GetWinningContractQuery query = new();

            //Act
            var action = async () => await queryRouter.QueryOneAsync(query).ConfigureAwait(false);

            //Assert
           await Assert.ThrowsAsync<InvalidOperationException>(action);

        }
    }
}