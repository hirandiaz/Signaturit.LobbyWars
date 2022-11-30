using MediatR;
using Moq;
using Signaturit.LobbyWars.Application.Handlers.Commands;
using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Core.Data.Routers;
using Signaturit.LobbyWars.Domain.Data.Commands;
using Signaturit.LobbyWars.Domain.Data.Models;
using Signaturit.LobbyWars.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Signaturit.LobbyWars.Test
{
    public class CreateContractShould
    {
        [Fact]
        public async void Handle_ReturnSuccesfullyOuput()
        {
            //Arrange
            Mock<IMediator>? mockMediator = new();

            CreateContractCommandHandler commandHandler = new();

            mockMediator
              .Setup(m => m.Send(It.IsAny<CreateContractCommand>(), It.IsAny<CancellationToken>()))

              .Returns<CreateContractCommand, CancellationToken>(
                      async (command, token) => await commandHandler.Handle(command, token));

            CommandRouter commandRouter = new(mockMediator.Object);

            SignatureCollectionDto signatures = new(new[] { SignatureRole.King, SignatureRole.Notary });

            CreateContractCommand command = new(signatures);

            //Act
            CommandResult<Contract> result = await commandRouter.ExecuteAsync(command).ConfigureAwait(false);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Item);


        }

        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData(new SignatureRole[] { }, typeof(ArgumentException))]
        public async void Handle_IfNoPassSignatures_ThrowException(IEnumerable<SignatureRole> roles, Type expectedException)
        {
            //Arrange
            Mock<IMediator>? mockMediator = new();

            CreateContractCommandHandler commandHandler = new();

            mockMediator
              .Setup(m => m.Send(It.IsAny<CreateContractCommand>(), It.IsAny<CancellationToken>()))

              .Returns<CreateContractCommand, CancellationToken>(
                      async (command, token) => await commandHandler.Handle(command, token));

            CommandRouter commandRouter = new(mockMediator.Object);

            SignatureCollectionDto signatures = new(roles);

            CreateContractCommand command = new(signatures);

            //Act
            var action = async () => await commandRouter.ExecuteAsync(command).ConfigureAwait(false);

            //Assert
            await Assert.ThrowsAsync(expectedException, action);


        }
    }
}