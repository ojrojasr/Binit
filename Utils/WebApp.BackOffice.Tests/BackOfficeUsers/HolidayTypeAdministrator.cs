using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace WebApp.BackOffice.Tests.BackOfficeUsers
{
    class HolidayTypeAdministrator : BaseTest
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
                Nombre = "Holiday Type",
                Apellido = "Administrator",
                Mail = "holidaytypeadmin@binit.com.ar",
                Telefono = "1166394901",
                Rol = "Backoffice.HolidayTypeAdministrator"
            };
            usuario.Crear(driver);
        }
    }
}
