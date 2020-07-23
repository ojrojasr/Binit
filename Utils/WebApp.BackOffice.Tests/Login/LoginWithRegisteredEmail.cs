using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace WebApp.BackOffice.Tests.Login
{
    public class LoginWithRegisteredEmail : BaseTest
    {
        [Test]
        public void SuccessfulLogin()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");

            Thread.Sleep(1000);
            Assert.IsTrue(IsElementPresent(By.Id("fort-test-id")));

        }

        [Test]
        public void EmailLogin_Validation()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "administrador@binit.com", "qweQWE123!#");

            Thread.Sleep(1000);
            Assert.IsTrue(IsElementPresent(By.CssSelector(".text-danger")));

        }

        [Test]
        public void LoginPassword_Validation()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "administrador@binit.com", "qweQWE123");

            Thread.Sleep(1000);
            Assert.IsTrue(IsElementPresent(By.CssSelector(".text-danger")));

        }

        [Test]
        public void EmailAndPassword_Validation()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "adminr@binit.com", "qweQWE123");

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
        public void EmailFormat_Validation()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "administradorbinit.com", "qweQWE123");

            Thread.Sleep(1000);
            Assert.IsTrue(IsElementPresent(By.Id("Input_Email-error")));

        }
    }
}

