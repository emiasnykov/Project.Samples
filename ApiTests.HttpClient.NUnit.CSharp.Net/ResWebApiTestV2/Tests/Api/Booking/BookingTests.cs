using System;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using ResWebApiTest.TestEngine;

namespace ResWebApiTest.Tests.Api.Booking
{
    [TestFixture]
    class BookingTests
    {
        [SetUp]
        public void Init()
        {
            // Main api path
            WebApiTest.ApiUriLogin = "Api/Login/ByLP";

            // Login credentials
            var Credentials = new
            {
                Login = "j********",
                Password = "j********"
            };

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Login, Credentials);

            // Assertions
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Login, "StatusCode"));
            Assert.AreEqual("janine", WebApiTest.Status(WebApiTest.ApiUri.Login, "login"));
            Assert.AreEqual("bearer", WebApiTest.Status(WebApiTest.ApiUri.Login, "token_type"));
            Assert.IsNotEmpty(WebApiTest.Status(WebApiTest.ApiUri.Login, "access_token"));
        }


        [Test]
        [Ignore("Due to CI booking event, the response is beeing changed each time, this test is ignored. Test passed on checking the request fuctionality.")]
        public void PostBookingEvent_ReturnCorrectWebBooking()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/BookEvent";

            // Test data
            string payload = @"
            {
	            ""EventId"": 1642,
	            ""CurrencyId"": 300,
	            ""AgentId"": 41,
	            ""ApplicationType"": ""BookingAgent"",
	            ""EventBeginDate"": ""2020-02-03T00:00:00"",
	            ""EventEndDate"": ""2020-02-06T00:00:00"",
	            ""Travelers"": [{
		            ""Personal"": {
			            ""FirstName"": ""John"",
			            ""MiddleName"": ""WWW"",
			            ""Surname"": ""Pickled"",
			            ""Title"": ""Mr"",
			            ""Phone1"": ""+48999888009"",
			            ""Email"": ""pickled@fake.com"",
			            ""CountryId"": 216,
			            ""ProvinceId"": 0,
			            ""City"": ""Crownville"",
			            ""Address"": ""1169 Gumbottom road"",
			            ""PostalOrZipCode"": ""21032"",
			            ""Password"": """",
			            ""ConfirmPassword"": """",
			            ""SalutationId"": 2942
		            },
		            ""PersonalDetails"": {
			            ""FolioType"": ""P"",
			            ""FolioRepeaterNo"": ""111222333"",
			            ""Gender"": ""F"",
			            ""AgeGroup"": ""Adult"",
			            ""BirthDate"": ""1975-03-01T00:00:00.000Z"",
			            ""Privacy1"": 1,
			            ""Privacy2"": 1,
			            ""Privacy3"": 1,
			            ""Privacy4"": 1,
			            ""Privacy5"": 1,
			            ""Privacy6"": 1,
			            ""Privacy7"": 1,
			            ""Privacy8"": 1
		            },
		            ""TravelerTripInfo"": {
			            ""EventId"": 1642,
			            ""EventBeginDate"": ""2020-02-03T00:00:00"",
			            ""EventEndDate"": ""2020-02-06T00:00:00"",
			            ""UnitId"": 19,
			            ""CategoryId"": 20,
			            ""BedsLayoutId"": 25802,
			            ""PriceRateId"": 12424
		            }
	            }, {
		            ""Personal"": {
			            ""FirstName"": ""Jarett"",
			            ""MiddleName"": ""jar"",
			            ""Surname"": ""Cohen"",
			            ""Title"": ""Mrs"",
			            ""Phone1"": ""+61299482929"",
			            ""Email"": ""jarcoh2@fake.com"",
			            ""CountryId"": 274,
			            ""ProvinceId"": 1269,
			            ""City"": ""Colyton"",
			            ""Address"": ""9 Swales Place"",
			            ""PostalOrZipCode"": ""2759"",
			            ""Password"": """",
			            ""ConfirmPassword"": """",
			            ""SalutationId"": 2940
		            },
		            ""PersonalDetails"": {
			            ""FolioType"": ""P"",
			            ""FolioRepeaterNo"": ""222333444"",
			            ""Gender"": ""F"",
			            ""AgeGroup"": ""Adult"",
			            ""BirthDate"": ""1994-03-17T00:00:00.000Z"",
			            ""Privacy1"": 1,
			            ""Privacy2"": 1,
			            ""Privacy3"": 1,
			            ""Privacy4"": 1,
			            ""Privacy5"": 1,
			            ""Privacy6"": 1,
			            ""Privacy7"": 1,
			            ""Privacy8"": 1
		            },
		            ""TravelerTripInfo"": {
			            ""EventId"": 1642,
			            ""EventBeginDate"": ""2020-02-03T00:00:00"",
			            ""EventEndDate"": ""2020-02-06T00:00:00"",
			            ""UnitId"": 19,
			            ""CategoryId"": 20,
			            ""BedsLayoutId"": 25802,
			            ""PriceRateId"": 12424
		            }
	            }]
            }";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, payload);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for PostBookingEvent_ReturnCorrectWebBooking

            // Asserts
            Assert.AreEqual(23628, WebApiTest.Node<int>("result", "BookingId"));
            Assert.AreEqual(918, WebApiTest.Node<int>("result", "TravellerInfo", 0, "FolioId"));
            Assert.AreEqual(36531, WebApiTest.Node<int>("result", "TravellerInfo", 0, "TravelerId"));
            Assert.AreEqual(36608, WebApiTest.Node<int>("result", "TravellerInfo", 0, "TripId"));
            Assert.AreEqual("John", WebApiTest.Node("result", "TravellerInfo", 0, "FirstName"));
            Assert.AreEqual("Pickled", WebApiTest.Node("result", "TravellerInfo", 0, "Surname"));
            Assert.AreEqual("Adult", WebApiTest.Node("result", "TravellerInfo", 0, "AgeGroup"));
            Assert.AreEqual(36961, WebApiTest.Node<int>("result", "TravellerInfo", 1, "FolioId"));
            Assert.AreEqual(36532, WebApiTest.Node<int>("result", "TravellerInfo", 1, "TravelerId"));
            Assert.AreEqual(36609, WebApiTest.Node<int>("result", "TravellerInfo", 1, "TripId"));
            Assert.AreEqual("Jarett", WebApiTest.Node("result", "TravellerInfo", 1, "FirstName"));
            Assert.AreEqual("Cohen", WebApiTest.Node("result", "TravellerInfo", 1, "Surname"));
            Assert.AreEqual("Adult", WebApiTest.Node("result", "TravellerInfo", 1, "AgeGroup"));
            Assert.AreEqual(new DateTime(1, 1, 1), WebApiTest.Node<DateTime>("result", "BlockUnitExpiryTime"));

            #endregion Asserts for PostBookingEvent_ReturnCorrectWebBooking

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void GetValidateAge_InRangeReturnTrue()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/ValidateAge?birthDate=2019/06/4";

            // Send GET request with authorization  
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Asserts
            Assert.AreEqual(true, WebApiTest.Node<bool>());

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Test passed
            Assert.Pass();
        }


        [Test]
        public void GetValidateAge_OutOfRangeReturnFalse()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/ValidateAge?birthDate=1880/06/4";

            // Send GET request with authorization  
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Asserts
            Assert.AreEqual(false, WebApiTest.Node<bool>());

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Test passed
            Assert.Pass();
        }


        [Test]
        [Ignore("Due to lack of availability of units, this test is ignored. Test passed on checking the request fuctionality.")]
        public void PostConfirmBooking_ReturnCorrectBooking()
        {   
            // Add booking
            // Main api path
            WebApiTest.ApiUriRequest = "api/Booking/BookEvent";
            string s = "{\"EventId\":1642,\"CurrencyId\":300,\"AgentId\":41,\"ApplicationType\":\"BookingAgent\",\"EventBeginDate\":\"2020-02-03T00:00:00\",\"EventEndDate\":\"2020-02-06T00:00:00\",\"Travelers\":[{\"Personal\":{\"FirstName\":\"John\",\"MiddleName\":\"WWW\",\"Surname\":\"Pickled\",\"Title\":\"Mr\",\"Phone1\":\"+48999888009\",\"Email\":\"pickled@fake.com\",\"CountryId\":216,\"ProvinceId\":0,\"City\":\"Crownville\",\"Address\":\"1169 Gumbottom road\",\"PostalOrZipCode\":\"21032\",\"Password\":\"\",\"ConfirmPassword\":\"\",\"SalutationId\":2942},\"PersonalDetails\":{\"FolioType\":\"P\",\"FolioRepeaterNo\":\"111222333\",\"Gender\":\"F\",\"AgeGroup\":\"Adult\",\"BirthDate\":\"1975-03-01T00:00:00.000Z\",\"Privacy1\":1,\"Privacy2\":1,\"Privacy3\":1,\"Privacy4\":1,\"Privacy5\":1,\"Privacy6\":1,\"Privacy7\":1,\"Privacy8\":1},\"TravelerTripInfo\":{\"EventId\":1642,\"EventBeginDate\":\"2020-02-03T00:00:00\",\"EventEndDate\":\"2020-02-06T00:00:00\",\"UnitId\":19,\"CategoryId\":20,\"BedsLayoutId\":25802,\"PriceRateId\":12424}},{\"Personal\":{\"FirstName\":\"Jarett\",\"MiddleName\":\"jar\",\"Surname\":\"Cohen\",\"Title\":\"Mrs\",\"Phone1\":\"+61299482929\",\"Email\":\"jarcoh2@fake.com\",\"CountryId\":274,\"ProvinceId\":1269,\"City\":\"Colyton\",\"Address\":\"9 Swales Place\",\"PostalOrZipCode\":\"2759\",\"Password\":\"\",\"ConfirmPassword\":\"\",\"SalutationId\":2940},\"PersonalDetails\":{\"FolioType\":\"P\",\"FolioRepeaterNo\":\"222333444\",\"Gender\":\"F\",\"AgeGroup\":\"Adult\",\"BirthDate\":\"1994-03-17T00:00:00.000Z\",\"Privacy1\":1,\"Privacy2\":1,\"Privacy3\":1,\"Privacy4\":1,\"Privacy5\":1,\"Privacy6\":1,\"Privacy7\":1,\"Privacy8\":1},\"TravelerTripInfo\":{\"EventId\":1642,\"EventBeginDate\":\"2020-02-03T00:00:00\",\"EventEndDate\":\"2020-02-06T00:00:00\",\"UnitId\":19,\"CategoryId\":20,\"BedsLayoutId\":25802,\"PriceRateId\":12424}}]}";
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, s);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check any response
            // Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));
            var bookingId = WebApiTest.Node<int>("result", "BookingId");

            // Confirm booking 
            // Main api path
            WebApiTest.ApiUriRequest = "api/Booking/ConfirmBooking";
            string s2 = $"{{\"EventId\":1642,\"BookingId\":{bookingId},\"IsAgentBooking\":true,\"WebSocketsConnectionId\":\"3a3b228a-9e04-4159-94f4-bdd940274347\"}}";
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, s2);

            // Asserts
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));      
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));
            Assert.Pass();
        }


        [Test]
        public void GetBookingForFolio_ReturnBooking()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/GetBookingsForFolio?folioId=1124";

            // Send GET request with authorization  
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetBookingForFolio_ReturnBooking

            // Asserts
            Assert.AreEqual(466, WebApiTest.Node<int>(0, 0, "BookingId"));
            Assert.AreEqual(4462, WebApiTest.Node<int>(0, 0, "BookingCode"));
            Assert.GreaterOrEqual(WebApiTest.Node<DateTime>(0, 0, "CreatedDate"), new DateTime(2009, 4, 27, 8, 18, 59, 0));
            Assert.AreEqual("R", WebApiTest.Node(0, 0, "BookingStatus"));
            Assert.AreEqual(2, WebApiTest.Node<int>(0, 0, "PeopleCount"));
            Assert.AreEqual(4, WebApiTest.Node<int>(0, 0, "CruiseDays"));
            Assert.AreEqual(1, WebApiTest.Node<int>(0, 0, "AgencyCode"));
            Assert.AreEqual("MBE", WebApiTest.Node(0, 0, "FacilityCode"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node(0, 0, "FacilityName"));
            Assert.AreEqual(41, WebApiTest.Node<int>(0, 0, "EventId"));
            Assert.AreEqual(905141, WebApiTest.Node<int>(0, 0, "EventCode"));
            Assert.AreEqual("Fly/Dive Coral Sea", WebApiTest.Node(0, 0, "EventName"));
            Assert.AreEqual(new DateTime(2009, 5, 14), WebApiTest.Node<DateTime>(0, 0, "EventBegDate"));
            Assert.AreEqual(new DateTime(2009, 5, 18), WebApiTest.Node<DateTime>(0, 0, "EventEndDate"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "EventRegionList"));
            Assert.IsEmpty(WebApiTest.Node<string>(0, 0, "EventImage"));
            Assert.AreEqual(3332.8, WebApiTest.Node<double>(0, 0, "TotalDebit"));
            Assert.AreEqual(3332.8, WebApiTest.Node<double>(0, 0, "TotalCredit"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "CommissionDebit"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "CommissionCredit"));
            Assert.AreEqual(3332.8, WebApiTest.Node<double>(0, 0, "Total"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "Commission"));
            Assert.AreEqual(3332.8, WebApiTest.Node<double>(0, 0, "NetAmount"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "NetBalance"));
            Assert.AreEqual(3332.8, WebApiTest.Node<double>(0, 0, "Payments"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "TotalBalance"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "EditManifest"));
            Assert.AreEqual("O", WebApiTest.Node(0, 0, "BookingOrigin"));
            Assert.AreEqual("Reserved", WebApiTest.Node(0, 0, "BookingStatusText"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "BookingType"));
            Assert.AreEqual("LimeGreen", WebApiTest.Node(0, 0, "BookingStatusColor"));

            #endregion Asserts for GetBookingForFolio_ReturnBooking

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Test passed
            Assert.Pass();
        }
        

        [Test]
        public void GetBookingSummary_ReturnCorrectBookingSummary()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/GetBookingSummary?bookingId=21113 ";

            // Send GET request with authorization  
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetBookingSummary_ReturnCorrectBookingSummary

            // Asserts
            Assert.AreEqual("MBE", WebApiTest.Node("FacilityCode"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node("FacilityName"));
            Assert.AreEqual(1642, WebApiTest.Node<int>("EventId"));
            Assert.AreEqual(2002032, WebApiTest.Node<int>("EventCode"));
            Assert.AreEqual("3 night Fly Dive Cod Hole", WebApiTest.Node("EventName"));
            Assert.AreEqual("", WebApiTest.Node("BookingCode"));
            Assert.AreEqual(new DateTime(2020, 2, 3), WebApiTest.Node<DateTime>("EventBeginDate"));
            Assert.AreEqual(new DateTime(2020, 2, 6), WebApiTest.Node<DateTime>("EventEndDate"));
            Assert.AreEqual(3, WebApiTest.Node<int>("EventDuration"));
            Assert.AreEqual("FLT/C-L", WebApiTest.Node("DeparturePortCode"));
            Assert.AreEqual("Chartered flight from Cairns to Lizard Island.", WebApiTest.Node("DeparturePortName"));
            Assert.AreEqual("TRIN", WebApiTest.Node("ArrivalPortCode"));
            Assert.AreEqual("8:00am disembark at Trinity Wharf", WebApiTest.Node("ArrivalPortName"));
            Assert.AreEqual("AUD", WebApiTest.Node("Currency"));
            Assert.AreEqual(5204, WebApiTest.Node<int>("TotalPrice"));
            Assert.AreEqual(0, WebApiTest.Node<int>("TotalCommission"));
            Assert.AreEqual(5204, WebApiTest.Node<int>("NetPrice"));
            Assert.AreEqual(5204, WebApiTest.Node<int>("NetBalance"));
            Assert.AreEqual(5204, WebApiTest.Node<int>("TotalBalance"));
            Assert.AreEqual(0, WebApiTest.Node<int>("CreditPayments"));
            Assert.AreEqual("R", WebApiTest.Node("BookingStatus"));
            Assert.AreEqual("D", WebApiTest.Node("PaymentStatus"));
            Assert.AreEqual(new DateTime(2200, 1, 1), WebApiTest.Node<DateTime>("BookingExpiryDate"));
            Assert.AreEqual(1, WebApiTest.Node<int>("UnitCount"));
            Assert.AreEqual(5204, WebApiTest.Node<int>("CruiseTotalPrice"));
            Assert.AreEqual(0, WebApiTest.Node<int>("CruiseTotalCommission"));
            Assert.AreEqual(0, WebApiTest.Node<int>("HotelItemsTotalPrice"));
            Assert.AreEqual(0, WebApiTest.Node<int>("HotelItemsTotalCommission"));
            Assert.AreEqual(0, WebApiTest.Node<int>("FlightItemsTotalPrice"));
            Assert.AreEqual(0, WebApiTest.Node<int>("FlightItemsTotalCommission"));
            Assert.AreEqual(0, WebApiTest.Node<int>("TourItemsTotalPrice"));
            Assert.AreEqual(0, WebApiTest.Node<int>("TourItemsTotalCommission"));
            Assert.AreEqual(0, WebApiTest.Node<int>("OptionItemsTotalPrice"));
            Assert.AreEqual(0, WebApiTest.Node<int>("OptionItemsTotalCommission"));
            Assert.AreEqual(0, WebApiTest.Node<int>("TransferItemsTotalPrice"));
            Assert.AreEqual(0, WebApiTest.Node<int>("TransferItemsTotalCommission"));
            Assert.AreEqual(true, WebApiTest.Node<bool>("GuestInformations", 0, "IsPrimaryTraveler"));
            Assert.AreEqual("John", WebApiTest.Node("GuestInformations", 0, "Forename"));
            Assert.AreEqual("Pickled", WebApiTest.Node("GuestInformations", 0, "Surname"));
            Assert.AreEqual(2602, WebApiTest.Node<int>("GuestInformations", 0, "Price"));
            Assert.AreEqual(918, WebApiTest.Node<int>("GuestInformations", 0, "FolioId"));
            Assert.AreEqual(0, WebApiTest.Node<int>("GuestInformations", 0, "Commision"));
            Assert.AreEqual("Adult", WebApiTest.Node("GuestInformations", 0, "AgeGroup"));
            Assert.AreEqual(33640, WebApiTest.Node<int>("GuestInformations", 0, "TravelerId"));
            Assert.AreEqual(33714, WebApiTest.Node<int>("GuestInformations", 0, "PrimaryTripId"));
            Assert.AreEqual(false, WebApiTest.Node<bool>("GuestInformations", 1, "IsPrimaryTraveler"));
            Assert.AreEqual("Jozef", WebApiTest.Node("GuestInformations", 1, "Forename"));
            Assert.AreEqual("Cohen", WebApiTest.Node("GuestInformations", 1, "Surname"));
            Assert.AreEqual(2602, WebApiTest.Node<int>("GuestInformations", 1, "Price"));
            Assert.AreEqual(4911, WebApiTest.Node<int>("GuestInformations", 1, "FolioId"));
            Assert.AreEqual(0, WebApiTest.Node<int>("GuestInformations", 1, "Commision"));
            Assert.AreEqual("Adult", WebApiTest.Node("GuestInformations", 1, "AgeGroup"));
            Assert.AreEqual(33641, WebApiTest.Node<int>("GuestInformations", 1, "TravelerId"));
            Assert.AreEqual(33715, WebApiTest.Node<int>("GuestInformations", 1, "PrimaryTripId"));
            Assert.AreEqual("S", WebApiTest.Node("BookingItems", 0, "ItemCode"));
            Assert.AreEqual("Standard - twin or sole occ, per person", WebApiTest.Node("BookingItems", 0, "ItemName"));
            Assert.AreEqual("DR", WebApiTest.Node("BookingItems", 0, "ItemGroup"));
            Assert.AreEqual(2602, WebApiTest.Node<int>("BookingItems", 0, "Value"));
            Assert.AreEqual("Pickled", WebApiTest.Node("BookingItems", 0, "Surname"));
            Assert.AreEqual("John", WebApiTest.Node("BookingItems", 0, "Forename"));
            Assert.AreEqual(918, WebApiTest.Node<int>("BookingItems", 0, "FolioId"));
            Assert.AreEqual(0, WebApiTest.Node<int>("BookingItems", 0, "Commission"));
            Assert.AreEqual("S", WebApiTest.Node("BookingItems", 1, "ItemCode"));
            Assert.AreEqual("Standard - twin or sole occ, per person", WebApiTest.Node("BookingItems", 1, "ItemName"));
            Assert.AreEqual("DR", WebApiTest.Node("BookingItems", 1, "ItemGroup"));
            Assert.AreEqual(2602, WebApiTest.Node<int>("BookingItems", 1, "Value"));
            Assert.AreEqual("Cohen", WebApiTest.Node("BookingItems", 1, "Surname"));
            Assert.AreEqual("Jozef", WebApiTest.Node("BookingItems", 1, "Forename"));
            Assert.AreEqual(4911, WebApiTest.Node<int>("BookingItems", 1, "FolioId"));
            Assert.AreEqual(0, WebApiTest.Node<int>("BookingItems", 1, "Commission"));
            Assert.AreEqual(false, WebApiTest.Node<bool>("BookingEvents", 0, "Disabled"));
            Assert.AreEqual(true, WebApiTest.Node<bool>("BookingEvents", 0, "Primary"));
            Assert.AreEqual(6, WebApiTest.Node<int>("BookingEvents", 0, "UnitNo"));
            Assert.AreEqual("S", WebApiTest.Node("BookingEvents", 0, "CategoryCode"));
            Assert.AreEqual("Standard - twin or sole occ, per person", WebApiTest.Node("BookingEvents", 0, "CategoryName"));
            Assert.AreEqual(1642, WebApiTest.Node<int>("BookingEvents", 0, "EventId"));
            Assert.AreEqual(2002032, WebApiTest.Node<int>("BookingEvents", 0, "EventCode"));
            Assert.AreEqual("3 night Fly Dive Cod Hole", WebApiTest.Node("BookingEvents", 0, "EventName"));
            Assert.IsNotEmpty(WebApiTest.Node<string>("BookingEvents", 0, "EventImage"));
            Assert.AreEqual(3438, WebApiTest.Node<int>("BookingEvents", 0, "EventPageInfoId"));
            Assert.AreEqual("null", WebApiTest.Node("BookingEvents", 0, "EventPageInfoType"));
            Assert.AreEqual("null", WebApiTest.Node("BookingEvents", 0, "EventPageInfoText"));
            Assert.AreEqual(3, WebApiTest.Node<int>("BookingEvents", 0, "Days"));
            Assert.AreEqual(new DateTime(2020, 2, 6), WebApiTest.Node<DateTime>("BookingEvents", 0, "LastDate"));
            Assert.AreEqual(new DateTime(2020, 2, 3), WebApiTest.Node<DateTime>("BookingEvents", 0, "StartDate"));

            #endregion Asserts for GetBookingSummary_ReturnCorrectBookingSummary

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Test passed
            Assert.Pass();
        }


        [Test]
        [Ignore("To be fixed later")]
        public void DeleteVoidBookingItems_ReturnStatusOk()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "api/Event/SelectEventItems";
            string s = "{\"BookingId\":21141,\"EventId\":1642,\"EventBeginDate\":\"2020-02-03T00:00:00\",\"EventEndDate\":\"2020-02-06T00:00:00\",\"ItemsList\":[{\"ItemId\":174,\"TravelerId\":33691,\"ItemFrom\":\"2020-01-31T00:00:00.000Z\",\"ItemTo\":\"2020-02-03T00:00:00\",\"DeparturePlaceCode\":\"GDN\",\"ArrivalPlaceCode\":\"CNS\",\"ItemPrice\":1200,\"ItemName\":\"Charter flight to CNS\",\"IsSelected\":true,\"Quantity\":1,\"TripId\":33765,\"FolioId\":918},{\"ItemId\":175,\"TravelerId\":33691,\"ItemFrom\":\"2020-02-06T00:00:00\",\"ItemTo\":\"2020-02-09T00:00:00.000Z\",\"DeparturePlaceCode\":\"CNS\",\"ArrivalPlaceCode\":\"GDN\",\"ItemPrice\":1200,\"ItemName\":\"Charter flight from CNS\",\"IsSelected\":true,\"Quantity\":1,\"TripId\":33765,\"FolioId\":918},{\"ItemId\":125,\"TravelerId\":33691,\"ItemFrom\":\"2020-02-03T00:00:00\",\"ItemTo\":\"2020-02-06T00:00:00\",\"DeparturePlaceCode\":\"\",\"ArrivalPlaceCode\":\"\",\"ItemPrice\":0,\"ItemName\":\"Airport Transfer\",\"IsSelected\":true,\"Quantity\":1,\"TripId\":33765,\"FolioId\":918},{\"ItemId\":6,\"TravelerId\":33691,\"ItemFrom\":\"2020-02-03T00:00:00.000Z\",\"ItemTo\":\"2020-02-03T00:00:00\",\"DeparturePlaceCode\":\"\",\"ArrivalPlaceCode\":\"\",\"ItemPrice\":190,\"ItemName\":\"The Hotel Cairns- Plantation\",\"IsSelected\":true,\"Quantity\":1,\"TripId\":33765,\"FolioId\":918},{\"ItemId\":8,\"TravelerId\":33691,\"ItemFrom\":\"2020-02-06T00:00:00\",\"ItemTo\":\"2020-02-06T00:00:00.000Z\",\"DeparturePlaceCode\":\"\",\"ArrivalPlaceCode\":\"\",\"ItemPrice\":260,\"ItemName\":\"Pullman Reef Casino - Standard King\",\"IsSelected\":true,\"Quantity\":1,\"TripId\":33765,\"FolioId\":918},{\"ItemId\":98,\"TravelerId\":33691,\"ItemFrom\":\"2020-02-03T00:00:00\",\"ItemTo\":\"2020-02-06T00:00:00\",\"DeparturePlaceCode\":\"\",\"ArrivalPlaceCode\":\"\",\"ItemPrice\":0,\"ItemName\":\"Miscellaneous Other Sale Item\",\"IsSelected\":true,\"Quantity\":1,\"TripId\":33765,\"FolioId\":918},{\"ItemId\":146,\"TravelerId\":33691,\"ItemFrom\":\"2020-02-03T00:00:00\",\"ItemTo\":\"2020-02-06T00:00:00\",\"DeparturePlaceCode\":\"\",\"ArrivalPlaceCode\":\"\",\"ItemPrice\":75,\"ItemName\":\"Nitrox Fills - 3 or 4 Night Exp\",\"TripId\":33765,\"FolioId\":918,\"IsSelected\":true,\"Quantity\":1},{\"ItemId\":149,\"TravelerId\":33691,\"ItemFrom\":\"2020-02-03T00:00:00\",\"ItemTo\":\"2020-02-06T00:00:00\",\"DeparturePlaceCode\":\"\",\"ArrivalPlaceCode\":\"\",\"ItemPrice\":132,\"ItemName\":\"Full Set- All Gear - 3 & 4 Night Exp\",\"TripId\":33765,\"FolioId\":918,\"IsSelected\":true,\"Quantity\":1}],\"CurrencyId\":300}";
            
            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, s);

            // Asserts
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Main api path
            var bookingId = 21141;
            WebApiTest.ApiUriRequest = $"Api/Booking/VoidBookingItems?bookingId={bookingId}";

            // Send GET request with authorization  
            WebApiTest.SendRequest(HttpMethod.Delete, WebApiTest.ApiUri.Request);

            // Asserts
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());
            Assert.Pass();
        }


        [Test]
        [Ignore("Ignored due to +-3 days filter on event search")]
        public void SearchTraveler_ReturnStatusOk()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/SearchTravelers?searchParam=&eventId=1600";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Assertions - Jim Booker
            Assert.AreEqual(1600, WebApiTest.Node<int>(0, 0, "EventId"));
            Assert.AreEqual(16, WebApiTest.Node<int>(0, 0, "ReservedImportId"));
            Assert.AreEqual(33789, WebApiTest.Node<int>(0, 0, "TripId"));
            Assert.AreEqual("Jim", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, 0, "Surname"));
            Assert.AreEqual(new DateTime(1984, 09, 10), WebApiTest.Node<DateTime>(0, 0, "Birthdate"));
            Assert.AreEqual("190008", WebApiTest.Node(0, 0, "BookingCode"));
            Assert.IsEmpty(WebApiTest.Node(0, 0, "BookingNumber"));
            Assert.AreEqual("3453474576245", WebApiTest.Node(0, 0, "DocumentNo"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasPhoto"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasCreditCard"));
            Assert.AreEqual("Unknown", WebApiTest.Node(0, 0, "CheckInStatus"));

            // Assertions 
            Assert.AreEqual(1600, WebApiTest.Node<int>(0, 1, "EventId"));
            Assert.AreEqual(17, WebApiTest.Node<int>(0, 1, "ReservedImportId"));
            Assert.AreEqual(33790, WebApiTest.Node<int>(0, 1, "TripId"));
            Assert.AreEqual("Matthew", WebApiTest.Node(0, 1, "Forename"));
            Assert.AreEqual("Brooker", WebApiTest.Node(0, 1, "Surname"));
            Assert.AreEqual(new DateTime(1979, 03, 16), WebApiTest.Node<DateTime>(0, 1, "Birthdate"));
            Assert.AreEqual("190008", WebApiTest.Node(0, 1, "BookingCode"));
            Assert.IsEmpty(WebApiTest.Node(0, 1, "BookingNumber"));
            Assert.IsEmpty(WebApiTest.Node(0, 1, "DocumentNo"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 1, "HasPhoto"));
            Assert.AreEqual(false, WebApiTest.Node<bool>(0, 1, "HasCreditCard"));
            Assert.AreEqual("Unknown", WebApiTest.Node(0, 1, "CheckInStatus"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Pass
            Assert.Pass();
        }


        [Test]
        [Ignore("Ignored due to +-3 days filter on event search")]
        public void ChangeCheckInStatus_ReturnStatusBadRequest_NoDocumentsFound()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/ChangeCheckInStatus?retUId=33790&checkInStatus=1";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, null, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Pass
            Assert.Pass();
        }


        [Test]
        public void ChangeCheckInStatus_ReturnStatusOk()
        {
            // Confirm UNKNOW status 
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/SearchTravelers?searchParam=&eventId=1600";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            var tripId = WebApiTest.Node<int>(0, 0, "TripId");

            // Assertions - Jim Booker
            Assert.AreEqual(1600, WebApiTest.Node<int>(0, 0, "EventId"));
            Assert.AreEqual(16, WebApiTest.Node<int>(0, 0, "ReservedImportId"));
            Assert.Greater(tripId, 0);
            Assert.AreEqual("Jim", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, 0, "Surname"));
            Assert.AreEqual(new DateTime(1984, 09, 10), WebApiTest.Node<DateTime>(0, 0, "Birthdate"));
            Assert.AreEqual("190008", WebApiTest.Node(0, 0, "BookingCode"));
            Assert.IsEmpty(WebApiTest.Node(0, 0, "BookingNumber"));
            Assert.AreEqual("3453474576245", WebApiTest.Node(0, 0, "DocumentNo"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasPhoto"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasCreditCard"));
            Assert.AreEqual("Unknown", WebApiTest.Node(0, 0, "CheckInStatus"));

            // Check count - just check Jim Brooker
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Change to NOSHOW status
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Booking/ChangeCheckInStatus?retUId={tripId}&checkInStatus=2";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Confirm NOSHOW status 
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/SearchTravelers?searchParam=&eventId=1600";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Assertions - Jim Booker
            Assert.AreEqual(1600, WebApiTest.Node<int>(0, 0, "EventId"));
            Assert.AreEqual(16, WebApiTest.Node<int>(0, 0, "ReservedImportId"));
            Assert.AreEqual(tripId, WebApiTest.Node<int>(0, 0, "TripId"));
            Assert.AreEqual("Jim", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, 0, "Surname"));
            Assert.AreEqual(new DateTime(1984, 09, 10), WebApiTest.Node<DateTime>(0, 0, "Birthdate"));
            Assert.AreEqual("190008", WebApiTest.Node(0, 0, "BookingCode"));
            Assert.IsEmpty(WebApiTest.Node(0, 0, "BookingNumber"));
            Assert.AreEqual("3453474576245", WebApiTest.Node(0, 0, "DocumentNo"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasPhoto"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasCreditCard"));
            Assert.AreEqual("NoShow", WebApiTest.Node(0, 0, "CheckInStatus"));

            // Check count - just check Jim Brooker
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Change to CHECK-IN status
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Booking/ChangeCheckInStatus?retUId={tripId}&checkInStatus=0";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Confirm CHECK-IN status 
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/SearchTravelers?searchParam=&eventId=1600";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Assertions - Jim Booker
            Assert.AreEqual(1600, WebApiTest.Node<int>(0, 0, "EventId"));
            Assert.AreEqual(16, WebApiTest.Node<int>(0, 0, "ReservedImportId"));
            Assert.AreEqual(tripId, WebApiTest.Node<int>(0, 0, "TripId"));
            Assert.AreEqual("Jim", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, 0, "Surname"));
            Assert.AreEqual(new DateTime(1984, 09, 10), WebApiTest.Node<DateTime>(0, 0, "Birthdate"));
            Assert.AreEqual("190008", WebApiTest.Node(0, 0, "BookingCode"));
            Assert.IsEmpty(WebApiTest.Node(0, 0, "BookingNumber"));
            Assert.AreEqual("3453474576245", WebApiTest.Node(0, 0, "DocumentNo"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasPhoto"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasCreditCard"));
            Assert.AreEqual("CheckIn", WebApiTest.Node(0, 0, "CheckInStatus"));

            // Check count - just check Jim Brooker
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Change to CHECK-OUT status
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Booking/ChangeCheckInStatus?retUId={tripId}&checkInStatus=1";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Confirm CHECK-OUT status 
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/SearchTravelers?searchParam=&eventId=1600";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Assertions - Jim Booker
            Assert.AreEqual(1600, WebApiTest.Node<int>(0, 0, "EventId"));
            Assert.AreEqual(16, WebApiTest.Node<int>(0, 0, "ReservedImportId"));
            Assert.AreEqual(tripId, WebApiTest.Node<int>(0, 0, "TripId"));
            Assert.AreEqual("Jim", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, 0, "Surname"));
            Assert.AreEqual(new DateTime(1984, 09, 10), WebApiTest.Node<DateTime>(0, 0, "Birthdate"));
            Assert.AreEqual("190008", WebApiTest.Node(0, 0, "BookingCode"));
            Assert.IsEmpty(WebApiTest.Node(0, 0, "BookingNumber"));
            Assert.AreEqual("3453474576245", WebApiTest.Node(0, 0, "DocumentNo"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasPhoto"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasCreditCard"));
            Assert.AreEqual("CheckOut", WebApiTest.Node(0, 0, "CheckInStatus"));

            // Check count - just check Jim Brooker
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Pass
            Assert.Pass();
        }


        [Test]
        [Ignore("Ignored due to +-3 days filter on event search")]
        public void ChangeCheckInStatus_FromCheckoutToUnknown_ReturnStatusBadRequest()
        {
            // Confirm CHECK-OUT status 
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/SearchTravelers?searchParam=&eventId=1600";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            var tripId = WebApiTest.Node<int>(0, 0, "TripId");

            // Assertions - Jim Booker
            Assert.AreEqual(1600, WebApiTest.Node<int>(0, 0, "EventId"));
            Assert.AreEqual(16, WebApiTest.Node<int>(0, 0, "ReservedImportId"));
            Assert.Greater(tripId, 0);
            Assert.AreEqual("Jim", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, 0, "Surname"));
            Assert.AreEqual(new DateTime(1984, 09, 10), WebApiTest.Node<DateTime>(0, 0, "Birthdate"));
            Assert.AreEqual("190008", WebApiTest.Node(0, 0, "BookingCode"));
            Assert.IsEmpty(WebApiTest.Node(0, 0, "BookingNumber"));
            Assert.AreEqual("3453474576245", WebApiTest.Node(0, 0, "DocumentNo"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasPhoto"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasCreditCard"));
            Assert.AreEqual("CheckOut", WebApiTest.Node(0, 0, "CheckInStatus"));

            // Check count - just check Jim Brooker
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Change to UNKNOW status - Not allowed
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Booking/ChangeCheckInStatus?retUId={tripId}& checkInStatus=3";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, null, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Pass
            Assert.Pass();
        }


        [Test]
        [Ignore("Ignored due to +-3 days filter on event search")]
        public void ChangeCheckInStatus_FromNoShowToCheckout_ReturnStatusBadRequest()
        {
            // Confirm NOSHOW status 
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/SearchTravelers?searchParam=&eventId=1600";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            var tripId = WebApiTest.Node<int>(0, 0, "TripId");

            // Assertions - Jim Booker
            Assert.AreEqual(1600, WebApiTest.Node<int>(0, 0, "EventId"));
            Assert.AreEqual(16, WebApiTest.Node<int>(0, 0, "ReservedImportId"));
            Assert.Greater(tripId, 0);
            Assert.AreEqual("Jim", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, 0, "Surname"));
            Assert.AreEqual(new DateTime(1984, 09, 10), WebApiTest.Node<DateTime>(0, 0, "Birthdate"));
            Assert.AreEqual("190008", WebApiTest.Node(0, 0, "BookingCode"));
            Assert.IsEmpty(WebApiTest.Node(0, 0, "BookingNumber"));
            Assert.AreEqual("3453474576245", WebApiTest.Node(0, 0, "DocumentNo"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasPhoto"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasCreditCard"));
            Assert.AreEqual("NoShow", WebApiTest.Node(0, 0, "CheckInStatus"));

            // Check count - just check Jim Brooker
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));


            // Change to CHECK-OUT status - Not allowed
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/ChangeCheckInStatus?retUId=33789&checkInStatus=1";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, null, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Pass
            Assert.Pass();
        }


        [Test]
        [Ignore("Ignored due to +-3 days filter on event search")]
        public void ChangeCheckInStatus_FromUnknownToCheckout_ReturnStatusBadRequest()
        {
            // Confirm UNKNOWN status 
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/SearchTravelers?searchParam=&eventId=1600";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            var tripId = WebApiTest.Node<int>(0, 0, "TripId");

            // Assertions - Jim Booker
            Assert.AreEqual(1600, WebApiTest.Node<int>(0, 0, "EventId"));
            Assert.AreEqual(16, WebApiTest.Node<int>(0, 0, "ReservedImportId"));
            Assert.Greater(tripId, 0);
            Assert.AreEqual("Jim", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, 0, "Surname"));
            Assert.AreEqual(new DateTime(1984, 09, 10), WebApiTest.Node<DateTime>(0, 0, "Birthdate"));
            Assert.AreEqual("190008", WebApiTest.Node(0, 0, "BookingCode"));
            Assert.IsEmpty(WebApiTest.Node(0, 0, "BookingNumber"));
            Assert.AreEqual("3453474576245", WebApiTest.Node(0, 0, "DocumentNo"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasPhoto"));
            Assert.AreEqual(true, WebApiTest.Node<bool>(0, 0, "HasCreditCard"));
            Assert.AreEqual("Unknown", WebApiTest.Node(0, 0, "CheckInStatus"));

            // Check count - just check Jim Brooker
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Change to CHECK-OUT status - Not allowed
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = "Api/Booking/ChangeCheckInStatus?retUId=33789&checkInStatus=1";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, null, WebApiTest.ThrowAnyException.No_AsItIsExpected);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.BadRequest, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Pass
            Assert.Pass();
        }


        [Test]
        [Ignore("Ignored due to +-3 days filter on event search.")]
        public void GetFolioDocumentsForCheckIn_ReturnStatusOk()
        {
            // GET DOCUMENTS - to check at the begining
            //---------------------------
            // Main api path
            var retUId = 33789;
            WebApiTest.ApiUriRequest = $"api/Booking/GetFolioDocumentsForCheckIn?retUId={retUId}";

            // Send Get request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Assertions documents for Jim Booker
            int idx = 0;
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "IssuingCountryId"));
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "ValidCountryId"));
            Assert.AreEqual("P", WebApiTest.Node(0, idx, "Type"));
            Assert.AreEqual("Jim", WebApiTest.Node(0, idx, "Firstname"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "MiddleName"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, idx, "Surname"));
            Assert.AreEqual("3453474576245", WebApiTest.Node(0, idx, "Number"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuedBy"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuePlace"));
            Assert.AreEqual(new DateTime(1800, 01, 01), WebApiTest.Node<DateTime>(0, idx, "IssueDate"));
            Assert.AreEqual(new DateTime(2200, 01, 01), WebApiTest.Node<DateTime>(0, idx, "ExpiryDate"));
            idx++;
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "IssuingCountryId"));
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "ValidCountryId"));
            Assert.AreEqual("V", WebApiTest.Node(0, idx, "Type"));
            Assert.AreEqual("Jim", WebApiTest.Node(0, idx, "Firstname"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "MiddleName"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, idx, "Surname"));
            Assert.AreEqual("4111 1111 1111 1111", WebApiTest.Node(0, idx, "Number"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuedBy"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuePlace"));
            Assert.AreEqual(new DateTime(1800, 01, 01), WebApiTest.Node<DateTime>(0, idx, "IssueDate"));
            Assert.AreEqual(new DateTime(2019, 12, 05), WebApiTest.Node<DateTime>(0, idx, "ExpiryDate"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Pass
            Assert.Pass();
        }


        [Test]
        [Ignore("Ignored due to +-3 days filter on event search. No Delete request")]
        public void AddFolioDocumentsForCheckIn_ReturnStatusOk()
        {
            // GET DOCUMENTS - to check at the begining
            //---------------------------
            // Main api path
            var retUId = 33789;
            WebApiTest.ApiUriRequest = $"api/Booking/GetFolioDocumentsForCheckIn?retUId={retUId}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Assertions documents for Jim Booker
            int idx = 0;
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "IssuingCountryId"));
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "ValidCountryId"));
            Assert.AreEqual("P", WebApiTest.Node(0, idx, "Type"));
            Assert.AreEqual("Jim", WebApiTest.Node(0, idx, "Firstname"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "MiddleName"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, idx, "Surname"));
            Assert.AreEqual("3453474576245", WebApiTest.Node(0, idx, "Number"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuedBy"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuedPlace"));
            Assert.AreEqual(new DateTime(1800, 01, 01), WebApiTest.Node<DateTime>(0, idx, "IssueDate"));
            Assert.AreEqual(new DateTime(2200, 01, 01), WebApiTest.Node<DateTime>(0, idx, "ExpiryDate"));
            idx++;
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "IssuingCountryId"));
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "ValidCountryId"));
            Assert.AreEqual("V", WebApiTest.Node(0, idx, "Type"));
            Assert.AreEqual("Jim", WebApiTest.Node(0, idx, "Firstname"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "MiddleName"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, idx, "Surname"));
            Assert.AreEqual("4111 1111 1111 1111", WebApiTest.Node(0, idx, "Number"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuedBy"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuedPlace"));
            Assert.AreEqual(new DateTime(1800, 01, 01), WebApiTest.Node<DateTime>(0, idx, "IssueDate"));
            Assert.AreEqual(new DateTime(2019, 12, 05), WebApiTest.Node<DateTime>(0, idx, "ExpiryDate"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // ADD NEW DOCUMENT
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Booking/AddFolioDocumentForCheckIn?retUId={retUId}";

            string bodyDoc =
                "{" +
                    "\"Type\":\"P\"," +
                    "\"Firstname\":\"Kozia\"," +
                    "\"MiddleName\":\"kocica\"," +
                    "\"Surname\":\"Wolcia\"," +
                    "\"Number\":\"5395876354\"," +
                    "\"IssuedBy\":\"Duda\"," +
                    "\"IssuedPlace\":\"Mgielkowo\"," +
                    "\"IssueDate\":\"2019-03-02\"," +
                    "\"ExpiryDate\":\"2019-07-05\"," +
                    "\"IssuingCountryId\":274," +
                    "\"ValidCountryId\":274" +
                "}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, bodyDoc);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // GET DOCUMENTS again
            //---------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"api/Booking/GetFolioDocumentsForCheckIn?retUId={retUId}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Assertions documents for Jim Booker
            idx = 0;
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "IssuingCountryId"));
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "ValidCountryId"));
            Assert.AreEqual("P", WebApiTest.Node(0, idx, "Type"));
            Assert.AreEqual("Jim", WebApiTest.Node(0, idx, "Firstname"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "MiddleName"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, idx, "Surname"));
            Assert.AreEqual("3453474576245", WebApiTest.Node(0, idx, "Number"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuedBy"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuedPlace"));
            Assert.AreEqual(new DateTime(1800, 01, 01), WebApiTest.Node<DateTime>(0, idx, "IssueDate"));
            Assert.AreEqual(new DateTime(2200, 01, 01), WebApiTest.Node<DateTime>(0, idx, "ExpiryDate"));
            idx++;
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "IssuingCountryId"));
            Assert.AreEqual(68, WebApiTest.Node<int>(0, idx, "ValidCountryId"));
            Assert.AreEqual("V", WebApiTest.Node(0, idx, "Type"));
            Assert.AreEqual("Jim", WebApiTest.Node(0, idx, "Firstname"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "MiddleName"));
            Assert.AreEqual("Booker", WebApiTest.Node(0, idx, "Surname"));
            Assert.AreEqual("4111 1111 1111 1111", WebApiTest.Node(0, idx, "Number"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuedBy"));
            Assert.IsEmpty(WebApiTest.Node(0, idx, "IssuedPlace"));
            Assert.AreEqual(new DateTime(1800, 01, 01), WebApiTest.Node<DateTime>(0, idx, "IssueDate"));
            Assert.AreEqual(new DateTime(2019, 12, 05), WebApiTest.Node<DateTime>(0, idx, "ExpiryDate"));
            idx++;
            Assert.AreEqual(274, WebApiTest.Node<int>(0, idx, "IssuingCountryId"));
            Assert.AreEqual(274, WebApiTest.Node<int>(0, idx, "ValidCountryId"));
            Assert.AreEqual("P", WebApiTest.Node(0, idx, "Type"));
            Assert.AreEqual("Kozia", WebApiTest.Node(0, idx, "Firstname"));
            Assert.AreEqual("kocica",WebApiTest.Node(0, idx, "MiddleName"));
            Assert.AreEqual("Wolcia", WebApiTest.Node(0, idx, "Surname"));
            Assert.AreEqual("5395876354", WebApiTest.Node(0, idx, "Number"));
            Assert.AreEqual("Duda", WebApiTest.Node(0, idx, "IssuedBy"));
            Assert.AreEqual("Mgielkowo", WebApiTest.Node(0, idx, "IssuedPlace"));
            Assert.AreEqual(new DateTime(2019, 03, 02), WebApiTest.Node<DateTime>(0, idx, "IssueDate"));
            Assert.AreEqual(new DateTime(2019, 07, 05), WebApiTest.Node<DateTime>(0, idx, "ExpiryDate"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Pass
            Assert.Pass();
        }


        [Test]
        [Ignore("Ignored due to +-3 days filter on event search")]
        public void GetCheckInSummary_ReturnStatusOk()
        {
            // GET SUMMARY 
            //---------------------------
            // Main api path
            var retUId = 36091;
            WebApiTest.ApiUriRequest = $"api/Booking/GetCheckInSummary?tripId={retUId}";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Assertions 
            Assert.AreEqual(23358, WebApiTest.Node<int>("BookingId"));
            Assert.AreEqual("190262", WebApiTest.Node("BookingCode"));
            Assert.AreEqual("", WebApiTest.Node("BookingNo"));
            Assert.AreEqual(new DateTime(2019, 07, 08), WebApiTest.Node<DateTime>("EmbarkDate"));
            Assert.AreEqual(new DateTime(2019, 07, 11), WebApiTest.Node<DateTime>("DisembarkDate"));
            Assert.AreEqual("Jim", WebApiTest.Node("Forename"));
            Assert.AreEqual("Booker", WebApiTest.Node("Surname"));
            Assert.AreEqual(new DateTime(1984, 09, 10), WebApiTest.Node<DateTime>("BirthDate"));
            Assert.AreEqual("M", WebApiTest.Node("Gender"));
            Assert.AreEqual("Australia", WebApiTest.Node("Nationality"));
            Assert.AreEqual("P", WebApiTest.Node("CategoryCode"));
            Assert.AreEqual("Premium - double bed, per person", WebApiTest.Node("CategoryName"));
            Assert.AreEqual("10", WebApiTest.Node("UnitNo"));
            Assert.AreEqual(18, WebApiTest.Node<int>("ReservedImportId"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());
 }

        [Test]
        public void ReplaceTravelerInBooking_AddExistingFolioAndReturnStatusOk()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "api/Booking/ReplaceTraveler";

            // Change to folio Tereshchenko Nat, folUId = 34406
            string existingFolio = "{\"FolioId\":31377,\"NationalityId\":0,\"LanguageId\":0,\"AgeGroupId\":6,\"FlagId\":0,\"SalutationId\":0,\"Forename\":\"Nat\",\"Midname\":\"\",\"Surname\":\"Tereshchenko\",\"FolioType\":\"P\",\"RepeaterNo\":\"\",\"Title\":\"\",\"Alias\":\"\",\"Gender\":\"F\",\"BirthDate\":\"1982-12-04T00:00:00\",\"Phone1\":\"48444555334\",\"Phone2\":\"\",\"Phone3\":\"\",\"Email\":\"natasha@rescompany.com\",\"Street\":\"xxxxxxx\",\"City\":\"xxxxxxxxxx\",\"ProvinceId\":0,\"PostalCode\":\"xxxxxxxxxx\",\"CountryId\":272,\"Flex01\":\"\",\"Flex02\":\"\",\"Flex03\":\"\",\"Flex04\":\"\",\"PrivacyFlex01\":-100000000,\"PrivacyFlex02\":-100000000,\"PrivacyFlex03\":-100000000,\"PrivacyFlex04\":-100000000,\"PrivacyFlex05\":-100000000,\"PrivacyFlex06\":-100000000,\"PrivacyFlex07\":-100000000,\"PrivacyFlex08\":-100000000,\"Primary\":0,\"CategoryId\":0,\"UnitId\":0,\"CategoryCode\":null,\"UnitNo\":null,\"AgeGroupName\":null,\"FlagValue\":0,\"RefUId\":0,\"EnquiryId\":0,\"MailingName\":\"\",\"EnvelopeName\":\"\",\"OldFolioId\":917,\"BookingId\":\"21191\"}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, existingFolio);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Assert on replase traveler in booking
            WebApiTest.ApiUriRequest = "api/Folio/GetTravelersForPrimaryTrip?bookingId=21191";

            // Send GET request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for ReplaceTravelerInBooking_AddExistingFolioAndReturnStatusOk

            // Asserts
            Assert.AreEqual(31377, WebApiTest.Node<int>(0, 0, "FolioId"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "NationalityId"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "LanguageId"));
            Assert.AreEqual(6, WebApiTest.Node<int>(0, 0, "AgeGroupId"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "FlagId"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "SalutationId"));
            Assert.AreEqual("Nat", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Midname"));
            Assert.AreEqual("Tereshchenko", WebApiTest.Node(0, 0, "Surname"));
            Assert.AreEqual("P", WebApiTest.Node(0, 0, "FolioType"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "RepeaterNo"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Title"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Alias"));
            Assert.AreEqual("F", WebApiTest.Node(0, 0, "Gender"));
            Assert.AreEqual(new DateTime(1982, 12, 4), WebApiTest.Node<DateTime>(0, 0, "BirthDate"));
            Assert.AreEqual(48444555334, WebApiTest.Node<double>(0, 0, "Phone1"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Phone2"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Phone3"));
            Assert.AreEqual("natasha@rescompany.com", WebApiTest.Node(0, 0, "Email"));
            Assert.AreEqual("xxxxxxx", WebApiTest.Node(0, 0, "Street"));
            Assert.AreEqual("xxxxxxxxxx", WebApiTest.Node(0, 0, "City"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "ProvinceId"));
            Assert.AreEqual("xxxxxxxxxx", WebApiTest.Node(0, 0, "PostalCode"));
            Assert.AreEqual(272, WebApiTest.Node<int>(0, 0, "CountryId"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex01"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex02"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex03"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex04"));
            Assert.AreEqual(-100000000, WebApiTest.Node<int>(0, 0, "PrivacyFlex01"));
            Assert.AreEqual(-100000000, WebApiTest.Node<int>(0, 0, "PrivacyFlex02"));
            Assert.AreEqual(-100000000, WebApiTest.Node<int>(0, 0, "PrivacyFlex03"));
            Assert.AreEqual(-100000000, WebApiTest.Node<int>(0, 0, "PrivacyFlex04"));
            Assert.AreEqual(-100000000, WebApiTest.Node<int>(0, 0, "PrivacyFlex05"));
            Assert.AreEqual(-100000000, WebApiTest.Node<int>(0, 0, "PrivacyFlex06"));
            Assert.AreEqual(-100000000, WebApiTest.Node<int>(0, 0, "PrivacyFlex07"));
            Assert.AreEqual(-100000000, WebApiTest.Node<int>(0, 0, "PrivacyFlex08"));
            Assert.AreEqual(1, WebApiTest.Node<int>(0, 0, "Primary"));
            Assert.AreEqual(16, WebApiTest.Node<int>(0, 0, "CategoryId"));
            Assert.AreEqual(40, WebApiTest.Node<int>(0, 0, "UnitId"));
            Assert.AreEqual("S SS", WebApiTest.Node(0, 0, "CategoryCode"));
            Assert.AreEqual("6  (b)", WebApiTest.Node(0, 0, "UnitNo"));
            Assert.AreEqual("Adult", WebApiTest.Node(0, 0, "AgeGroupName"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "FlagValue"));
            Assert.AreEqual(33781, WebApiTest.Node<int>(0, 0, "RefUId"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "EnquiryId"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "MailingName"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "EnvelopeName"));

            #endregion Asserts for ReplaceTravelerInBooking_AddExistingFolioAndReturnStatusOk

            // Check count - just check Nat Tereshchenko
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Replace folio back
            // Main api path
            WebApiTest.ApiUriRequest = "api/Booking/ReplaceTraveler";

            // Change to folio Stacy Brenner, folUId = 34406
            string backFolio = "{\"FolioId\":917,\"NationalityId\":68,\"LanguageId\":5,\"AgeGroupId\":6,\"FlagId\":1720,\"SalutationId\":0,\"Forename\":\"Stacy\",\"Midname\":\"\",\"Surname\":\"Brenner\",\"FolioType\":\"P\",\"RepeaterNo\":\"\",\"Title\":\"\",\"Alias\":\"\",\"Gender\":\"N\",\"BirthDate\":\"1984-09-10T00:00:00\",\"Phone1\":\"410 923 1447\",\"Phone2\":\"\",\"Phone3\":\"\",\"Email\":\"\",\"Street\":\"1169 Gumbottom road\",\"City\":\"Crownville\",\"ProvinceId\":1383,\"PostalCode\":\"21032\",\"CountryId\":275,\"Flex01\":\"\",\"Flex02\":\"\",\"Flex03\":\"\",\"Flex04\":\"\",\"PrivacyFlex01\":0,\"PrivacyFlex02\":0,\"PrivacyFlex03\":0,\"PrivacyFlex04\":1,\"PrivacyFlex05\":0,\"PrivacyFlex06\":0,\"PrivacyFlex07\":0,\"PrivacyFlex08\":0,\"Primary\":0,\"CategoryId\":0,\"UnitId\":0,\"CategoryCode\":null,\"UnitNo\":null,\"AgeGroupName\":null,\"FlagValue\":65535,\"RefUId\":0,\"EnquiryId\":0,\"MailingName\":\"\",\"EnvelopeName\":\"brebber@ieee.org\",\"OldFolioId\":31377,\"BookingId\":\"21191\"}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, backFolio);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Assert on replase traveler back in booking
            WebApiTest.ApiUriRequest = "api/Folio/GetTravelersForPrimaryTrip?bookingId=21191";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for ReplaceTravelerInBooking_AddExistingFolioAndReturnStatusOk

            // Asserts
            Assert.AreEqual(917, WebApiTest.Node<int>(0, 0, "FolioId"));
            Assert.AreEqual(68, WebApiTest.Node<int>(0, 0, "NationalityId"));
            Assert.AreEqual(5, WebApiTest.Node<int>(0, 0, "LanguageId"));
            Assert.AreEqual(6, WebApiTest.Node<int>(0, 0, "AgeGroupId"));
            Assert.AreEqual(1720, WebApiTest.Node<int>(0, 0, "FlagId"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "SalutationId"));
            Assert.AreEqual("Stacy", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Midname"));
            Assert.AreEqual("Brenner", WebApiTest.Node(0, 0, "Surname"));
            Assert.AreEqual("P", WebApiTest.Node(0, 0, "FolioType"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "RepeaterNo"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Title"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Alias"));
            Assert.AreEqual("N", WebApiTest.Node(0, 0, "Gender"));
            Assert.AreEqual(new DateTime(1984, 9, 10), WebApiTest.Node<DateTime>(0, 0, "BirthDate"));
            Assert.AreEqual("410 923 1447", WebApiTest.Node(0, 0, "Phone1"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Phone2"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Phone3"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Email"));
            Assert.AreEqual("1169 Gumbottom road", WebApiTest.Node(0, 0, "Street"));
            Assert.AreEqual("Crownville", WebApiTest.Node(0, 0, "City"));
            Assert.AreEqual(1383, WebApiTest.Node<int>(0, 0, "ProvinceId"));
            Assert.AreEqual(21032, WebApiTest.Node<int>(0, 0, "PostalCode"));
            Assert.AreEqual(275, WebApiTest.Node<int>(0, 0, "CountryId"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex01"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex02"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex03"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex04"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "PrivacyFlex01"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "PrivacyFlex02"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "PrivacyFlex03"));
            Assert.AreEqual(1, WebApiTest.Node<int>(0, 0, "PrivacyFlex04"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "PrivacyFlex05"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "PrivacyFlex06"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "PrivacyFlex07"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "PrivacyFlex08"));
            Assert.AreEqual(1, WebApiTest.Node<int>(0, 0, "Primary"));
            Assert.AreEqual(16, WebApiTest.Node<int>(0, 0, "CategoryId"));
            Assert.AreEqual(40, WebApiTest.Node<int>(0, 0, "UnitId"));
            Assert.AreEqual("S SS", WebApiTest.Node(0, 0, "CategoryCode"));
            Assert.AreEqual("6  (b)", WebApiTest.Node(0, 0, "UnitNo"));
            Assert.AreEqual("Adult", WebApiTest.Node(0, 0, "AgeGroupName"));
            Assert.AreEqual(65535, WebApiTest.Node<int>(0, 0, "FlagValue"));
            Assert.AreEqual(33781, WebApiTest.Node<int>(0, 0, "RefUId"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "EnquiryId"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "MailingName"));
            Assert.AreEqual("brebber@ieee.org", WebApiTest.Node(0, 0, "EnvelopeName"));

            #endregion Asserts for ReplaceTravelerInBooking_AddExistingFolioAndReturnStatusOk

            // Check count - just check Stasy Brenner
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Pass
            Assert.Pass();
        }


        [Test]
        public void ReplaceTravelerInBooking_AddNewFolioAndReturnStatusOk()
        {
            // Main api path
            WebApiTest.ApiUriRequest = "api/Booking/ReplaceTraveler";

            // Replace folio
            DateTime time = DateTime.Now;
            string newFolio = "{\"Surname\":\"test" + time.ToString("o") + "\",\"FolioId\":0,\"OldFolioId\":917,\"BookingId\":\"344\"}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, newFolio);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Get new folio id
            WebApiTest.ApiUriRequest = "api/Folio/GetTravelersForPrimaryTrip?bookingId=344";

            // Send GET request with authorization
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            int folioId = WebApiTest.Node<int>(0, 0, "FolioId");

            // Assert on replase traveler in booking
            WebApiTest.ApiUriRequest = "api/Folio/GetTravelersForPrimaryTrip?bookingId=344";

            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Asserts
            Assert.IsTrue(folioId>0);
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "NationalityId"));
            Assert.AreEqual(5, WebApiTest.Node<int>(0, 0, "LanguageId"));
            Assert.AreEqual(6, WebApiTest.Node<int>(0, 0, "AgeGroupId"));
            Assert.AreEqual(-21, WebApiTest.Node<int>(0, 0, "FlagId"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "SalutationId"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Midname"));
            Assert.AreEqual("test" + time.ToString("o"), WebApiTest.Node(0, 0, "Surname"));
            Assert.AreEqual("P", WebApiTest.Node(0, 0, "FolioType"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "RepeaterNo"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Title"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Alias"));
            Assert.AreEqual("N", WebApiTest.Node(0, 0, "Gender"));
            Assert.AreEqual(new DateTime(1800, 01, 01), WebApiTest.Node<DateTime>(0, 0, "BirthDate"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Phone1"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Phone2"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Phone3"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Email"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Street"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "City"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "ProvinceId"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "PostalCode"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "CountryId"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex01"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex02"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex03"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "Flex04"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "PrivacyFlex01"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "PrivacyFlex02"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "PrivacyFlex03"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "PrivacyFlex04"));
            Assert.AreEqual(-100000000, WebApiTest.Node<int>(0, 0, "PrivacyFlex05"));
            Assert.AreEqual(-100000000, WebApiTest.Node<int>(0, 0, "PrivacyFlex06"));
            Assert.AreEqual(1, WebApiTest.Node<int>(0, 0, "PrivacyFlex07"));
            Assert.AreEqual(-100000000, WebApiTest.Node<int>(0, 0, "PrivacyFlex08"));
            Assert.AreEqual(1, WebApiTest.Node<int>(0, 0, "Primary"));
            Assert.AreEqual(16, WebApiTest.Node<int>(0, 0, "CategoryId"));
            Assert.AreEqual(-972, WebApiTest.Node<int>(0, 0, "UnitId"));
            Assert.AreEqual("S SS", WebApiTest.Node(0, 0, "CategoryCode"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "UnitNo"));
            Assert.AreEqual("Adult", WebApiTest.Node(0, 0, "AgeGroupName"));
            Assert.AreEqual(-1.0000, WebApiTest.Node<float>(0, 0, "FlagValue"));
            Assert.AreEqual(946, WebApiTest.Node<int>(0, 0, "RefUId"));
            Assert.AreEqual(0, WebApiTest.Node<int>(0, 0, "EnquiryId"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "MailingName"));
            Assert.AreEqual("", WebApiTest.Node(0, 0, "EnvelopeName"));

            // Check count - just check new folio
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Replace folio back
            // Main api path
            WebApiTest.ApiUriRequest = "api/Booking/ReplaceTraveler";

            // Replace traveler back
            string oldFolio = "{\"FolioId\":917,\"NationalityUId\":68,\"LanguageUId\":5,\"AgeGroupUId\":6,\"FlagId\":1720,\"SalutationUId\":0,\"Forename\":\"Stacy\",\"Midname\":\"\",\"Surname\":\"Brenner\",\"FolioType\":\"P\",\"RepeaterNo\":\"\",\"Title\":\"\",\"Alias\":\"\",\"Gender\":\"N\",\"BirthDate\":\"1984-09-10T00:00:00\",\"Phone1\":\"410 923 1447\",\"Phone2\":\"\",\"Phone3\":\"\",\"Email\":\"\",\"Street\":\"1169 Gumbottom road\",\"City\":\"Crownville\",\"ProvinceUId\":1383,\"PostalCode\":\"21032\",\"CountryId\":275,\"Flex01\":\"\",\"Flex02\":\"\",\"Flex03\":\"\",\"Flex04\":\"\",\"PrivacyFlex01\":0,\"PrivacyFlex02\":0,\"PrivacyFlex03\":0,\"PrivacyFlex04\":1,\"PrivacyFlex05\":0,\"PrivacyFlex06\":0,\"PrivacyFlex07\":0,\"PrivacyFlex08\":0,\"Primary\":0,\"CategoryUId\":0,\"UnitUId\":0,\"CategoryCode\":null,\"UnitNo\":null,\"AgeGroupId\":\"A\",\"AgeGroupName\":null,\"FlagValue\":65535,\"RefUId\":0,\"EnquiryId\":0,\"MailingName\":\"\",\"EnvelopeName\":\"brebber@ieee.org\",\"OldFolioId\":" + folioId + ",\"BookingId\":\"344\"}";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, oldFolio);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check Stasy Brenner
            // Assert on replase traveler back in booking
            WebApiTest.ApiUriRequest = "api/Folio/GetTravelersForPrimaryTrip?bookingId=344";

            // Send Post request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check Stasy Brenner
            Assert.AreEqual("Stacy", WebApiTest.Node(0, 0, "Forename"));
            Assert.AreEqual(917, WebApiTest.Node<int>(0, 0, "FolioId"));
            Assert.AreEqual("Brenner", WebApiTest.Node(0, 0, "Surname"));

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check count - just check Stasy Brenner
            Assert.IsTrue(WebApiTest.IsCountCorrect(WebApiTest.CheckCount.Ignore));

            // Pass
            Assert.Pass();
        }


        [Test]
        public void GetBookingDetailsTab_ReturnAllCorrectData()
        {
            // Main api path
            var rebUid = 21214;
            WebApiTest.ApiUriRequest = $"Api/Booking/GetBookingDetails?bookingId={rebUid}";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetBookingDetailsTab_ReturnAllCorrectData

            // Asserts
            Assert.AreEqual(190044, WebApiTest.Node<int>("BookingCode"));
            Assert.AreEqual("FLT/L-C", WebApiTest.Node("ArrivalPortCode"));
            Assert.AreEqual("TRINDEP", WebApiTest.Node("DeparturePortCode"));
            Assert.AreEqual("O", WebApiTest.Node("BookingStatus"));
            Assert.AreEqual("AUD", WebApiTest.Node("Currency"));
            Assert.AreEqual(14, WebApiTest.Node<int>("FacilityId"));
            Assert.AreEqual("Spoilsport", WebApiTest.Node("FacilityName"));
            Assert.AreEqual("4 night Fly Dive Coral Sea", WebApiTest.Node("EventName"));
            Assert.AreEqual(1911211, WebApiTest.Node<int>("EventCode"));
            Assert.AreEqual(1653, WebApiTest.Node<int>("EventId"));
            Assert.AreEqual(4, WebApiTest.Node<int>("EventDuration"));
            Assert.AreEqual(new DateTime(2019, 11, 21), WebApiTest.Node<DateTime>("EventBeginDate"));
            Assert.AreEqual(new DateTime(2019, 11, 25), WebApiTest.Node<DateTime>("EventEndDate"));

            #endregion Asserts for GetBookingDetailsTab_ReturnAllCorrectData

            // check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Pass
            Assert.Pass();
        }


        [Test]
        public void GetBookingDetailsForChangeBooking_ReturnCorrectData()
        {
            // Main api path
            var rebUid = 21232;
            WebApiTest.ApiUriRequest = $"Api/Booking/GetBookingDetailsForChangeBooking?bookingId={rebUid}";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetBookingDetailsForChangeBooking_ReturnCorrectData

            // Asserts
            Assert.AreEqual(21232, WebApiTest.Node<int>("BookingId"));
            Assert.AreEqual(654, WebApiTest.Node<int>("BookingNumber"));
            Assert.AreEqual("R", WebApiTest.Node("BookingStatus"));
            Assert.AreEqual(20909, WebApiTest.Node<int>("BillingFolioId"));
            Assert.AreEqual(40696, WebApiTest.Node<int>("SourceId"));
            Assert.AreEqual(21404, WebApiTest.Node<int>("BookingTypeId"));
            Assert.AreEqual(40686, WebApiTest.Node<int>("MarketingId"));
            Assert.AreEqual(40702, WebApiTest.Node<int>("OriginId"));
            Assert.AreEqual(40700, WebApiTest.Node<int>("ReservationTypeId"));
            Assert.AreEqual(40690, WebApiTest.Node<int>("PaymentTypeId"));
            Assert.AreEqual(0, WebApiTest.Node<int>("EnquiryId"));
            Assert.AreEqual(111, WebApiTest.Node<int>("Flex01"));
            Assert.AreEqual(222, WebApiTest.Node<int>("Flex02"));
            Assert.AreEqual(333, WebApiTest.Node<int>("Flex03"));
            Assert.AreEqual("comment", WebApiTest.Node("Comments"));
            Assert.AreEqual(53, WebApiTest.Node<int>("UserId"));
            Assert.AreEqual(true, WebApiTest.Node<bool>("CanModifyBooking"));
            Assert.AreEqual(true, WebApiTest.Node<bool>("CanUseWebManifest"));

            #endregion Asserts for GetBookingDetailsForChangeBooking_ReturnCorrectData

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Pass
            Assert.Pass();
        }


        [Test]
        public void ChangeBooking_ReturnCorrectStatus()
        {
            // Check Booking before changes
            //-----------------------------
            // Main api path
            var rebUid = 21232;
            WebApiTest.ApiUriRequest = $"Api/Booking/GetBookingDetailsForChangeBooking?bookingId={rebUid}";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetBookingDetailsForChangeBooking_BeforeChanges

            // Asserts
            Assert.AreEqual(21232, WebApiTest.Node<int>("BookingId"));
            Assert.AreEqual(654, WebApiTest.Node<int>("BookingNumber"));
            Assert.AreEqual("R", WebApiTest.Node("BookingStatus"));
            Assert.AreEqual(20909, WebApiTest.Node<int>("BillingFolioId"));
            Assert.AreEqual(40696, WebApiTest.Node<int>("SourceId"));
            Assert.AreEqual(21404, WebApiTest.Node<int>("BookingTypeId"));
            Assert.AreEqual(40686, WebApiTest.Node<int>("MarketingId"));
            Assert.AreEqual(40702, WebApiTest.Node<int>("OriginId"));
            Assert.AreEqual(40700, WebApiTest.Node<int>("ReservationTypeId"));
            Assert.AreEqual(40690, WebApiTest.Node<int>("PaymentTypeId"));
            Assert.AreEqual(0, WebApiTest.Node<int>("EnquiryId"));
            Assert.AreEqual(111, WebApiTest.Node<int>("Flex01"));
            Assert.AreEqual(222, WebApiTest.Node<int>("Flex02"));
            Assert.AreEqual(333, WebApiTest.Node<int>("Flex03"));
            Assert.AreEqual("comment", WebApiTest.Node("Comments"));
            Assert.AreEqual(53, WebApiTest.Node<int>("UserId"));
            Assert.AreEqual(true, WebApiTest.Node<bool>("CanModifyBooking"));
            Assert.AreEqual(true, WebApiTest.Node<bool>("CanUseWebManifest"));

            #endregion Asserts for GetBookingDetailsForChangeBooking_BeforeChanges

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Change Booking data
            //--------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Booking/ChangeBooking";

            string payload = @"
            {
                ""BookingId"": 21232,
                ""BookingNumber"": ""123"",
                ""BookingStatus"": ""R"",
                ""BillingFolioId"": 12938,
                ""SourceId"": 40697,
                ""BookingTypeId"": 3179,
                ""MarketingId"": 40689,
                ""OriginId"": 40701,
                ""ReservationTypeId"": 40699,
                ""PaymentTypeId"": 40691,
                ""EnquiryId"": 0,
                ""Flex01"": ""1119"",
                ""Flex02"": ""2229"",
                ""Flex03"": ""3339"",
                ""Comments"": ""comment-changed"",
                ""UserId"": 53,
                ""CanModifyBooking"": true,
                ""CanUseWebManifest"": false
            }
            ";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, payload);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Check Booking after changes
            //-----------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Booking/GetBookingDetailsForChangeBooking?bookingId={rebUid}";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetBookingDetailsForChangeBooking_AfterChanges

            // Asserts
            Assert.AreEqual(21232, WebApiTest.Node<int>("BookingId"));
            Assert.AreEqual(123, WebApiTest.Node<int>("BookingNumber"));
            Assert.AreEqual("R", WebApiTest.Node("BookingStatus"));
            Assert.AreEqual(12938, WebApiTest.Node<int>("BillingFolioId"));
            Assert.AreEqual(40697, WebApiTest.Node<int>("SourceId"));
            Assert.AreEqual(3179, WebApiTest.Node<int>("BookingTypeId"));
            Assert.AreEqual(40689, WebApiTest.Node<int>("MarketingId"));
            Assert.AreEqual(40701, WebApiTest.Node<int>("OriginId"));
            Assert.AreEqual(40699, WebApiTest.Node<int>("ReservationTypeId"));
            Assert.AreEqual(40691, WebApiTest.Node<int>("PaymentTypeId"));
            Assert.AreEqual(0, WebApiTest.Node<int>("EnquiryId"));
            Assert.AreEqual(1119, WebApiTest.Node<int>("Flex01"));
            Assert.AreEqual(2229, WebApiTest.Node<int>("Flex02"));
            Assert.AreEqual(3339, WebApiTest.Node<int>("Flex03"));
            Assert.AreEqual("comment-changed", WebApiTest.Node("Comments"));
            Assert.AreEqual(53, WebApiTest.Node<int>("UserId"));
            Assert.AreEqual(true, WebApiTest.Node<bool>("CanModifyBooking"));
            Assert.AreEqual(false, WebApiTest.Node<bool>("CanUseWebManifest"));

            #endregion Asserts for GetBookingDetailsForChangeBooking_AfterChanges

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Revert change Booking data
            //--------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Booking/ChangeBooking";

            payload = @"
            {
                ""BookingId"": 21232,
                ""BookingNumber"": ""654"",
                ""BookingStatus"": ""R"",
                ""BillingFolioId"": 20909,
                ""SourceId"": 40696,
                ""BookingTypeId"": 21404,
                ""MarketingId"": 40686,
                ""OriginId"": 40702,
                ""ReservationTypeId"": 40700,
                ""PaymentTypeId"": 40690,
                ""EnquiryId"": 0,
                ""Flex01"": ""111"",
                ""Flex02"": ""222"",
                ""Flex03"": ""333"",
                ""Comments"": ""comment"",
                ""UserId"": 53,
                ""CanModifyBooking"": true,
                ""CanUseWebManifest"": true
            }";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Post, WebApiTest.ApiUri.Request, payload);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Check Booking after revert
            //-----------------------------
            // Main api path
            WebApiTest.ApiUriRequest = $"Api/Booking/GetBookingDetailsForChangeBooking?bookingId={rebUid}";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Check StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetBookingDetailsForChangeBooking_AfterRevert

            // Asserts
            Assert.AreEqual(21232, WebApiTest.Node<int>("BookingId"));
            Assert.AreEqual(654, WebApiTest.Node<int>("BookingNumber"));
            Assert.AreEqual("R", WebApiTest.Node("BookingStatus"));
            Assert.AreEqual(20909, WebApiTest.Node<int>("BillingFolioId"));
            Assert.AreEqual(40696, WebApiTest.Node<int>("SourceId"));
            Assert.AreEqual(21404, WebApiTest.Node<int>("BookingTypeId"));
            Assert.AreEqual(40686, WebApiTest.Node<int>("MarketingId"));
            Assert.AreEqual(40702, WebApiTest.Node<int>("OriginId"));
            Assert.AreEqual(40700, WebApiTest.Node<int>("ReservationTypeId"));
            Assert.AreEqual(40690, WebApiTest.Node<int>("PaymentTypeId"));
            Assert.AreEqual(0, WebApiTest.Node<int>("EnquiryId"));
            Assert.AreEqual(111, WebApiTest.Node<int>("Flex01"));
            Assert.AreEqual(222, WebApiTest.Node<int>("Flex02"));
            Assert.AreEqual(333, WebApiTest.Node<int>("Flex03"));
            Assert.AreEqual("comment", WebApiTest.Node("Comments"));
            Assert.AreEqual(53, WebApiTest.Node<int>("UserId"));
            Assert.AreEqual(true, WebApiTest.Node<bool>("CanModifyBooking"));
            Assert.AreEqual(true, WebApiTest.Node<bool>("CanUseWebManifest"));

            #endregion Asserts for GetBookingDetailsForChangeBooking_AfterRevert

            // Check count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Pass
            Assert.Pass();
        }


        [Test]
        public void GetChangeBookingSetup_ReturnCorrectData()
        {
            // Main api path
            var rebUid = 21232;
            WebApiTest.ApiUriRequest = $"Api/Booking/GetChangeBookingSetup?bookingId={rebUid}";

            // Send request
            WebApiTest.SendRequest(HttpMethod.Get, WebApiTest.ApiUri.Request);

            // Ñheck StatusCode
            Assert.AreEqual(HttpStatusCode.OK, WebApiTest.Status<HttpStatusCode>(WebApiTest.ApiUri.Request, "StatusCode"));

            #region Asserts for GetChangeBookingSetup_ReturnCorrectData

            // Asserts
            Assert.AreEqual(40695, WebApiTest.Node<int>("BookingSource", 0, "Id"));
            Assert.AreEqual("Christmas", WebApiTest.Node("BookingSource", 0, "Name"));
            Assert.AreEqual(100, WebApiTest.Node<int>("BookingSource", 0, "Code"));
            Assert.AreEqual(40696, WebApiTest.Node<int>("BookingSource", 1, "Id"));
            Assert.AreEqual("Gardens", WebApiTest.Node("BookingSource", 1, "Name"));
            Assert.AreEqual(200, WebApiTest.Node<int>("BookingSource", 1, "Code"));
            Assert.AreEqual(40697, WebApiTest.Node<int>("BookingSource", 2, "Id"));
            Assert.AreEqual("Music", WebApiTest.Node("BookingSource", 2, "Name"));
            Assert.AreEqual(300, WebApiTest.Node<int>("BookingSource", 2, "Code"));
            Assert.AreEqual(40699, WebApiTest.Node<int>("BookingReservation", 0, "Id"));
            Assert.AreEqual("MANAGER", WebApiTest.Node("BookingReservation", 0, "Code"));
            Assert.AreEqual("Manager", WebApiTest.Node("BookingReservation", 0, "Name"));
            Assert.AreEqual(0, WebApiTest.Node<int>("BookingReservation", 0, "Type"));
            Assert.AreEqual(40700, WebApiTest.Node<int>("BookingReservation", 1, "Id"));
            Assert.AreEqual("OTHER", WebApiTest.Node("BookingReservation", 1, "Code"));
            Assert.AreEqual("Other", WebApiTest.Node("BookingReservation", 1, "Name"));
            Assert.AreEqual(0, WebApiTest.Node<int>("BookingReservation", 1, "Type"));
            Assert.AreEqual(40698, WebApiTest.Node<int>("BookingReservation", 2, "Id"));
            Assert.AreEqual("STAFF", WebApiTest.Node("BookingReservation", 2, "Code"));
            Assert.AreEqual("Staff", WebApiTest.Node("BookingReservation", 2, "Name"));
            Assert.AreEqual(0, WebApiTest.Node<int>("BookingReservation", 2, "Type"));
            Assert.AreEqual(40701, WebApiTest.Node<int>("BookingOrigin", 0, "Id"));
            Assert.AreEqual("Phone", WebApiTest.Node("BookingOrigin", 0, "Name"));
            Assert.AreEqual("1-PHONE", WebApiTest.Node("BookingOrigin", 0, "Code"));
            Assert.AreEqual(40702, WebApiTest.Node<int>("BookingOrigin", 1, "Id"));
            Assert.AreEqual("Website", WebApiTest.Node("BookingOrigin", 1, "Name"));
            Assert.AreEqual("2-WEB", WebApiTest.Node("BookingOrigin", 1, "Code"));
            Assert.AreEqual(40703, WebApiTest.Node<int>("BookingOrigin", 2, "Id"));
            Assert.AreEqual("Agents", WebApiTest.Node("BookingOrigin", 2, "Name"));
            Assert.AreEqual("3-AGNT", WebApiTest.Node("BookingOrigin", 2, "Code"));
            Assert.AreEqual(40686, WebApiTest.Node<int>("BookingMarket", 0, "Id"));
            Assert.AreEqual("Cruise Only (No air or land))", WebApiTest.Node("BookingMarket", 0, "Name"));
            Assert.AreEqual("C", WebApiTest.Node("BookingMarket", 0, "Code"));
            Assert.AreEqual(40689, WebApiTest.Node<int>("BookingMarket", 1, "Id"));
            Assert.AreEqual("Air & Cruise Only (No land)", WebApiTest.Node("BookingMarket", 1, "Name"));
            Assert.AreEqual("F", WebApiTest.Node("BookingMarket", 1, "Code"));
            Assert.AreEqual(40688, WebApiTest.Node<int>("BookingMarket", 2, "Id"));
            Assert.AreEqual("Package (Cruise, Air, and/or incl/opt Land)", WebApiTest.Node("BookingMarket", 2, "Name"));
            Assert.AreEqual("P", WebApiTest.Node("BookingMarket", 2, "Code"));
            Assert.AreEqual(40687, WebApiTest.Node<int>("BookingMarket", 3, "Id"));
            Assert.AreEqual("Cruise/Tour (Cruise, incl/or opt land, and NO Air)", WebApiTest.Node("BookingMarket", 3, "Name"));
            Assert.AreEqual("T", WebApiTest.Node("BookingMarket", 3, "Code"));
            Assert.AreEqual(3179, WebApiTest.Node<int>("BookingType", 0, "Id"));
            Assert.AreEqual("Chameleon Bookings", WebApiTest.Node("BookingType", 0, "Name"));
            Assert.AreEqual(2, WebApiTest.Node<int>("BookingType", 0, "Code"));
            Assert.AreEqual(21404, WebApiTest.Node<int>("BookingType", 1, "Id"));
            Assert.AreEqual("Voucher Booking", WebApiTest.Node("BookingType", 1, "Name"));
            Assert.AreEqual(3, WebApiTest.Node<int>("BookingType", 1, "Code"));
            Assert.AreEqual(25473, WebApiTest.Node<int>("BookingType", 2, "Id"));
            Assert.AreEqual("OceanDivers_404241", WebApiTest.Node("BookingType", 2, "Name"));
            Assert.AreEqual(5, WebApiTest.Node<int>("BookingType", 2, "Code"));
            Assert.AreEqual(25475, WebApiTest.Node<int>("BookingType", 3, "Id"));
            Assert.AreEqual("DCM_405184Y", WebApiTest.Node("BookingType", 3, "Name"));
            Assert.AreEqual(6, WebApiTest.Node<int>("BookingType", 3, "Code"));
            Assert.AreEqual(25083, WebApiTest.Node<int>("BookingType", 4, "Id"));
            Assert.AreEqual("Frog_40619M1", WebApiTest.Node("BookingType", 4, "Name"));
            Assert.AreEqual(7, WebApiTest.Node<int>("BookingType", 4, "Code"));
            Assert.AreEqual(25484, WebApiTest.Node<int>("BookingType", 5, "Id"));
            Assert.AreEqual("Frog_41002C", WebApiTest.Node("BookingType", 5, "Name"));
            Assert.AreEqual(8, WebApiTest.Node<int>("BookingType", 5, "Code"));
            Assert.AreEqual(25483, WebApiTest.Node<int>("BookingType", 6, "Id"));
            Assert.AreEqual("ScubaWorldSac_50917C", WebApiTest.Node("BookingType", 6, "Name"));
            Assert.AreEqual(9, WebApiTest.Node<int>("BookingType", 6, "Code"));
            Assert.AreEqual(25477, WebApiTest.Node<int>("BookingType", 7, "Id"));
            Assert.AreEqual("DolphinSC_51001C", WebApiTest.Node("BookingType", 7, "Name"));
            Assert.AreEqual(10, WebApiTest.Node<int>("BookingType", 7, "Code"));
            Assert.AreEqual(25481, WebApiTest.Node<int>("BookingType", 8, "Id"));
            Assert.AreEqual("AnchorGp_51105C", WebApiTest.Node("BookingType", 8, "Name"));
            Assert.AreEqual(11, WebApiTest.Node<int>("BookingType", 8, "Code"));
            Assert.AreEqual(25480, WebApiTest.Node<int>("BookingType", 9, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 9, "Name"));
            Assert.AreEqual(12, WebApiTest.Node<int>("BookingType", 9, "Code"));
            Assert.AreEqual(25474, WebApiTest.Node<int>("BookingType", 10, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 10, "Name"));
            Assert.AreEqual(13, WebApiTest.Node<int>("BookingType", 10, "Code"));
            Assert.AreEqual(25478, WebApiTest.Node<int>("BookingType", 11, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 11, "Name"));
            Assert.AreEqual(14, WebApiTest.Node<int>("BookingType", 11, "Code"));
            Assert.AreEqual(25482, WebApiTest.Node<int>("BookingType", 12, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 12, "Name"));
            Assert.AreEqual(15, WebApiTest.Node<int>("BookingType", 12, "Code"));
            Assert.AreEqual(25476, WebApiTest.Node<int>("BookingType", 13, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 13, "Name"));
            Assert.AreEqual(16, WebApiTest.Node<int>("BookingType", 13, "Code"));
            Assert.AreEqual(25479, WebApiTest.Node<int>("BookingType", 14, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 14, "Name"));
            Assert.AreEqual(17, WebApiTest.Node<int>("BookingType", 14, "Code"));
            Assert.AreEqual(25485, WebApiTest.Node<int>("BookingType", 15, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 15, "Name"));
            Assert.AreEqual(18, WebApiTest.Node<int>("BookingType", 15, "Code"));
            Assert.AreEqual(25486, WebApiTest.Node<int>("BookingType", 16, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 16, "Name"));
            Assert.AreEqual(19, WebApiTest.Node<int>("BookingType", 16, "Code"));
            Assert.AreEqual(25487, WebApiTest.Node<int>("BookingType", 17, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 17, "Name"));
            Assert.AreEqual(20, WebApiTest.Node<int>("BookingType", 17, "Code"));
            Assert.AreEqual(25492, WebApiTest.Node<int>("BookingType", 18, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 18, "Name"));
            Assert.AreEqual(21, WebApiTest.Node<int>("BookingType", 18, "Code"));
            Assert.AreEqual(25488, WebApiTest.Node<int>("BookingType", 19, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 19, "Name"));
            Assert.AreEqual(22, WebApiTest.Node<int>("BookingType", 19, "Code"));
            Assert.AreEqual(25489, WebApiTest.Node<int>("BookingType", 20, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 20, "Name"));
            Assert.AreEqual(23, WebApiTest.Node<int>("BookingType", 20, "Code"));
            Assert.AreEqual(25490, WebApiTest.Node<int>("BookingType", 21, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 21, "Name"));
            Assert.AreEqual(24, WebApiTest.Node<int>("BookingType", 21, "Code"));
            Assert.AreEqual(25491, WebApiTest.Node<int>("BookingType", 22, "Id"));
            Assert.AreEqual("TBA", WebApiTest.Node("BookingType", 22, "Name"));
            Assert.AreEqual(25, WebApiTest.Node<int>("BookingType", 22, "Code"));
            Assert.AreEqual(40690, WebApiTest.Node<int>("BookingPayment", 0, "Id"));
            Assert.AreEqual("Poland", WebApiTest.Node("BookingPayment", 0, "Name"));
            Assert.AreEqual(100, WebApiTest.Node<int>("BookingPayment", 0, "Code"));
            Assert.AreEqual(40691, WebApiTest.Node<int>("BookingPayment", 1, "Id"));
            Assert.AreEqual("Germany", WebApiTest.Node("BookingPayment", 1, "Name"));
            Assert.AreEqual(101, WebApiTest.Node<int>("BookingPayment", 1, "Code"));
            Assert.AreEqual(40692, WebApiTest.Node<int>("BookingPayment", 2, "Id"));
            Assert.AreEqual("Japan", WebApiTest.Node("BookingPayment", 2, "Name"));
            Assert.AreEqual(102, WebApiTest.Node<int>("BookingPayment", 2, "Code"));
            Assert.AreEqual(3136, WebApiTest.Node<int>("BookingEnquiry", 0, "Id"));
            Assert.AreEqual("Undefined", WebApiTest.Node("BookingEnquiry", 0, "Name"));
            Assert.AreEqual(0, WebApiTest.Node<int>("BookingEnquiry", 0, "Code"));
            Assert.AreEqual(1968, WebApiTest.Node<int>("BookingEnquiry", 1, "Id"));
            Assert.AreEqual("Mike Ball Brochure", WebApiTest.Node("BookingEnquiry", 1, "Name"));
            Assert.AreEqual(1, WebApiTest.Node<int>("BookingEnquiry", 1, "Code"));
            Assert.AreEqual(3040, WebApiTest.Node<int>("BookingEnquiry", 2, "Id"));
            Assert.AreEqual("Mike Ball Website", WebApiTest.Node("BookingEnquiry", 2, "Name"));
            Assert.AreEqual(2, WebApiTest.Node<int>("BookingEnquiry", 2, "Code"));
            Assert.AreEqual(3041, WebApiTest.Node<int>("BookingEnquiry", 3, "Id"));
            Assert.AreEqual("Mike Ball Direct Email", WebApiTest.Node("BookingEnquiry", 3, "Name"));
            Assert.AreEqual(3, WebApiTest.Node<int>("BookingEnquiry", 3, "Code"));
            Assert.AreEqual(1965, WebApiTest.Node<int>("BookingEnquiry", 4, "Id"));
            Assert.AreEqual("Mike Ball Direct Mail", WebApiTest.Node("BookingEnquiry", 4, "Name"));
            Assert.AreEqual(4, WebApiTest.Node<int>("BookingEnquiry", 4, "Code"));
            Assert.AreEqual(3042, WebApiTest.Node<int>("BookingEnquiry", 5, "Id"));
            Assert.AreEqual("Trade Show", WebApiTest.Node("BookingEnquiry", 5, "Name"));
            Assert.AreEqual(10, WebApiTest.Node<int>("BookingEnquiry", 5, "Code"));
            Assert.AreEqual(1961, WebApiTest.Node<int>("BookingEnquiry", 6, "Id"));
            Assert.AreEqual("Travel Agent", WebApiTest.Node("BookingEnquiry", 6, "Name"));
            Assert.AreEqual(11, WebApiTest.Node<int>("BookingEnquiry", 6, "Code"));
            Assert.AreEqual(3043, WebApiTest.Node<int>("BookingEnquiry", 7, "Id"));
            Assert.AreEqual("Dive Shop", WebApiTest.Node("BookingEnquiry", 7, "Name"));
            Assert.AreEqual(12, WebApiTest.Node<int>("BookingEnquiry", 7, "Code"));
            Assert.AreEqual(3044, WebApiTest.Node<int>("BookingEnquiry", 8, "Id"));
            Assert.AreEqual("Repeat Traveller", WebApiTest.Node("BookingEnquiry", 8, "Name"));
            Assert.AreEqual(20, WebApiTest.Node<int>("BookingEnquiry", 8, "Code"));
            Assert.AreEqual(2988, WebApiTest.Node<int>("BookingEnquiry", 9, "Id"));
            Assert.AreEqual("Recommendation", WebApiTest.Node("BookingEnquiry", 9, "Name"));
            Assert.AreEqual(21, WebApiTest.Node<int>("BookingEnquiry", 9, "Code"));
            Assert.AreEqual(3045, WebApiTest.Node<int>("BookingEnquiry", 10, "Id"));
            Assert.AreEqual("Reputation", WebApiTest.Node("BookingEnquiry", 10, "Name"));
            Assert.AreEqual(22, WebApiTest.Node<int>("BookingEnquiry", 10, "Code"));
            Assert.AreEqual(2989, WebApiTest.Node<int>("BookingEnquiry", 11, "Id"));
            Assert.AreEqual("Other Website", WebApiTest.Node("BookingEnquiry", 11, "Name"));
            Assert.AreEqual(30, WebApiTest.Node<int>("BookingEnquiry", 11, "Code"));
            Assert.AreEqual(2986, WebApiTest.Node<int>("BookingEnquiry", 12, "Id"));
            Assert.AreEqual("Sport Diver (USA)", WebApiTest.Node("BookingEnquiry", 12, "Name"));
            Assert.AreEqual(40, WebApiTest.Node<int>("BookingEnquiry", 12, "Code"));
            Assert.AreEqual(2987, WebApiTest.Node<int>("BookingEnquiry", 13, "Id"));
            Assert.AreEqual("Scuba Diving (USA)", WebApiTest.Node("BookingEnquiry", 13, "Name"));
            Assert.AreEqual(41, WebApiTest.Node<int>("BookingEnquiry", 13, "Code"));
            Assert.AreEqual(2990, WebApiTest.Node<int>("BookingEnquiry", 14, "Id"));
            Assert.AreEqual("Undersea Journal (USA)", WebApiTest.Node("BookingEnquiry", 14, "Name"));
            Assert.AreEqual(42, WebApiTest.Node<int>("BookingEnquiry", 14, "Code"));
            Assert.AreEqual(2991, WebApiTest.Node<int>("BookingEnquiry", 15, "Id"));
            Assert.AreEqual("Alert Diver (USA)", WebApiTest.Node("BookingEnquiry", 15, "Name"));
            Assert.AreEqual(44, WebApiTest.Node<int>("BookingEnquiry", 15, "Code"));
            Assert.AreEqual(2992, WebApiTest.Node<int>("BookingEnquiry", 16, "Id"));
            Assert.AreEqual("Sport Diver (AUS)", WebApiTest.Node("BookingEnquiry", 16, "Name"));
            Assert.AreEqual(50, WebApiTest.Node<int>("BookingEnquiry", 16, "Code"));
            Assert.AreEqual(2993, WebApiTest.Node<int>("BookingEnquiry", 17, "Id"));
            Assert.AreEqual("Dive Log Australasia", WebApiTest.Node("BookingEnquiry", 17, "Name"));
            Assert.AreEqual(52, WebApiTest.Node<int>("BookingEnquiry", 17, "Code"));
            Assert.AreEqual(3038, WebApiTest.Node<int>("BookingEnquiry", 18, "Id"));
            Assert.AreEqual("Dive Pacific/Dive NZ", WebApiTest.Node("BookingEnquiry", 18, "Name"));
            Assert.AreEqual(53, WebApiTest.Node<int>("BookingEnquiry", 18, "Code"));
            Assert.AreEqual(3039, WebApiTest.Node<int>("BookingEnquiry", 19, "Id"));
            Assert.AreEqual("Alert Diver (ASIA/PACIFIC)", WebApiTest.Node("BookingEnquiry", 19, "Name"));
            Assert.AreEqual(54, WebApiTest.Node<int>("BookingEnquiry", 19, "Code"));
            Assert.AreEqual(2994, WebApiTest.Node<int>("BookingEnquiry", 20, "Id"));
            Assert.AreEqual("Diver (UK)", WebApiTest.Node("BookingEnquiry", 20, "Name"));
            Assert.AreEqual(60, WebApiTest.Node<int>("BookingEnquiry", 20, "Code"));
            Assert.AreEqual(2995, WebApiTest.Node<int>("BookingEnquiry", 21, "Id"));
            Assert.AreEqual("Tauchen (GERMANY)", WebApiTest.Node("BookingEnquiry", 21, "Name"));
            Assert.AreEqual(61, WebApiTest.Node<int>("BookingEnquiry", 21, "Code"));
            Assert.AreEqual(2996, WebApiTest.Node<int>("BookingEnquiry", 22, "Id"));
            Assert.AreEqual("Unterwasser (GERMANY)", WebApiTest.Node("BookingEnquiry", 22, "Name"));
            Assert.AreEqual(62, WebApiTest.Node<int>("BookingEnquiry", 22, "Code"));
            Assert.AreEqual(3046, WebApiTest.Node<int>("BookingEnquiry", 23, "Id"));
            Assert.AreEqual("II Subacqueo (ITALY)", WebApiTest.Node("BookingEnquiry", 23, "Name"));
            Assert.AreEqual(66, WebApiTest.Node<int>("BookingEnquiry", 23, "Code"));
            Assert.AreEqual(3047, WebApiTest.Node<int>("BookingEnquiry", 24, "Id"));
            Assert.AreEqual("Mondo Sommerso (ITALY)", WebApiTest.Node("BookingEnquiry", 24, "Name"));
            Assert.AreEqual(67, WebApiTest.Node<int>("BookingEnquiry", 24, "Code"));
            Assert.AreEqual(3002, WebApiTest.Node<int>("BookingEnquiry", 25, "Id"));
            Assert.AreEqual("Sub (ITALY)", WebApiTest.Node("BookingEnquiry", 25, "Name"));
            Assert.AreEqual(68, WebApiTest.Node<int>("BookingEnquiry", 25, "Code"));
            Assert.AreEqual(3001, WebApiTest.Node<int>("BookingEnquiry", 26, "Id"));
            Assert.AreEqual("Marine Diving  (JAPAN)", WebApiTest.Node("BookingEnquiry", 26, "Name"));
            Assert.AreEqual(70, WebApiTest.Node<int>("BookingEnquiry", 26, "Code"));
            Assert.AreEqual(3000, WebApiTest.Node<int>("BookingEnquiry", 27, "Id"));
            Assert.AreEqual("Diving World (JAPAN)", WebApiTest.Node("BookingEnquiry", 27, "Name"));
            Assert.AreEqual(71, WebApiTest.Node<int>("BookingEnquiry", 27, "Code"));
            Assert.AreEqual(2999, WebApiTest.Node<int>("BookingEnquiry", 28, "Id"));
            Assert.AreEqual("Diver (JAPAN)", WebApiTest.Node("BookingEnquiry", 28, "Name"));
            Assert.AreEqual(72, WebApiTest.Node<int>("BookingEnquiry", 28, "Code"));
            Assert.AreEqual(2998, WebApiTest.Node<int>("BookingEnquiry", 29, "Id"));
            Assert.AreEqual("Asian Diver", WebApiTest.Node("BookingEnquiry", 29, "Name"));
            Assert.AreEqual(73, WebApiTest.Node<int>("BookingEnquiry", 29, "Code"));
            Assert.AreEqual(2997, WebApiTest.Node<int>("BookingEnquiry", 30, "Id"));
            Assert.AreEqual("Lonley Planet", WebApiTest.Node("BookingEnquiry", 30, "Name"));
            Assert.AreEqual(80, WebApiTest.Node<int>("BookingEnquiry", 30, "Code"));
            Assert.AreEqual(1963, WebApiTest.Node<int>("BookingEnquiry", 31, "Id"));
            Assert.AreEqual("Word Of Mouth", WebApiTest.Node("BookingEnquiry", 31, "Name"));
            Assert.AreEqual(81, WebApiTest.Node<int>("BookingEnquiry", 31, "Code"));
            Assert.AreEqual(3224, WebApiTest.Node<int>("BookingEnquiry", 32, "Id"));
            Assert.AreEqual("Walk-in Mike Ball Office", WebApiTest.Node("BookingEnquiry", 32, "Name"));
            Assert.AreEqual(89, WebApiTest.Node<int>("BookingEnquiry", 32, "Code"));
            Assert.AreEqual("R", WebApiTest.Node("BookingStatus"));
            Assert.AreEqual(21232, WebApiTest.Node<int>("BookingId"));
            Assert.AreEqual(190057, WebApiTest.Node<int>("BookingCode"));
            Assert.AreEqual("", WebApiTest.Node("ExportCode"));
            Assert.AreEqual("", WebApiTest.Node("ImportCode"));
            Assert.AreEqual(654, WebApiTest.Node<int>("BookingNumber"));
            Assert.AreEqual(456, WebApiTest.Node<int>("InsuranceCardNumber"));
            Assert.AreEqual(111, WebApiTest.Node<int>("Flex"));
            Assert.AreEqual(222, WebApiTest.Node<int>("Flex2"));
            Assert.AreEqual(333, WebApiTest.Node<int>("Flex3"));
            Assert.AreEqual("Nobs, Helena", WebApiTest.Node("BillingFolio"));
            Assert.AreEqual(true, WebApiTest.Node<bool>("CanUseWebManifest"));
            Assert.AreEqual(true, WebApiTest.Node<bool>("CanModifyBooking"));
            Assert.AreEqual(new DateTime(2200, 1, 1), WebApiTest.Node<DateTime>("TripExpirationDate"));
            Assert.AreEqual(100000000, WebApiTest.Node<double>("TripExpirationDays"));
            Assert.AreEqual("comment", WebApiTest.Node("Comments"));
            Assert.AreEqual(30, WebApiTest.Node<int>("Users", 0, "Item1"));
            Assert.AreEqual("API Agent", WebApiTest.Node("Users", 0, "Item2"));
            Assert.AreEqual(50, WebApiTest.Node<int>("Users", 1, "Item1"));
            Assert.AreEqual("Reservations", WebApiTest.Node("Users", 1, "Item2"));
            Assert.AreEqual(51, WebApiTest.Node<int>("Users", 2, "Item1"));
            Assert.AreEqual("Reservations", WebApiTest.Node("Users", 2, "Item2"));
            Assert.AreEqual(68, WebApiTest.Node<int>("Users", 3, "Item1"));
            Assert.AreEqual("ResWebConvert", WebApiTest.Node("Users", 3, "Item2"));
            Assert.AreEqual(21, WebApiTest.Node<int>("Users", 4, "Item1"));
            Assert.AreEqual("Agent, Agent", WebApiTest.Node("Users", 4, "Item2"));
            Assert.AreEqual(22, WebApiTest.Node<int>("Users", 5, "Item1"));
            Assert.AreEqual("Agent Supervisor Test, Agent Supervisor Test", WebApiTest.Node("Users", 5, "Item2"));
            Assert.AreEqual(17, WebApiTest.Node<int>("Users", 6, "Item1"));
            Assert.AreEqual("Browne, Amba", WebApiTest.Node("Users", 6, "Item2"));
            Assert.AreEqual(39, WebApiTest.Node<int>("Users", 7, "Item1"));
            Assert.AreEqual("Sarrazin, Beef", WebApiTest.Node("Users", 7, "Item2"));
            Assert.AreEqual(46, WebApiTest.Node<int>("Users", 8, "Item1"));
            Assert.AreEqual("Schmidberger, Bettina", WebApiTest.Node("Users", 8, "Item2"));
            Assert.AreEqual(47, WebApiTest.Node<int>("Users", 9, "Item1"));
            Assert.AreEqual("Jennison, Bre", WebApiTest.Node("Users", 9, "Item2"));
            Assert.AreEqual(52, WebApiTest.Node<int>("Users", 10, "Item1"));
            Assert.AreEqual("Ferreira, Carmen", WebApiTest.Node("Users", 10, "Item2"));
            Assert.AreEqual(44, WebApiTest.Node<int>("Users", 11, "Item1"));
            Assert.AreEqual("Lutrop, Claudia", WebApiTest.Node("Users", 11, "Item2"));
            Assert.AreEqual(31, WebApiTest.Node<int>("Users", 12, "Item1"));
            Assert.AreEqual("Evans, Craig", WebApiTest.Node("Users", 12, "Item2"));
            Assert.AreEqual(19, WebApiTest.Node<int>("Users", 13, "Item1"));
            Assert.AreEqual("Stephen, Craig", WebApiTest.Node("Users", 13, "Item2"));
            Assert.AreEqual(60, WebApiTest.Node<int>("Users", 14, "Item1"));
            Assert.AreEqual("Printer, Dave", WebApiTest.Node("Users", 14, "Item2"));
            Assert.AreEqual(23, WebApiTest.Node<int>("Users", 15, "Item1"));
            Assert.AreEqual("Harcarik, Deb", WebApiTest.Node("Users", 15, "Item2"));
            Assert.AreEqual(45, WebApiTest.Node<int>("Users", 16, "Item1"));
            Assert.AreEqual("Werner - Lutrop, Dirk", WebApiTest.Node("Users", 16, "Item2"));
            Assert.AreEqual(27, WebApiTest.Node<int>("Users", 17, "Item1"));
            Assert.AreEqual("Warfield, Fiona", WebApiTest.Node("Users", 17, "Item2"));
            Assert.AreEqual(43, WebApiTest.Node<int>("Users", 18, "Item1"));
            Assert.AreEqual("Stoehr, Guenter", WebApiTest.Node("Users", 18, "Item2"));
            Assert.AreEqual(76, WebApiTest.Node<int>("Users", 19, "Item1"));
            Assert.AreEqual("Lockwood, Ian", WebApiTest.Node("Users", 19, "Item2"));
            Assert.AreEqual(18, WebApiTest.Node<int>("Users", 20, "Item1"));
            Assert.AreEqual("Lucas, Janine", WebApiTest.Node("Users", 20, "Item2"));
            Assert.AreEqual(37, WebApiTest.Node<int>("Users", 21, "Item1"));
            Assert.AreEqual("Grobero, Joel", WebApiTest.Node("Users", 21, "Item2"));
            Assert.AreEqual(55, WebApiTest.Node<int>("Users", 22, "Item1"));
            Assert.AreEqual("Brown, John", WebApiTest.Node("Users", 22, "Item2"));
            Assert.AreEqual(69, WebApiTest.Node<int>("Users", 23, "Item1"));
            Assert.AreEqual("Saltarelli, Kiana", WebApiTest.Node("Users", 23, "Item2"));
            Assert.AreEqual(72, WebApiTest.Node<int>("Users", 24, "Item1"));
            Assert.AreEqual("Sledziewski, Krzysztof", WebApiTest.Node("Users", 24, "Item2"));
            Assert.AreEqual(66, WebApiTest.Node<int>("Users", 25, "Item1"));
            Assert.AreEqual("Waters, Laura", WebApiTest.Node("Users", 25, "Item2"));
            Assert.AreEqual(33, WebApiTest.Node<int>("Users", 26, "Item1"));
            Assert.AreEqual("Hancock, Leah", WebApiTest.Node("Users", 26, "Item2"));
            Assert.AreEqual(29, WebApiTest.Node<int>("Users", 27, "Item1"));
            Assert.AreEqual("Holdaway, Leah", WebApiTest.Node("Users", 27, "Item2"));
            Assert.AreEqual(28, WebApiTest.Node<int>("Users", 28, "Item1"));
            Assert.AreEqual("Cooper, Libby", WebApiTest.Node("Users", 28, "Item2"));
            Assert.AreEqual(38, WebApiTest.Node<int>("Users", 29, "Item1"));
            Assert.AreEqual("Bernstein, Louise", WebApiTest.Node("Users", 29, "Item2"));
            Assert.AreEqual(54, WebApiTest.Node<int>("Users", 30, "Item1"));
            Assert.AreEqual("RES, Luc", WebApiTest.Node("Users", 30, "Item2"));
            Assert.AreEqual(59, WebApiTest.Node<int>("Users", 31, "Item1"));
            Assert.AreEqual("Taylor, Lynn", WebApiTest.Node("Users", 31, "Item2"));
            Assert.AreEqual(62, WebApiTest.Node<int>("Users", 32, "Item1"));
            Assert.AreEqual("Kramer, Manuel", WebApiTest.Node("Users", 32, "Item2"));
            Assert.AreEqual(20, WebApiTest.Node<int>("Users", 33, "Item1"));
            Assert.AreEqual("., Mike", WebApiTest.Node("Users", 33, "Item2"));
            Assert.AreEqual(15, WebApiTest.Node<int>("Users", 34, "Item1"));
            Assert.AreEqual("Ball, Mike", WebApiTest.Node("Users", 34, "Item2"));
            Assert.AreEqual(56, WebApiTest.Node<int>("Users", 35, "Item1"));
            Assert.AreEqual("Moni, Monik", WebApiTest.Node("Users", 35, "Item2"));
            Assert.AreEqual(74, WebApiTest.Node<int>("Users", 36, "Item1"));
            Assert.AreEqual("OWASP2Name, OWASP2Surname", WebApiTest.Node("Users", 36, "Item2"));
            Assert.AreEqual(73, WebApiTest.Node<int>("Users", 37, "Item1"));
            Assert.AreEqual("OWASPSurname, OWASPName", WebApiTest.Node("Users", 37, "Item2"));
            Assert.AreEqual(58, WebApiTest.Node<int>("Users", 38, "Item1"));
            Assert.AreEqual("DeGroot, Pat", WebApiTest.Node("Users", 38, "Item2"));
            Assert.AreEqual(41, WebApiTest.Node<int>("Users", 39, "Item1"));
            Assert.AreEqual("Gower, Pip", WebApiTest.Node("Users", 39, "Item2"));
            Assert.AreEqual(53, WebApiTest.Node<int>("Users", 40, "Item1"));
            Assert.AreEqual("Letts, Rachel", WebApiTest.Node("Users", 40, "Item2"));
            Assert.AreEqual(67, WebApiTest.Node<int>("Users", 41, "Item1"));
            Assert.AreEqual("Diviac, Reservations", WebApiTest.Node("Users", 41, "Item2"));
            Assert.AreEqual(63, WebApiTest.Node<int>("Users", 42, "Item1"));
            Assert.AreEqual("Staff, Reservations", WebApiTest.Node("Users", 42, "Item2"));
            Assert.AreEqual(64, WebApiTest.Node<int>("Users", 43, "Item1"));
            Assert.AreEqual("Staff, Reservations", WebApiTest.Node("Users", 43, "Item2"));
            Assert.AreEqual(42, WebApiTest.Node<int>("Users", 44, "Item1"));
            Assert.AreEqual("Dalemans-Posor, Ruth", WebApiTest.Node("Users", 44, "Item2"));
            Assert.AreEqual(32, WebApiTest.Node<int>("Users", 45, "Item1"));
            Assert.AreEqual("Fifield, Samantha", WebApiTest.Node("Users", 45, "Item2"));
            Assert.AreEqual(24, WebApiTest.Node<int>("Users", 46, "Item1"));
            Assert.AreEqual("Voelkerling, Sharryn", WebApiTest.Node("Users", 46, "Item2"));
            Assert.AreEqual(57, WebApiTest.Node<int>("Users", 47, "Item1"));
            Assert.AreEqual("Yeo, Sophia", WebApiTest.Node("Users", 47, "Item2"));
            Assert.AreEqual(71, WebApiTest.Node<int>("Users", 48, "Item1"));
            Assert.AreEqual("Brady, Steve", WebApiTest.Node("Users", 48, "Item2"));
            Assert.AreEqual(75, WebApiTest.Node<int>("Users", 49, "Item1"));
            Assert.AreEqual("Testowysurname, Test", WebApiTest.Node("Users", 49, "Item2"));
            Assert.AreEqual(40, WebApiTest.Node<int>("Users", 50, "Item1"));
            Assert.AreEqual("Lamkijja, Vichai", WebApiTest.Node("Users", 50, "Item2"));
            Assert.AreEqual(35, WebApiTest.Node<int>("Users", 51, "Item1"));
            Assert.AreEqual("Booking, Web", WebApiTest.Node("Users", 51, "Item2"));
            Assert.AreEqual(34, WebApiTest.Node<int>("Users", 52, "Item1"));
            Assert.AreEqual("Direct, Web", WebApiTest.Node("Users", 52, "Item2"));
            Assert.AreEqual(61, WebApiTest.Node<int>("Users", 53, "Item1"));
            Assert.AreEqual("Pacofsky, Wendy", WebApiTest.Node("Users", 53, "Item2"));
            Assert.AreEqual("#B0C4DE", WebApiTest.Node("BookingStatusInquiryColorName"));
            Assert.AreEqual("#F4A460", WebApiTest.Node("BookingStatusCancelledColorName"));
            Assert.AreEqual("#ADD8E6", WebApiTest.Node("BookingStatusOptionColorName"));
            Assert.AreEqual("#32CD32", WebApiTest.Node("BookingStatusReservedColorName"));
            Assert.AreEqual("#FF4500", WebApiTest.Node("BookingStatusWaitlistColorName"));
            Assert.AreEqual(0, WebApiTest.Node<int>("BlockingLevels", 0, "Id"));
            Assert.AreEqual("", WebApiTest.Node("BlockingLevels", 0, "Name"));
            Assert.AreEqual("NonBlocking", WebApiTest.Node("BlockingLevels", 0, "Code"));
            Assert.AreEqual(-1, WebApiTest.Node<int>("BlockingLevels", 1, "Id"));
            Assert.AreEqual("", WebApiTest.Node("BlockingLevels", 1, "Name"));
            Assert.AreEqual("NotRequired", WebApiTest.Node("BlockingLevels", 1, "Code"));
            Assert.AreEqual(40696, WebApiTest.Node<int>("BookingSourceTypeId"));
            Assert.AreEqual(21404, WebApiTest.Node<int>("BookingTypeId"));
            Assert.AreEqual(40686, WebApiTest.Node<int>("BookingMarketingTypeId"));
            Assert.AreEqual(40702, WebApiTest.Node<int>("BookingOriginId"));
            Assert.AreEqual(40690, WebApiTest.Node<int>("BookingPaymentId"));
            Assert.AreEqual(20909, WebApiTest.Node<int>("BookingBillingFolioId"));
            Assert.AreEqual(0, WebApiTest.Node<int>("BookingEnquiryId"));
            Assert.AreEqual(40700, WebApiTest.Node<int>("BookingReservationId"));
            Assert.AreEqual(53, WebApiTest.Node<int>("UserId"));
            Assert.AreEqual(0, WebApiTest.Node<int>("BookInquiryBlockDays"));
            Assert.AreEqual(7, WebApiTest.Node<int>("BookOptionBlockDays"));
            Assert.AreEqual(1000, WebApiTest.Node<int>("BookReserveBlockDays"));
            Assert.AreEqual(1000, WebApiTest.Node<int>("BookConfirmBlockDays"));
            Assert.AreEqual(0, WebApiTest.Node<int>("BookWaitlistBlockDays"));

            #endregion Asserts for GetChangeBookingSetup_ReturnCorrectData

            // Ñheck count
            Assert.IsTrue(WebApiTest.IsCountCorrect(), WebApiTest.InfoCountOnFailure());

            // Pass
            Assert.Pass();
        }


        [TearDown]
        public void CleanUp()
        {
            WebApiTest.ClearAll();
        }
    }
}
