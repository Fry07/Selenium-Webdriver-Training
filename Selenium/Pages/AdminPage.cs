using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Pages
{
    public class AdminPage
    {
        public IWebDriver driver;

        public AdminPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "#box-login > form > div.content > div:nth-child(2) > div > input")]
        public IWebElement login { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#box-login > form > div.content > div:nth-child(3) > div > input")]
        public IWebElement password { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#box-login > form > div.footer > button")]
        public IWebElement loginButton { get; set; }

        [FindsBy(How = How.LinkText, Using = "Add New Product")]
        public IWebElement addNewProductButton { get; set; }

        [FindsBy(How = How.LinkText, Using = "General")]
        public IWebElement tabGeneral { get; set; }

        [FindsBy(How = How.LinkText, Using = "Information")]
        public IWebElement tabInformation { get; set; }

        [FindsBy(How = How.LinkText, Using = "Prices")]
        public IWebElement tabPrices { get; set; }

        [FindsBy(How = How.LinkText, Using = "Stock")]
        public IWebElement tabStock { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#tab-general > div > div:nth-child(2) > div:nth-child(1) > div > input")]
        public IWebElement productName { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#tab-information > div:nth-child(1) > div > input")]
        public IWebElement productShortDescription { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/main/form/div/div[2]/div[2]/div/div/textarea")]
        public IWebElement productDescription { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#prices > table > tbody > tr:nth-child(1) > td:nth-child(1) > div > input")]
        public IWebElement priceUSD { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#main > form > p > button:nth-child(1)")]
        public IWebElement saveButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#main > form > p > button:nth-child(3)")]
        public IWebElement deleteButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#images > div.new-images > div > div.input-group > input")]
        public IWebElement imagePath { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#tab-general > div > div:nth-child(1) > div:nth-child(1) > div > div > label:nth-child(1)")]
        public IWebElement statusEnabled { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#table-stock > tbody > tr > td:nth-child(5) > input")]
        public IWebElement stockQty { get; set; }



        public IWebElement GetProductInGrid(string name)
        {
            return driver.FindElement(By.LinkText(name));
        }

        public void Login(string login, string password)
        {
            this.login.SendKeys(login);
            this.password.SendKeys(password);
            loginButton.Click();
        }

        public void AddNewProduct(string name)
        {
            OpenMenuByName("Catalog");
            addNewProductButton.Click();
            tabGeneral.Click();
            productName.SendKeys(name);
            imagePath.SendKeys(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Files\\duck_hunt.jpg")));
            statusEnabled.Click();
            tabInformation.Click();
            productShortDescription.SendKeys(Faker.Company.CatchPhrase());
            productDescription.SendKeys(Faker.Company.CatchPhrase());
            tabPrices.Click();
            priceUSD.SendKeys(Faker.RandomNumber.Next(10000).ToString());
            tabStock.Click();
            stockQty.SendKeys(Faker.RandomNumber.Next(1000).ToString());
            saveButton.Click();
        }

        public void DeleteProduct(string name)
        {
            OpenMenuByName("Catalog");
            GetProductInGrid(name).Click();
            deleteButton.Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        public bool ClickOnMenus()
        {
            var menuContainer = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists((By.CssSelector("#box-apps-menu"))));
            IList<IWebElement> menuLinks = menuContainer.FindElements(By.CssSelector("#app- > a"));

            for (int i = 0; i < menuLinks.Count; i++)
            {
                menuContainer = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists((By.CssSelector("#box-apps-menu"))));
                menuLinks = menuContainer.FindElements(By.CssSelector("#app- > a"));
                IWebElement menuItemLink = driver.FindElement(By.LinkText(menuLinks[i].Text));
                menuItemLink.Click();
                var selectedMenu = driver.FindElement(By.ClassName("selected"));
                IList<IWebElement> innerMenu = selectedMenu.FindElements(By.TagName("li"));

                for (int j = 0; j < innerMenu.Count; j++)
                {
                    selectedMenu = driver.FindElement(By.ClassName("docs"));
                    innerMenu = selectedMenu.FindElements(By.TagName("li"));
                    IWebElement innerMenuItemLink = driver.FindElement(By.LinkText(innerMenu[j].Text));
                    innerMenuItemLink.Click();

                    if (!IsElementPresent(By.TagName("h1"))) return false;
                }
                if (!IsElementPresent(By.TagName("h1"))) return false;
            }
            return true;
        }

        public void OpenMenuByName(string menuName)
        {
            var menuContainer = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists((By.CssSelector("#box-apps-menu"))));
            IList<IWebElement> menuLinks = menuContainer.FindElements(By.CssSelector("#app- > a"));
            IWebElement menuItemLink = driver.FindElement(By.LinkText(menuName));
            menuItemLink.Click();
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
