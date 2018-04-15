using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;

namespace Selenium
{
    class MainPage
    {
        private String url = "http://159.89.100.130/litecart/en/";
        private String expectedTitle = "My Store | Online Store";

        [Test]
        public void chromeOpen()
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);

            String actualTitle = driver.Title;
            Assert.AreEqual(expectedTitle, actualTitle);

            driver.Quit();
        }

        [Test]
        public void firefoxOpen()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl(url);

            String actualTitle = driver.Title;
            Assert.AreEqual(expectedTitle, actualTitle);

            driver.Quit();
        }

        [Test]
        public void explorerOpen()
        {
            IWebDriver driver = new InternetExplorerDriver();
            driver.Navigate().GoToUrl(url);

            String actualTitle = driver.Title;
            Assert.AreEqual(expectedTitle, actualTitle);

            driver.Quit();
        }
    }
}
