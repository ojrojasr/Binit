﻿using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace WebApp.BackOffice.Tests.FrontUsers
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

            HelperTest.clickAhref(driver, "/FrontUser");
            Thread.Sleep(1000);

            var usuario = new UsersFront()
            {
                Nombre = "Product",
                Apellido = "Administrator",
                Fnaciemiento = "30/10/2019",
                Mail = "frontProductadmin@binit.com.ar",
                Telefono = "1166394901",
                Rol = "Front.ProductAdministrator"
            };
            usuario.Crear(driver);
        }
    }
}
