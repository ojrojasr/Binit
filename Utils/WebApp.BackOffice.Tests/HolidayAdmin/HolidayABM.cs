using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace WebApp.BackOffice.Tests.HolidayAdmin
{
    [TestFixture]
    public class HolidayABM : BaseTest
    {
        [Test]
        public void TheHolidayCreateTest()
        {
            driver.Navigate().GoToUrl(baseURL);
            HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");
            Thread.Sleep(2000);

            var menuButton = driver.FindElement(By.XPath("//ul[@class='sidebarnav bottom']"));
            menuButton.Click();
            Thread.Sleep(1000);

            HelperTest.clickAhref(driver, "/Holiday");
            Thread.Sleep(1000);

            // Botón nuevo Holiday
            var newUserButton = driver.FindElement(By.CssSelector("button[type='button']"));
            newUserButton.Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("Name")).Click();
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("Feriado");
            driver.FindElement(By.Id("Description")).Click();
            driver.FindElement(By.Id("Description")).Clear();
            driver.FindElement(By.Id("Description")).SendKeys("Feriado para las fiestas");
            driver.FindElement(By.Id("Message")).Click();
            driver.FindElement(By.Id("Message")).Clear();
            driver.FindElement(By.Id("Message")).SendKeys("Feriado para las fiestas");

            driver.FindElement(By.XPath("//option[@value='B47C3169-BFBA-C9F1-4F37-39F0D85B42B4']")).Click();
            driver.FindElement(By.Id("UsersIds"));

            driver.FindElement(By.CssSelector("button.btn[type='submit']")).Click();
            Thread.Sleep(1000);

            HelperTest.TableCreate(driver, "/Holiday", "Feriado", "Feriado para las fiestas");
            Thread.Sleep(1000);

            //    [Test]
            //    public void TheHolidayModifyTest()
            //    {
            //        driver.Navigate().GoToUrl(baseURL);
            //        HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");
            //        Thread.Sleep(1000);

            //        var menuButton = driver.FindElement(By.XPath("//ul[@class='sidebarnav bottom']"));
            //        menuButton.Click();
            //        Thread.Sleep(1000);

            //        Encontrando el registro existente para modificarlo
            //        HelperTest.TableCreate(driver, "/Holiday", "Feriado", "Feriado para las fiestas");
            //        Thread.Sleep(1000);

            //        var editRegistrer = driver.FindElement(By.XPath("//button[@class='btn waves-effect waves-light btn-primary ml-2 btn-sm edit-action datatable-action-button']"));
            //        editRegistrer.Click();
            //        Thread.Sleep(1000);

            //        driver.FindElement(By.Id("Name")).Click();
            //        driver.FindElement(By.Id("Name")).Clear();
            //        driver.FindElement(By.Id("Name")).SendKeys("Feriado -feriado");
            //        driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Cancelar'])[1]/following::button[1]")).Click();
            //        driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Acciones'])[1]/following::div[1]")).Click();

            //        Assert para la modificación del resgistro
            //        HelperTest.TableEdit(driver, "/Holiday", "Feriado -feriado");
            //        Thread.Sleep(1000);
            //    }

            //    [Test]
            //    public void TheHolidayTypeQuitTest()
            //    {
            //        driver.Navigate().GoToUrl(baseURL);

            //        HelperTest.Login(driver, "administrador@binit.com.ar", "qweQWE123!#");
            //        Thread.Sleep(1000);

            //        var menuButton = driver.FindElement(By.XPath("//ul[@class='sidebarnav bottom']"));
            //        menuButton.Click();

            //        HelperTest.clickAhref(driver, "/Holiday");
            //        Thread.Sleep(1000);

            //        HelperTest.TableEdit(driver, "/Holiday", "Feriado - feriado");
            //        Thread.Sleep(1000);

            //        var deleteRegistrer = driver.FindElement(By.XPath("//button[@class='btn waves-effect waves-light btn-danger ml-2 btn-sm delete-action datatable-action-button']"));
            //        deleteRegistrer.Click();
            //        Thread.Sleep(1000);

            //        var deleteConfirmation = driver.FindElement(By.XPath("//button[@class='swal2-confirm swal2-styled']"));
            //        deleteConfirmation.Click();
            //        Thread.Sleep(1000);
            //    }
            //}
        }
    }
}