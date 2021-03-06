﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Selenium.Pages;
using System.IO;
using OpenQA.Selenium.Support.Events;

namespace Selenium
{
    [TestFixture]
    public class AdminTest
    {
        public IWebDriver driver { get; set; }
        public WebDriverWait wait { get; set; }
        public static string logPath;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();            
            logPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Logs\\")) + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".txt";

            EventFiringWebDriver firingDriver = new EventFiringWebDriver(driver);
            firingDriver.ExceptionThrown += new EventHandler<WebDriverExceptionEventArgs>(WebDriverEvents.firingDriver_ExceptionThrown);
            firingDriver.ElementClicked += new EventHandler<WebElementEventArgs>(WebDriverEvents.firingDriver_ElementClicked);
            firingDriver.FindElementCompleted += new EventHandler<FindElementEventArgs>(WebDriverEvents.firingDriver_FindElementCompleted);
            driver = firingDriver;
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null) driver.Quit();
        }

        [Test, Category("AdminPage"), Description("Login to admin panel. Sequentially click on each item in side menu (left) including all submenu items. For each new page verify if header is present on the page (element h1).")]
        public void OpenAllSubmenus()
        {
            AdminPage adminPage = new AdminPage(driver);
            adminPage.OpenAdminPage();
            adminPage.Login(Properties.Settings.Default.AdminLogin, Properties.Settings.Default.AdminPassword);
            Assert.IsTrue(adminPage.ClickOnMenus(), "Header wasn't found");
        }

        [Test, Description("Add and edit products to cart including new product that was added from admin page.")]
        public void EditCartWithNewProduct()
        {
            var name = Faker.Company.Name();            
            AdminPage adminPage = new AdminPage(driver);
            MainPage mainPage = new MainPage(driver);

            adminPage.OpenAdminPage();
            adminPage.Login(Properties.Settings.Default.AdminLogin, Properties.Settings.Default.AdminPassword);
            adminPage.AddNewProduct(name);
            Assert.IsTrue(adminPage.IsElementPresent(By.LinkText(name)), "New product wasn't found in the main grid");                    
            
            mainPage.OpenMainPage();
            mainPage.SearchAndOpenProduct(name);
            mainPage.AddProductToTheCart("1");
            Assert.AreEqual(1, mainPage.GetCartQuantity(), "Wrong product count in the cart after adding first product");
            mainPage.OpenBlueDuckPage();
            mainPage.AddProductToTheCart("2");
            Assert.AreEqual(2, mainPage.GetCartQuantity(), "Wrong product count in the cart after adding second product");

            mainPage.RemoveTopProductsFromCart(2);
            Assert.AreEqual("There are no items in your cart.", mainPage.GetEmptyCartMessage(), "Wrong text of empty cart message");

            mainPage.OpenMainPage();
            Assert.AreEqual(0, mainPage.GetCartQuantity(), "Wrong product count in the cart after deleting all products");

            adminPage.OpenAdminPage();
            adminPage.DeleteProduct(name);
            Assert.IsFalse(adminPage.IsElementPresent(By.LinkText(name)), "Deleted product was found in the main grid");
        }
    }
}
