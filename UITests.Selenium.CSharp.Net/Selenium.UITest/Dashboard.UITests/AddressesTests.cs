using Microsoft.VisualStudio.TestTools.UnitTesting;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;
using TestContext = NUnit.Framework.TestContext;
using Shared.Driver;
using Dashboard.UITests.Pages;
using NUnit.Framework;
using Dashboard.UITests.Enum;
using OpenQA.Selenium;
using System;

namespace Dashboard.UITests
{
    [TestFixture("Test")]
    [TestFixture("Stage")]
    internal class AddressesTests
    {
        public IWebDriver driver;
        public string url;
        public readonly string useEnvironment;
        public string browser;

        /// <summary>
        /// From TestFixture
        /// </summary>
        /// /// <param name="useEnvironment"></param>
        public AddressesTests(string useEnvironment)
        {
            this.useEnvironment = useEnvironment;
        }

        [SetUp]
        public void Setup()
        {
            browser = TestContext.Parameters["browser"];
            if (browser == "Firefox")
            {
                browser = "Firefox";
            }
            else 
            {
                browser = "Chrome";  // Default to Chrome
            }
        }

        [TestMethod]
        [Description("TestCaseId:C517")]
        public void RegisterAddressTests_Pos(string currency)
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);                                          // Set environment URL
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);                      // Log in as valid user

                // Steps
                Navigation.NavigateToAddresses(driver, url);                             // Navigate to 'Addresses'
                AddressesPage.RemoveAnyAddresses(driver, true);                          // Remove existed addresses
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.ValidValues);   // Fill in form with valid values
                AddressesPage.SelectCurrency(driver, currency);                          // Select currencies
                AddressesPage.RegisterAddress(driver);                                   // Register new address

                // Assert
                Assertions.CheckNewAddressIsAt(driver);                                  // Check new address created

                // CleanUp
                AddressesPage.RemoveAnyAddresses(driver, true);                          // Clean Up
            }
        }


        [Test]
        public void Addresses_USDCG_Valid()
        {
            RegisterAddressTests_Pos("USDCG");
        }

                   
        [Test]
        public void Addresses_sNGNG_Valid()
        {
            RegisterAddressTests_Pos("sNGNG");
        }


        [Test]
        public void Addresses_NGNG_sUSDCG_Valid()
        {
            RegisterAddressTests_Pos("NGNG/sUSDCG");
        }


        [Test]
        public void Addresses_USDCG_NGNG_sUSDCG_sNGNG_Valid()
        {
            RegisterAddressTests_Pos("USDCG/NGNG/sUSDCG/sNGNG");     
        }


        [Test]
        [Description("TestCaseId:C528")]
        public void Addresses_ValidAddressNoPrefix_Pos()
        {           
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToAddresses(driver, url);
                AddressesPage.RemoveAnyAddresses(driver, true);
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.ValidAddressNoPrefix);   // Valid address without prefix
                AddressesPage.SelectCurrency(driver, "sUSDCG");
                AddressesPage.RegisterAddress(driver);

                // Assert
                Assertions.CheckAddressNoPrefix(driver);

                // CleanUp
                AddressesPage.RemoveAnyAddresses(driver, true);
            }
        }


        [Test]
        [Description("TestCaseId:C524")]
        public void InvalidSignatureTest_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToAddresses(driver, url);
                AddressesPage.RemoveAnyAddresses(driver, true);
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.InvalidSign);   // Invalid signature
                AddressesPage.SelectCurrency(driver, "sUSDCG");
                AddressesPage.RegisterAddress(driver);

                // Assert
                Assertions.ConfirmValidationError(driver);
            }
        }


        [Test]
        [Description("TestCaseId:C516")]
        public void InvalidAddressTest_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToAddresses(driver, url);
                AddressesPage.RemoveAnyAddresses(driver, true);
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.InvalidAddress);   // Invalid address
                AddressesPage.SelectCurrency(driver, "USDCG");
                AddressesPage.RegisterAddress(driver);

                // Assert
                Assertions.ConfirmValidationError(driver);
            }
        }


        [Test]
        [Description("TestCaseId:C525")]
        public void InvalidMessageTest_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToAddresses(driver, url);
                AddressesPage.RemoveAnyAddresses(driver, true);
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.InvalidMsg);    // Invalid message
                AddressesPage.SelectCurrency(driver, "sUSDCG");
                AddressesPage.RegisterAddress(driver);

                // Assert
                Assertions.ConfirmValidationError(driver);
            }
        }


        [Test]
        public void Addresses_DeleteAddress_Exit()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToAddresses(driver, url);
                AddressesPage.RemoveAnyAddresses(driver, true);
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.ValidValues);
                AddressesPage.SelectCurrency(driver, "sUSDCG");
                AddressesPage.RegisterAddress(driver);
                AddressesPage.RemoveAnyAddresses(driver, true);

                // Assert
                Assertions.CheckAddressDeleted(driver);
            }
        }


        [Test]
        [Description("TestCaseId:C528")]
        public void Addresses_NoCurrency_Neg()
        {
            using (var init = new TestScope(browser, useEnvironment))
            {
                // Setup
                driver = init.Instance;
                url = init.Env;

                // Arrange
                LoginPage.GoToUrl(driver, url);
                LoginPage.LoginAs(driver, LoginUserEnum.ValidUser);

                // Steps
                Navigation.NavigateToAddresses(driver, url);
                AddressesPage.RemoveAnyAddresses(driver, true);
                AddressesPage.AddressesForm(driver, url, AddressFormEnum.ValidAddressNoPrefix); 
                AddressesPage.RegisterAddress(driver);

                // Assert
                Assertions.ConfirmCurrencyNotSelected(driver);
            }
        }


        /// <summary>
        /// Initialization
        /// Set environment
        /// TearDown
        /// </summary>
        private sealed class TestScope : IDisposable
        {
            public IWebDriver Instance { get; }
            public string Env { get; }

            // SetUp
            public TestScope(string browser, string useEnvironment)
            {
                Driver initialize = new Driver();
                Instance = initialize.StartBrowser(browser);
                Shared setup = new Shared();
                Env = setup.SetEnvironmentVariables(useEnvironment);
            }

            // TearDown
            public void Dispose()
            {
                if (Instance != null)
                {
                    Instance.Quit();
                }
            }
        }
    }
}
