using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;
using System.Threading;


namespace WebApp.BackOffice.Tests.FrontUsers
{
    public class UsersFront
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Fnaciemiento { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public string Rol { get; set; }

        public void Crear(IWebDriver driver)
        {
            // Botón nuevo usuario
            var newUserButton = driver.FindElement(By.CssSelector("button[type='button']"));
            newUserButton.Click();
            Thread.Sleep(1000);

            HelperTest.RegisterUserFront(driver, this.Nombre, this.Apellido, this.Fnaciemiento, this.Mail, this.Telefono, this.Rol);
            // select de longitud de tabla
            var tablelength = driver.FindElement(By.XPath("//option[@value='-1']"));
            tablelength.Click();
            Thread.Sleep(1000);

            var table = driver.FindElement(By.XPath("//table[@data-get-url='/FrontUser/GetAll']"));
            var body = table.FindElement(By.TagName("tbody"));

            var data = body.FindElements(By.TagName("tr")).Select(x => new
            {
                Nombre = x.FindElements(By.TagName("td"))[0].Text,
                Apellido = x.FindElements(By.TagName("td"))[1].Text
            }).ToList();

            Assert.IsTrue(data.Any(x => x.Nombre == this.Nombre && x.Apellido == this.Apellido));

        }
    }
}
