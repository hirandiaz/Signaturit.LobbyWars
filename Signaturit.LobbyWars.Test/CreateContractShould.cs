using DhubSolutions.Core.Domain.Data.Routers;
using MediatR;
using Moq;
using Signaturit.LobbyWars.Application.Handlers.Commands;
using Signaturit.LobbyWars.Core.Data.Models;
using Signaturit.LobbyWars.Domain.Data.Commands;
using Signaturit.LobbyWars.Domain.Data.Models;
using Signaturit.LobbyWars.Domain.Entities;
using System.Threading;
using Xunit;

namespace Signaturit.LobbyWars.Test
{
    public class CreateContractShould
    {
        [Fact]
        public async void Create_ReturnSuccesfullyOuput()
        {
            //Arrange
            Mock<IMediator>? mockMediator = new();

            CreateContractCommandHandler commandHandler = new();

            mockMediator
              .Setup(m => m.Send(It.IsAny<CreateContractCommand>(), It.IsAny<CancellationToken>()))

              .Returns<CreateContractCommand, CancellationToken>(
                      async (command, token) => await commandHandler.Handle(command, token));

            CommandRouter commandRouter = new(mockMediator.Object);

            SignatureCollectionDto signatures = new(SignatureRole.King, SignatureRole.Notary);

            CreateContractCommand command = new(signatures);

            //Act
            CommandResult result = await commandRouter.ExecuteAsync(command).ConfigureAwait(false);

            //Assert
            Assert.True(result.IsSuccess);


        }
    }
}