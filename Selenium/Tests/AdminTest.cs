using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selenium.Pages;
using System.IO;

namespace Selenium
{
    [TestFixture]
    public class AdminTest
    {
        public IWebDriver driver { get; set; }
        public WebDriverWait wait { get; set; }

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test, Category("AdminPage"), Description("Login to admin panel. Sequentially click on each item in side menu (left) including all submenu items. For each new page verify if header is present on the page (element h1).")]
        public void OpenAllSubmenus()
        {
            driver.Navigate().GoToUrl(Properties.Settings.Default.AdminPageURL);
            AdminPage adminPage = new AdminPage(driver);
            adminPage.Login(Properties.Settings.Default.AdminLogin, Properties.Settings.Default.AdminPassword);
            Assert.IsTrue(adminPage.ClickOnMenus());
        }

        [Test, Description("Add and edit products to cart including new product that was added from admin page.")]
        public void EditCartWithNewProduct()
        {
            var name = Faker.Company.Name();
            driver.Navigate().GoToUrl(Properties.Settings.Default.AdminPageURL);
            AdminPage adminPage = new AdminPage(driver);
            adminPage.Login(Properties.Settings.Default.AdminLogin, Properties.Settings.Default.AdminPassword);
            adminPage.AddNewProduct(name);
            Assert.IsTrue(adminPage.IsElementPresent(By.LinkText(name)));

            driver.Navigate().GoToUrl(Properties.Settings.Default.MainPageURL);
            MainPage mainPage = new MainPage(driver);
            mainPage.SearchAndOpenProduct(name);
            mainPage.addToCart.Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(mainPage.cartQty, "1"));
            driver.Navigate().GoToUrl(Properties.Settings.Default.BlueDuckURL);
            mainPage.addToCart.Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(mainPage.cartQty, "2"));

            mainPage.RemoveTopProductsFromCart(2);
            Assert.AreEqual("There are no items in your cart.", mainPage.emptyCartMsg.Text);

            driver.Navigate().GoToUrl(Properties.Settings.Default.MainPageURL);
            Assert.AreEqual(0, Convert.ToInt32(mainPage.cartQty.Text));

            driver.Navigate().GoToUrl(Properties.Settings.Default.AdminPageURL);
            adminPage.DeleteProduct(name);
            Assert.IsFalse(adminPage.IsElementPresent(By.LinkText(name)));
        }
    }
}
