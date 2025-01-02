using GluwaAPI.TestEngine.ApiController;
using GluwaAPI.TestEngine.AssertionHandlers;
using GluwaAPI.TestEngine.CurrencyUtils;
using GluwaAPI.TestEngine.EErrorTypeUtils;
using GluwaAPI.TestEngine.Setup;
using GluwaAPI.TestEngine.Utils;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Transfer.Tests
{
    [TestFixture("Test")]
    //[TestFixture("Staging")]
    //[TestFixture("Production")]
    [Category("QRCode"), Category("Post")]
    public class PostQRCodeTests : AmountUtils
    {
        /// <summary>
        /// Image type based on test name
        /// </summary>
        private static string mQRCodeImageType
        {
            get
            {
                List<string> testName = TestContext.CurrentContext.Test.Name.Split("_").ToList();
                string imageType = testName[testName.Count - 2];
                return imageType.ToLower();
            }
        }

        /// <summary>
        /// QR Code to print
        /// </summary>
        private static string mQRCode { get; set; }
        private string environment;
        internal ECurrency currency; 

        public PostQRCodeTests(string environment)
        {
            this.environment = environment;
        }

        [SetUp]
        public void Setup()
        {
            // Set up user types
            GluwaTestApi.SetUpGluwaTests(EUserType.QAAssertible, environment);
            currency = TestSettingsUtil.Currency;
        }

        [Test]
        [Category("Positive"), Category("NgNg")]
        [Description("TestCaseId:C4031")]
        public void Post_QRCode_NgNg_Base64_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("NgNg")]
        [Description("TestCaseId:C4032")]
        public void Post_QRCode_NgNg_Png_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("NgNg")]
        [Description("TestCaseId:C4033")]
        public void Post_QRCode_NgNg_Jpeg_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("sNgNg")]
        [Description("TestCaseId:C4034")]
        public void Post_QRCode_sNgNg_Base64_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("sNgNg")]
        [Description("TestCaseId:C4035")]
        public void Post_QRCode_sNgNg_Png_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("sNgNg")]
        [Description("TestCaseId:C4036")]
        public void Post_QRCode_sNgNg_Jpeg_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("sUsdcg")]
        [Description("TestCaseId:C4037")]
        public void Post_QRCode_sUsdcg_Base64_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(currency.ToDefaultCurrencyAmount()).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("sUsdcg")]
        [Description("TestCaseId:C4038")]
        public void Post_QRCode_sUsdcg_Png_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(currency.ToDefaultCurrencyAmount()).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("sUsdcg")]
        [Description("TestCaseId:C4039")]
        public void Post_QRCode_sUsdcg_Jpeg_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(currency.ToDefaultCurrencyAmount()).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("Usdcg")]
        [Description("TestCaseId:C4040")]
        public void Post_QRCode_Usdcg_Base64_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("Usdcg")]
        [Description("TestCaseId:C4041")]
        public void Post_QRCode_Usdcg_Png_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("Usdcg")]
        [Description("TestCaseId:C4042")]
        public void Post_QRCode_Usdcg_Jpeg_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert 
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        [Description("TestCaseId:C4043")]
        public void Post_QRCode_Gcre_Base64_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        [Description("TestCaseId:C4044")]
        public void Post_QRCode_Gcre_Png_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        [Description("TestCaseId:C4045")]
        public void Post_QRCode_Gcre_Jpeg_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("sUsdcg")]
        [Description("TestCaseId:C4046")]
        public void Post_QRCode_sUsdcg_Base64_WithNote_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(currency.ToDefaultCurrencyAmount()).ToString(), "Test QR Code");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("Usdcg")]
        [Description("TestCaseId:C4047")]
        public void Post_QRCode_Usdcg_Base64_WithNote_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString(), "Test QR Code");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("NgNg")]
        [Description("TestCaseId:C4048")]
        public void Post_QRCode_NgNg_Base64_WithNote_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString(), "Test QR Code");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("sNgNg")]
        [Description("TestCaseId:C4049")]
        public void Post_QRCode_sNgNg_Base64_WithNote_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString(), "Test QR Code");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("Gcre")]
        [Description("TestCaseId:C4050")]
        public void Post_QRCode_Gcre_Base64_WithNote_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString(), "Test QR Code");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            mQRCode = response.Content;
        }


        [Test]
        [Category("Positive"), Category("Payload")]
        [Description("TestCaseId:C4051")]
        public void Post_QRCode_WithPayload_Base64_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, 
                                                            currency.ToString(), 
                                                            CalculateQRCodeAmountFromFee(1m, currency).ToString(),
                                                            "Base64",
                                                            "250");

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode/payload"),
                                                     Api.SendRequest(Method.POST, body));

            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects.SelectToken("Data.PaymentID").ToString(), Is.Not.Null);
            Assert.AreEqual(currency.ToString().ToUpper(), objects.SelectToken("Data.Currency").ToString());
            Assert.AreEqual(targetAddress.Address, objects.SelectToken("Data.Target").ToString());
            Assert.That(objects.SelectToken("Data.Amount").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("Data.Fee").ToString(), Is.Not.Null);
            Assert.AreEqual("Base64", objects.SelectToken("Data.Note").ToString());
            Assert.AreEqual("250", objects.SelectToken("Data.MerchantOrderID").ToString());
            Assert.That(objects.SelectToken("Data.PaymentSig").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("Data.Environment").ToString(), Is.Not.Null);
        }


        [Test]
        [Category("Positive"), Category("Payload")]
        [Description("TestCaseId:C4052")]
        public void Post_QRCode_WithPayload_Jpeg_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress,
                                                            currency.ToString(),
                                                            CalculateQRCodeAmountFromFee(1m, currency).ToString(),
                                                            "Jpeg",
                                                            "250");
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode/payload"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects.SelectToken("Data.PaymentID").ToString(), Is.Not.Null);
            Assert.AreEqual(currency.ToString().ToUpper(), objects.SelectToken("Data.Currency").ToString());
            Assert.AreEqual(targetAddress.Address, objects.SelectToken("Data.Target").ToString());
            Assert.That(objects.SelectToken("Data.Amount").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("Data.Fee").ToString(), Is.Not.Null);
            Assert.AreEqual("Jpeg", objects.SelectToken("Data.Note").ToString());
            Assert.AreEqual("250", objects.SelectToken("Data.MerchantOrderID").ToString());
            Assert.That(objects.SelectToken("Data.PaymentSig").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("Data.Environment").ToString(), Is.Not.Null);
        }


        [Test]
        [Category("Positive"), Category("Payload")]
        [Description("TestCaseId:C4053")]
        public void Post_QRCode_WithPayload_Png_Pos()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress,
                                                            currency.ToString(),
                                                            CalculateQRCodeAmountFromFee(1m, currency).ToString(),
                                                            "Png",
                                                            "250");
            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode/payload"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Deserialization
            JObject objects = JObject.Parse(response.Content);

            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.OK, response, environment);
            Assert.That(objects.SelectToken("Data.PaymentID").ToString(), Is.Not.Null);
            Assert.AreEqual(currency.ToString().ToUpper(), objects.SelectToken("Data.Currency").ToString());
            Assert.AreEqual(targetAddress.Address, objects.SelectToken("Data.Target").ToString());
            Assert.That(objects.SelectToken("Data.Amount").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("Data.Fee").ToString(), Is.Not.Null);
            Assert.AreEqual("Png", objects.SelectToken("Data.Note").ToString());
            Assert.AreEqual("250", objects.SelectToken("Data.MerchantOrderID").ToString());
            Assert.That(objects.SelectToken("Data.PaymentSig").ToString(), Is.Not.Null);
            Assert.That(objects.SelectToken("Data.Environment").ToString(), Is.Not.Null);
        }


        [Test]
        [Category("Negative"), Category("Btc")]
        [Description("TestCaseId:C4054")]
        public void Post_QRCode_Btc_Neg()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetBtcAddress("Receiver", environment);
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidFieldError("Btc", "Currency"), response);
        }


        [Test]
        [Category("Negative"), Category("MissingBody")]
        [Description("TestCaseId:C4055")]
        public void Post_QRCode_MissingBody_Neg()
        {
            // Execute without body
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, null));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.missingBody, response);
        }


        [Test]
        [Category("Negative"), Category("AmountTooSmall")]
        [Description("TestCaseId:C4056")]
        public void Post_QRCode_AmountTooSmall_Neg()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(-1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetQRCodeAmountTooSmallError(AmountUtils.GetFee(TestSettingsUtil.Currency), body.Currency.ToUpper()), response);
        }


        [Test]
        [Category("Negative")]
        [Description("TestCaseId:C4057")]
        public void Post_QRCode_InvalidBody_Neg()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, "foobar", CalculateQRCodeAmountFromFee(1m, currency).ToString());

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.GetExpectedInvalidFieldError("foobar", "Currency"), response);
        }


        [Test]
        [Category("Negative")]
        [Description("TestCaseId:C4058")]
        public void Post_QRCode_InvalidFormat_Neg()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());


            // Execute with invalid image type
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", "image/txt"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.invalidQRCodeFormat, response);
        }


        [Test]
        [Category("Negative")]
        [Description("TestCaseId:C4059")]
        public void Post_QRCode_LongOrderID_Neg()
        {
            // Arrange address and create body
            var targetAddress = QAKeyVault.GetGluwaAddress("Receiver");
            var body = RequestTestBody.CreatePostQRCodeBody(targetAddress, currency.ToString(), CalculateQRCodeAmountFromFee(1m, currency).ToString());


            // Input a merchantOrderID that's too long
            body.MerchantOrderID = "1012345678901234567890123456789012345678901234567890123456789";

            // Execute
            IRestResponse response = Api.GetResponse(Api.SetGluwaApiUrlWithAuth($"v1/QRCode"),
                                                     Api.SendRequest(Method.POST, body)
                                                        .AddQueryParameter("format", $"image/{mQRCodeImageType}"));
            // Assert
            Assertions.HandleAssertionStatusCode(HttpStatusCode.BadRequest, response, environment);
            Assertions.HandleNegativeTestErrorMessages(ErrorOptions.LongMerchantOrderID, response);
        }


        [TearDown]
        public void WriteQRCodeTextInFile()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            if (!string.IsNullOrEmpty(mQRCodeImageType) && !testName.Contains("Neg"))
            {
                TestContext.WriteLine($"Printing QR Code with image type {mQRCodeImageType} in {testName}.txt.\nPlease verify the QR Code with the Gluwa Mobile App");
                GluwaTestApi.WriteQRCodeToFile($"{testName}.txt", mQRCode);
            }
            else
            {
                TestContext.WriteLine($"{testName} is a negative test or we can't write the QR Code to file");
            }
        }
    }
}
