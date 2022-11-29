using MediatR;
using Moq;
using Signaturit.LobbyWars.Application.Handlers.Queries;
using Signaturit.LobbyWars.Core.Data.Routers;
using Signaturit.LobbyWars.Domain.Data.Queries;
using Signaturit.LobbyWars.Domain.Services.Base;
using System;
using System.Threading;
using Xunit;

namespace Signaturit.LobbyWars.Test
{
    public class GeMinimumSignatureQueryShould
    {
        [Fact]
        public async void Handle_IfNumberIsLessThanTwo_ThrowInvalidOperation()
        {
            //Arrange
            Mock<IMediator>? mockMediator = new();

            Mock<IGetContractValueService> mockService = new();

            GetMinimumSignatureQueryHandler queryHandler = new();

            mockMediator
              .Setup(m => m.Send(It.IsAny<GetMinimumSignatureQuery>(), It.IsAny<CancellationToken>()))

              .Returns<GetMinimumSignatureQuery, CancellationToken>(
                      async (query, token) => await queryHandler.Handle(query, token));

            QueryRouter queryRouter = new(mockMediator.Object);

            GetMinimumSignatureQuery query = new();

            //Act
            var action = async () => await queryRouter.QueryOneAsync(query).ConfigureAwait(false);

            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(action);
        }
    }
}