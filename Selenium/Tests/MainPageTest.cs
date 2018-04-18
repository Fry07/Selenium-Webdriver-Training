﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium
{
    [TestFixture, Description("Create simple test which just open browser, navigates to litecart and then close the browser. Try to run different browsers.")]
    public class MainPageTest
    {
        public IWebDriver driver { get; set; }
        private String expectedTitle = "My Store | Online Store";
            
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test, Category("MainPage")]
        public void ChromeOpen()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(Properties.Settings.Default.MainPageURL);

            String actualTitle = driver.Title;
            Assert.AreEqual(expectedTitle, actualTitle);
        }

        [Test, Category("MainPage")]
        public void FirefoxOpen()
        {
            driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(Properties.Settings.Default.MainPageURL);

            String actualTitle = driver.Title;
            Assert.AreEqual(expectedTitle, actualTitle);
        }

        [Test, Category("MainPage")]
        public void ExplorerOpen()
        {
            driver = new InternetExplorerDriver();
            driver.Navigate().GoToUrl(Properties.Settings.Default.MainPageURL);

            String actualTitle = driver.Title;
            Assert.AreEqual(expectedTitle, actualTitle);
        }
    }
}