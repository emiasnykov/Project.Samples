using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.Setup;
using NUnit.Framework;
using RestSharp;
using System.Net;
using GluwaAPI.TestEngine.Utils;

namespace FtaAccount.Tests
{
    [TestFixture("Test")]
    [TestFixture("Staging")]
    public class GetFtaInvestmentDetails : TransferFunctions
    {
        private string environment;
        private string keyName;

        public GetFtaInvestmentDetails(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            keyName = GluwaTestApi.SetUpGluwaKeyName(environment);
        }

        [Test]
        public void Get_FtaInvestmentDetails_Pos()
        {
            // Arrange
            string poolId = GetFtaPoolId(environment, poolState: "Scheduled");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolId}/InvestmentDetails"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
        }


        [Test]
        public void Get_FtaInvestmentDetails_Invalid_PoolID_Neg()
        {
            // Arrange
            string poolId = GetFtaPoolId(environment, poolState: "Open")[0..^3];

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolId}/InvestmentDetails"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleAssertionInnerMessage($"The value '{poolId}' is not valid.", response);
        }


        [Test]
        public void Get_FtaInvestmentDetails_NotFound_PoolID_Neg()
        {
            // Arrange
            string poolID = Shared.NOT_FOUND_ID;

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolID}/InvestmentDetails"),
                                           Api.SendRequest(Method.GET)
                                              .AddHeader("Authorization", "Bearer " + Api.GetBearerToken(environment)));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.NotFound, response, environment);
            Assertions.HandleAssertionMessage($"Could not find PoolID {poolID.ToLower()} in Gluwa", response);
        }


        [Test]
        public void Get_FtaInvestmentDetails_NoAuth_Neg()
        {
            // Arrange
            string poolId = GetFtaPoolId(environment, poolState: "Open");

            // Execute 
            var response = Api.GetResponse(Api.SetGluwaApiUrl($"V1/FtaPools/{poolId}/InvestmentDetails"),
                                           Api.SendRequest(Method.GET));
                                           //No Authorization header
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.Unauthorized, response, environment);
            Assertions.HandleAssertionMessage($"Client does not have permission to access this service.", response);
        }
    }
}
