using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace WebApp.BackOffice.Tests.HolidayTypeAdmin
{
    [TestFixture]
    public class HolidayTypeABM : BaseTest
    {
        [Test]
        public void TheHolidayTypeCreateTest()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");
            Thread.Sleep(2000);

            var menuButton = driver.FindElement(By.XPath("//ul[@class='sidebarnav bottom']"));
            menuButton.Click();
            Thread.Sleep(1000);

            HelperTest.clickAhref(driver, "/HolidayType");
            Thread.Sleep(1000);

            // Botón nuevo HolidayType
            var newUserButton = driver.FindElement(By.CssSelector("button[type='button']"));
            newUserButton.Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("Name")).Click();
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("Feriado");
            driver.FindElement(By.Id("Description")).Click();
            driver.FindElement(By.Id("Description")).Clear();
            driver.FindElement(By.Id("Description")).SendKeys("Feriado, feriado, feriado, feriado");
            driver.FindElement(By.CssSelector("button.btn[type='submit']")).Click();
            Thread.Sleep(1000);


            HelperTest.TableCreate(driver, "/HolidayType", "Feriado", "Feriado, feriado, feriado, feriado");
            Thread.Sleep(1000);
        }

        //[Test]
        //public void TheHolidayTypeModifyTest()
        //{
        //    driver.Navigate().GoToUrl(baseURL);
        //    HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");
        //    Thread.Sleep(1000);

        //    var menuButton = driver.FindElement(By.XPath("//ul[@class='sidebarnav bottom']"));
        //    menuButton.Click();
        //    Thread.Sleep(1000);

        //    HelperTest.clickAhref(driver, "/HolidayType");
        //    Thread.Sleep(1000);

        //    Encontrando el registro existente para modificarlo
        //    HelperTest.TableCreate(driver, "/HolidayType", "Feriado", "Feriado, feriado, feriado, feriado");
        //    Thread.Sleep(1000);

        //    var editRegistrer = driver.FindElement(By.XPath("//button[@class='btn waves-effect waves-light btn-primary ml-2 btn-sm edit-action datatable-action-button']"));
        //    editRegistrer.Click();
        //    Thread.Sleep(1000);

        //    driver.FindElement(By.Id("Description")).Click();
        //    driver.FindElement(By.Id("Description")).Clear();
        //    driver.FindElement(By.Id("Description")).SendKeys("Feriado feriadoooooooooooooooooooooooo");
        //    driver.FindElement(By.CssSelector("button.btn[type='submit']")).Click();
        //    Thread.Sleep(1000);

        //    Assert para la modificación del resgistro
        //    HelperTest.TableEdit(driver, "/HolidayType", "Feriado");
        //    Thread.Sleep(1000);
        //}

        //[Test]
        //public void TheHolidayTypeQuitTest()
        //{
        //    driver.Navigate().GoToUrl(baseURL);

        //    HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");
        //    Thread.Sleep(1000);

        //    var menuButton = driver.FindElement(By.XPath("//ul[@class='sidebarnav bottom']"));
        //    menuButton.Click();

        //    HelperTest.clickAhref(driver, "/HolidayType");
        //    Thread.Sleep(1000);

        //    HelperTest.TableEdit(driver, "/HolidayType", "Feriado");
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
