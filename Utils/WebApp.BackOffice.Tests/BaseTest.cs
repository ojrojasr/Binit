using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Text;

namespace WebApp.BackOffice.Tests
{
    [TestFixture]
    public class BaseTest
    {
        public static IWebDriver driver;
        public static StringBuilder verificationErrors;
        public static string baseURL;
        public static string baseURLFront;
        public static bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            verificationErrors = new StringBuilder();
            baseURL = TestContext.Parameters["BaseUrl"];
            baseURLFront = TestContext.Parameters["BaseUrlFront"];

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();

        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        public string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

    }
}
