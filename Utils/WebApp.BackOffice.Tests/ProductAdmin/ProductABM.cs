using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace WebApp.BackOffice.Tests.ProductAdmin
{
    [TestFixture]
    public class ProductABM : BaseTest
    {
        [Test]
        public void TheProductCreateTest()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");
            Thread.Sleep(2000);

            var menuButton = driver.FindElement(By.XPath("//ul[@class='sidebarnav bottom']"));
            menuButton.Click();
            Thread.Sleep(1000);

            HelperTest.clickAhref(driver, "/Product");
            Thread.Sleep(1000);

            // boton nuevo Producto
            var newUserButton = driver.FindElement(By.CssSelector("button[type='button']"));
            newUserButton.Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("Name")).Click();
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("Periféricos");
            driver.FindElement(By.Id("Description")).Click();
            driver.FindElement(By.Id("Description")).Clear();
            driver.FindElement(By.Id("Description")).SendKeys("Periféricos y más periféricos");
            driver.FindElement(By.Id("Price")).Click();
            driver.FindElement(By.Id("Price")).Clear();
            driver.FindElement(By.Id("Price")).SendKeys("13000");
            driver.FindElement(By.Id("ReleaseDate")).Click();
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Sa'])[1]/following::td[6]")).Click();
            driver.FindElement(By.Id("select2-CategoryId-container")).Click();
            driver.FindElement(By.CssSelector("button.btn[type='submit']")).Click();
            Thread.Sleep(1000);

            HelperTest.TableCreate(driver, "/Product", "Periféricos", "Periféricos y más periféricos");
            Thread.Sleep(1000);
        }

        //[Test]
        //public void TheProductModifyTest()
        //{
        //    driver.Navigate().GoToUrl(baseURL);
        //    HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");
        //    Thread.Sleep(1000);

        //    var menuButton = driver.FindElement(By.XPath("//ul[@class='sidebarnav bottom']"));
        //    menuButton.Click();
        //    Thread.Sleep(1000);

        //    HelperTest.clickAhref(driver, "/Product");
        //    Thread.Sleep(1000);

        //    // Encontrando el registro existente para modificarlo
        //    HelperTest.TableCreate(driver, "/Product", "Periféricos", "Periféricos y más periféricos");
        //    Thread.Sleep(1000);

        //    var editRegistrer = driver.FindElement(By.XPath("//button[@class='btn waves-effect waves-light btn-primary ml-2 btn-sm edit-action datatable-action-button']"));
        //    editRegistrer.Click();
        //    Thread.Sleep(1000);

        //    driver.FindElement(By.Id("Description")).Click();
        //    driver.FindElement(By.Id("Description")).Clear();
        //    driver.FindElement(By.Id("Description")).SendKeys("Periféricos y más periféricos y más perifericos");
        //    driver.FindElement(By.Id("select2-CategoryId-container")).Click();

        //    driver.FindElement(By.CssSelector("button.btn[type='submit']")).Click();
        //    Thread.Sleep(1000);

        //    // Assert para la modificación del resgistro
        //    HelperTest.TableEdit(driver, "/Product", "Periféricos");
        //    Thread.Sleep(1000);
        //}

        //[Test]
        //public void TheProductQuitTest()
        //{
        //    driver.Navigate().GoToUrl(baseURL);

        //    HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");
        //    Thread.Sleep(1000);

        //    var menuButton = driver.FindElement(By.XPath("//ul[@class='sidebarnav bottom']"));
        //    menuButton.Click();

        //    HelperTest.clickAhref(driver, "/Product");
        //    Thread.Sleep(1000);

        //    HelperTest.TableEdit(driver, "/Product", "Periféricos");
        //    Thread.Sleep(1000);

        //    var deleteRegistrer = driver.FindElement(By.XPath("//button[@class='btn waves-effect waves-light btn-danger ml-2 btn-sm delete-action datatable-action-button']"));
        //    deleteRegistrer.Click();
        //    Thread.Sleep(1000);

        //    var deleteConfirmation = driver.FindElement(By.XPath("//button[@class='swal2-confirm swal2-styled']"));
        //    deleteConfirmation.Click();
        //    Thread.Sleep(1000);
        //}
    }
}
