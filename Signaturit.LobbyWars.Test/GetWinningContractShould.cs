using MediatR;
using Moq;
using Signaturit.LobbyWars.Application.Handlers.Queries;
using Signaturit.LobbyWars.Core.Data.Routers;
using Signaturit.LobbyWars.Domain.Data.Queries;
using Signaturit.LobbyWars.Domain.Entities;
using Signaturit.LobbyWars.Domain.Factories;
using Signaturit.LobbyWars.Domain.Services;
using Signaturit.LobbyWars.Domain.Services.Base;
using System;
using System.Collections.Generic;
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

            Mock<IGetContractValueService> mockService = new();

            GetWinningContractQueryHandler queryHandler = new(mockService.Object);

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

        [Theory]
        [InlineData(5, 7, 7)]
        [InlineData(8, 6, 8)]
        public async void Handle_IfPass2Contract_ReturnExpectedValue(
            int contractValue1, int contractValue2, int expectedValue)
        {
            //Arrange
            Mock<Contract> contract1 = new();
            Mock<Contract> contract2 = new();

            Mock<IMediator>? mockMediator = new();

            Mock<IGetContractValueService> mockService = new();

            mockService.Setup(service => service.GetValue(contract1.Object)).Returns(contractValue1);

            mockService.Setup(service => service.GetValue(contract2.Object)).Returns(contractValue2);

            GetWinningContractQueryHandler queryHandler = new(mockService.Object);

            mockMediator
              .Setup(m => m.Send(It.IsAny<GetWinningContractQuery>(), It.IsAny<CancellationToken>()))

              .Returns<GetWinningContractQuery, CancellationToken>(
                      async (query, token) => await queryHandler.Handle(query, token));

            QueryRouter queryRouter = new(mockMediator.Object);

            GetWinningContractQuery query = new();

            query.AddParameter("contract1", contract1.Object)
                .AddParameter("contract2", contract2.Object);

            //Act
            Contract contract = await queryRouter.QueryOneAsync(query).ConfigureAwait(false);

            int contractValue = mockService.Object.GetValue(contract);

            //Assert
            //Assert.Same(expected: contract2.Object, contract);
            Assert.Equal(expected: expectedValue, contractValue);

        }
    }
}