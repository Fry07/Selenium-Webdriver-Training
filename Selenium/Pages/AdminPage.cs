using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
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

        public void Login(string login, string password)
        {
            this.login.SendKeys(login);
            this.password.SendKeys(password);
            loginButton.Click();
        }

        public void ClickOnMenus()
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

                    Assert.IsTrue(IsElementPresent(By.TagName("h1")));
                }
                Assert.IsTrue(IsElementPresent(By.TagName("h1")));
            }
        }

        private bool IsElementPresent(By by)
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
