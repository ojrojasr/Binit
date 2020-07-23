using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace WebApp.BackOffice.Tests.BackOfficeUsers
{
    class ProductAdministrator : BaseTest
    {
        [Test]
        public void RegisterUser()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");

            Thread.Sleep(2000);

            // encuentra el menu de settings
            var menuButton = driver.FindElement(By.XPath("//ul[@class='sidebarnav bottom']"));
            menuButton.Click();

            HelperTest.clickAhref(driver, "/BackOfficeUser");
            Thread.Sleep(1000);

            var usuario = new UsersBackOffice()
            {
                Nombre = "Product",
                Apellido = "Administrator",
                Mail = "productadmin@binit.com.ar",
                Telefono = "1166394901",
                Rol = "Backoffice.ProductAdministrator"
            };
            usuario.Crear(driver);
        }
    }
}
