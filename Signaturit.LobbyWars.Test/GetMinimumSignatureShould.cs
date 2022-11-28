using Moq;
using Signaturit.LobbyWars.Domain.Entities;
using Signaturit.LobbyWars.Domain.Services;
using Signaturit.LobbyWars.Domain.Services.Base;
using System;
using Xunit;

namespace Signaturit.LobbyWars.Test
{
    public class GetMinimumSignatureShould
    {
        [Fact]
        public void GetSigntureRole_IfContractNoHaveMissingSignature_ThrowArgumentException()
        {
            //Arrange
            Mock<Contract> contractMock = new();

            IGetMinimumSigntureService getMinimumSigntureService = new GetMinimumSignatureService();

            //Act
            Action action = () => getMinimumSigntureService.GetSigntureRole(contractMock.Object);

            //Assert
            Assert.Throws<ArgumentException>(action);
        }
    }
}