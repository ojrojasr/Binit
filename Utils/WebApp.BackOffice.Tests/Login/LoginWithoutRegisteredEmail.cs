using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace WebApp.BackOffice.Tests.Login
{
    public class LoginWithoutRegisteredEmail : BaseTest
    {
        [Test]
        public void EmailDoesntExist_Validation()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "uvalce@viakaadi.a.lofteone.ru", "qweQWE123!#");

            Thread.Sleep(1000);
            Assert.IsTrue(IsElementPresent(By.CssSelector(".text-danger")));

        }

        [Test]
        public void LoginEmail_Validation()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "uvalce@viak02.a.lofteone.ru", "qweQWE123!#");

            Thread.Sleep(1000);
            Assert.IsTrue(IsElementPresent(By.CssSelector(".text-danger")));

        }

        [Test]
        public void LoginPassword_Validation()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "uvalce@viakaadia6202.a.lofteone.ru", "qweQWE123");

            Thread.Sleep(1000);
            Assert.IsTrue(IsElementPresent(By.CssSelector(".text-danger")));

        }

        [Test]
        public void EmailAndPassword_Validation()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "uvalce@viak.a.lofteone.ru", "qweQWE123");

            Thread.Sleep(1000);
            Assert.IsTrue(IsElementPresent(By.CssSelector(".text-danger")));

        }

        [Test]
        public void EmptyForm_Validation()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "", "");

            Thread.Sleep(1000);
            Assert.IsTrue(IsElementPresent(By.Id("Input_Email-error")));
            Assert.IsTrue(IsElementPresent(By.Id("Input_Password-error")));

        }

        [Test]
        public void EmailFormatValidation()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "adminrbinit", "qweQWE123");

            Thread.Sleep(1000);
            Assert.IsTrue(IsElementPresent(By.Id("Input_Email-error")));

        }
    }
}
