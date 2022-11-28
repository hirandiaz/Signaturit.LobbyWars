using Moq;
using Signaturit.LobbyWars.Domain.Entities;
using Signaturit.LobbyWars.Domain.Factories;
using Signaturit.LobbyWars.Domain.Services;
using Signaturit.LobbyWars.Domain.Services.Base;
using Xunit;

namespace Signaturit.LobbyWars.Test
{
    public class GetContractValueShould
    {
        [Fact]
        public void GetValue_IfContractIsInvalid_ReturnNegativeValue()
        {
            //Arrange
            Mock<Contract> contractMock = new();

            IGetContractValueService service = new GetContractValueService();

            //Act 
            int contractValue = service.GetValue(contractMock.Object);

            //Assert
            Assert.Equal(expected: -1, contractValue);
        }

        [Theory]
        [InlineData(SignatureRole.King, SignatureRole.Validator, 5)]
        [InlineData(SignatureRole.King, SignatureRole.Notary, 7)]
        [InlineData(SignatureRole.Notary, SignatureRole.Validator, 3)]
        [InlineData(SignatureRole.Missing, SignatureRole.Validator, 1)]
        public void GetValue_IfContractIsSignedMultiplesRoles_ReturnExpectedValue(
            SignatureRole role1, SignatureRole role2, int expectedValue)
        {
            //Arrange
            Signature signature1 = new SignatureFactory(role1).Create();
            Signature signature2 = new SignatureFactory(role2).Create();

            Contract contract = new ContractFactory(new[] { signature1, signature2 }).Create();

            IGetContractValueService service = new GetContractValueService();

            //Act 
            int contractValue = service.GetValue(contract);

            //Assert
            Assert.Equal(expected: expectedValue, contractValue);
        }


    }
}