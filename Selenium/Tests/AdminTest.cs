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
            adminPage.OpenMenuByName("Catalog");
            adminPage.addNewProductButton.Click();
            adminPage.tabGeneral.Click();
            adminPage.productName.SendKeys(name);
            //adminPage.imagePath.SendKeys("asd");
            adminPage.statusEnabled.Click();
            adminPage.tabInformation.Click();
            adminPage.productShortDescription.SendKeys(Faker.Company.CatchPhrase());
            //adminPage.productDescription.SendKeys(Faker.Company.CatchPhrase());
            adminPage.tabPrices.Click();
            adminPage.priceUSD.SendKeys(Faker.RandomNumber.Next(10000).ToString());
            adminPage.tabStock.Click();
            adminPage.stockQty.SendKeys(Faker.RandomNumber.Next(1000).ToString());
            adminPage.saveButton.Click();

            driver.Navigate().GoToUrl(Properties.Settings.Default.MainPageURL);
            MainPage mainPage = new MainPage(driver);
            mainPage.search.SendKeys(name);
            mainPage.search.SendKeys(Keys.Enter);
            mainPage.addToCart.Click();
            Thread.Sleep(600);
            driver.Navigate().GoToUrl(Properties.Settings.Default.BlueDuckURL);
            mainPage.addToCart.Click();
            Thread.Sleep(600);
            Assert.AreEqual(2, Convert.ToInt32(mainPage.cartQty.Text));
            mainPage.cart.Click();
            Thread.Sleep(1500);
            mainPage.removeButton.Click();
            Thread.Sleep(1500);
            mainPage.removeButton.Click();
            Thread.Sleep(1500);

            driver.Navigate().GoToUrl(Properties.Settings.Default.MainPageURL);
            Assert.AreEqual(0, Convert.ToInt32(mainPage.cartQty.Text));
            driver.Navigate().GoToUrl(Properties.Settings.Default.AdminPageURL);
            adminPage.OpenMenuByName("Catalog");
            adminPage.GetProductInGrid(name).Click();
            adminPage.deleteButton.Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }
    }
}
