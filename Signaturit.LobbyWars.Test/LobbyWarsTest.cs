using Signaturit.LobbyWars.Domain.Entities;
using Signaturit.LobbyWars.Domain.Specifications;
using System;
using Xunit;

namespace Signaturit.LobbyWars.Test
{
    public class ContractShould
    {

        [Fact]
        public void IsValidContract_IfHaveMoreThanOneMissingSignature_ThrowException()
        {
            //Arrange
            Signature missingSignature1 = new(SignatureRole.Missing);
            Signature missingSignature2 = new(SignatureRole.Missing);

            Contract contract = new();

            //Act

            contract.AddSignature(missingSignature1);
            Action action = () => contract.AddSignature(missingSignature2);

            Assert.Throws<InvalidOperationException>(action);
        }

        [Theory]
        [InlineData(SignatureRole.Missing)]
        [InlineData(SignatureRole.Validator)]
        public void IsValidContract_IfHaveZeroOrOneMissingSignature_ReturnTrue(SignatureRole signatureRole)
        {
            IsValidContractSpecification specification = new();
            Signature signature = new(signatureRole);
            Contract contract = new();

            contract.AddSignature(signature);

            bool isValid = specification.IsSatisfiedBy(contract);

            Assert.True(isValid);


        }
    }
}