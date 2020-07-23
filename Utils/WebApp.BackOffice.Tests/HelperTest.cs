using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;

namespace WebApp.BackOffice.Tests
{
    class HelperTest
    {
        public static void Login(IWebDriver driver, string user, string pass)
        {
            var form = driver.FindElement(By.Id("account"));

            var box_email = form.FindElement(By.Id("Input_Email"));
            box_email.Click();
            box_email.SendKeys(user);

            var box_password = form.FindElement(By.Id("Input_Password"));
            box_password.Click();
            box_password.SendKeys(pass);

            var login_button = form.FindElement(By.CssSelector("button.btn[type='submit']"));
            login_button.Click();
        }
        public static void Register(IWebDriver driver, string name, string lastName, string email, string phoneNumber, string rol)
        {
            var createUserBoxName = driver.FindElement(By.Id("Name"));
            createUserBoxName.Click();
            createUserBoxName.SendKeys(name);

            var createUserBoxLastName = driver.FindElement(By.Id("LastName"));
            createUserBoxLastName.Click();
            createUserBoxLastName.SendKeys(lastName);

            var createUserBoxEmail = driver.FindElement(By.Id("Email"));
            createUserBoxEmail.Click();
            createUserBoxEmail.SendKeys(email);

            var createUserBoxPhone = driver.FindElement(By.Id("PhoneNumber"));
            createUserBoxPhone.Click();
            createUserBoxPhone.SendKeys(phoneNumber);

            var formCreateSelectRol = driver.FindElement(By.XPath("//option[@value='" + rol + "']"));
            formCreateSelectRol.Click();

            var formCreateSelectTenant = driver.FindElement(By.XPath("//option[@value='11645D89-8D3B-4108-B54D-816C9745B931']"));
            formCreateSelectTenant.Click();

            var create_button = driver.FindElement(By.CssSelector("button.btn[type='submit']"));
            create_button.Click();
        }
        public static void RegisterUserFront(IWebDriver driver, string name, string lastName, string Fnacimiento, string email, string phoneNumber, string rol)
        {
            var createUserBoxName = driver.FindElement(By.Id("Name"));
            createUserBoxName.Click();
            createUserBoxName.SendKeys(name);

            var createUserBoxLastName = driver.FindElement(By.Id("LastName"));
            createUserBoxLastName.Click();
            createUserBoxLastName.SendKeys(lastName);

            var FechaNacimiento = driver.FindElement(By.Id("Birthdate"));
            FechaNacimiento.Click();
            FechaNacimiento.SendKeys(Fnacimiento);

            var createUserBoxEmail = driver.FindElement(By.Id("Email"));
            createUserBoxEmail.Click();
            createUserBoxEmail.SendKeys(email);

            var createUserBoxPhone = driver.FindElement(By.Id("PhoneNumber"));
            createUserBoxPhone.Click();
            createUserBoxPhone.SendKeys(phoneNumber);

            var formCreateSelectRol = driver.FindElement(By.XPath("//option[@value='" + rol + "']"));
            formCreateSelectRol.Click();

            var formCreateSelectTenant = driver.FindElement(By.XPath("//option[@value='11645D89-8D3B-4108-B54D-816C9745B931']"));
            formCreateSelectTenant.Click();

            var create_button = driver.FindElement(By.CssSelector("button.btn[type='submit']"));
            create_button.Click();
        }
        public static void clickAhref(IWebDriver driver, string link)
        {
            var path = "//a[@href='" + link + "']";
            var element = driver.FindElement(By.XPath(path));
            element.Click();
        }
        public static void clickAhrefront(IWebDriver driver, string link)
        {
            var path = "//a[@href='" + link + "']";
            var element = driver.FindElement(By.XPath(path));
            element.Click();
        }
        public static void TableCreate(IWebDriver driver, string linkTable, string name, string description)
        {
            var table_ = driver.FindElement(By.XPath("//table[@data-get-url='" + linkTable + "/GetAll']"));
            var body_ = table_.FindElement(By.TagName("tbody"));

            var data_ = body_.FindElements(By.TagName("tr")).Select(x => new
            {
                Name = x.FindElements(By.TagName("td"))[0].Text,
                Description = x.FindElements(By.TagName("td"))[1].Text
            }).ToList();

            Assert.IsTrue(data_.Any(x => x.Name == name && x.Description == description));
        }
        public static void TableEdit(IWebDriver driver, string linkTablEdit, string namEdit)
        {
            var tablEdit = driver.FindElement(By.XPath("//table[@data-get-url='" + linkTablEdit + "/GetAll']"));
            var bodyEdit = tablEdit.FindElement(By.TagName("tbody"));
            var dataEdit = bodyEdit.FindElements(By.TagName("tr")).Select(x => new
            {
                NamEdit = x.FindElements(By.TagName("td"))[0].Text,
            }).ToList();
            Assert.IsTrue(dataEdit.Any(x => x.NamEdit == namEdit));
        }
    }
}
