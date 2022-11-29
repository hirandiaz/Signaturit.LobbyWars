using Moq;
using Signaturit.LobbyWars.Domain.Entities;
using Signaturit.LobbyWars.Domain.Factories;
using Signaturit.LobbyWars.Domain.Services;
using Signaturit.LobbyWars.Domain.Services.Base;
using System;
using Xunit;

namespace Signaturit.LobbyWars.Test
{
    public class GetMinimumSignatureShould
    {
        [Fact]
        public void GetSigntureRole_IfContractNotHaveMissingSignature_ThrowArgumentException()
        {
            //Arrange
            Mock<Contract> contractMock = new();
            Mock<IGetContractValueService> serviceMock = new();

            IGetMinimumSigntureService getMinimumSigntureService = new GetMinimumSignatureService(serviceMock.Object);

            //Act
            Action action = () => getMinimumSigntureService.GetSigntureRole(contractMock.Object, -1);

            //Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData(3, 4, SignatureRole.Notary)]
        [InlineData(4, 5, SignatureRole.Notary)]
        [InlineData(2, 6, SignatureRole.King)]

        public void GetSigntureRole_IfContractHaveMissingSignature_ReturnExpectedSignatureRole(
            int partialContractValue, int opponentContractValue, SignatureRole expectedSignatureRole)
        {
            //Arrange
            Mock<Contract> contractMock = new();
            Mock<IGetContractValueService> serviceMock = new();
            Signature signature = new SignatureFactory(SignatureRole.Missing).Create();

            contractMock.Object
                .AddSignature(signature);

            serviceMock
                .Setup(service => service.GetValue(contractMock.Object))
                .Returns(partialContractValue);

            IGetMinimumSigntureService getMinimumSigntureService = new GetMinimumSignatureService(serviceMock.Object);

            //Act
            SignatureRole signatureRole = getMinimumSigntureService.GetSigntureRole(contractMock.Object, opponentContractValue);

            //Assert
            Assert.Equal(expected: expectedSignatureRole, actual: signatureRole);
        }

    }
}