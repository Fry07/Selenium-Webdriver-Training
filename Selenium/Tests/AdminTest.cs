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
            adminPage.ClickOnMenus();
        }
    }
}
