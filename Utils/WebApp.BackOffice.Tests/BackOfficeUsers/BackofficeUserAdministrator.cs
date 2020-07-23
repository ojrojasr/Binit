using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace WebApp.BackOffice.Tests.BackOfficeUsers
{
    class BackofficeUserAdministrator : BaseTest
    {
        [Test]
        public void RegisterUser()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");

            Thread.Sleep(1000);

            // encuentra el menu de settings
            var menuButton = driver.FindElement(By.XPath("//ul[@class='sidebarnav bottom']"));
            menuButton.Click();

            HelperTest.clickAhref(driver, "/BackOfficeUser");
            Thread.Sleep(1000);

            var usuario = new UsersBackOffice()
            {
                Nombre = "Backoffice",
                Apellido = "Administrator",
                Mail = "backofficeadmin@binit.com.ar",
                Telefono = "1166394901",
                Rol = "Backoffice.BackofficeUserAdministrator"
            };
            usuario.Crear(driver);
        }

    }
}
