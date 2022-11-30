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
using System.Linq;
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

            Mock<IGetMinimumSigntureService> mockGetMinimumSignatureService = new();
            Mock<IGetContractValueService> mockGetContractValueService = new();

            GetMinimumSignatureQueryHandler queryHandler = new(
                mockGetContractValueService.Object,
                mockGetMinimumSignatureService.Object);

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

        [Theory]
        [InlineData(
            new[]
            {
                SignatureRole.Notary,
                SignatureRole.Missing,
                SignatureRole.Validator
            },
            new[]
            {
                SignatureRole.Notary,
                SignatureRole.Validator,
                SignatureRole.Validator
            },
            SignatureRole.Notary)]
        [InlineData(
            new[]
            {
                SignatureRole.Validator,
                SignatureRole.Missing,
                SignatureRole.Validator
            },
            new[]
            {
                SignatureRole.Notary,
                SignatureRole.Notary,
                SignatureRole.Notary
            },
            SignatureRole.Missing)]
        [InlineData(
            new[]
            {
                SignatureRole.Notary,
                SignatureRole.Notary,
                SignatureRole.Notary
            },
            new[]
            {
                 SignatureRole.Validator,
                SignatureRole.Missing,
                SignatureRole.Validator
            },
            SignatureRole.Missing)]

        public async void Handle_IfPassTwoValidContract_ReturnExpectedResult(
            IEnumerable<SignatureRole> roles1, IEnumerable<SignatureRole> roles2, SignatureRole expectedRole)
        {
            Mock<IMediator>? mockMediator = new();

            IGetContractValueService getContractValueService = new GetContractValueService();
            IGetMinimumSigntureService getMinimumSignatureService = new GetMinimumSignatureService(getContractValueService);

            IEnumerable<Signature> signatures1 = roles1.Select(role => new SignatureFactory(role).Create());
            IEnumerable<Signature> signatures2 = roles2.Select(role => new SignatureFactory(role).Create());

            Contract contract1 = new ContractFactory(signatures1).Create();
            Contract contract2 = new ContractFactory(signatures2).Create();

            GetMinimumSignatureQueryHandler queryHandler = new(
                getContractValueService,
                getMinimumSignatureService);

            mockMediator
              .Setup(m => m.Send(It.IsAny<GetMinimumSignatureQuery>(), It.IsAny<CancellationToken>()))

              .Returns<GetMinimumSignatureQuery, CancellationToken>(
                      async (query, token) => await queryHandler.Handle(query, token));

            QueryRouter queryRouter = new(mockMediator.Object);

            GetMinimumSignatureQuery query = new();

            query
                .AddParameter("contract1", contract1)
                .AddParameter("contract2", contract2);

            //Act
            Signature signature = await queryRouter.QueryOneAsync(query).ConfigureAwait(false);

            //Assert
            Assert.Equal(expectedRole, signature.Role);
        }
    }
}