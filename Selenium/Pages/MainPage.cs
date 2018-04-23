using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium.Pages
{
    public class MainPage
    {
        public IWebDriver driver;
        public WebDriverWait wait { get; set; }

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "#site-menu > nav > div.navbar-header > form > div > input")]
        public IWebElement search { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#box-product > div.row > div:nth-child(3) > div.buy_now > form > div > div > div:nth-child(2) > button")]
        public IWebElement addToCart { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#cart > a > div > span.quantity")]
        public IWebElement cartQty { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#cart")]
        public IWebElement cart { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#box-checkout-cart > div > table > tbody > tr:nth-child(1) > td:nth-child(6) > button")]
        public IWebElement removeButton { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/main/form/div/div[1]/p[1]/em")]
        public IWebElement emptyCartMsg { get; set; }

        public void RemoveTopProductsFromCart(int productsCount)
        {
            cart.Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#box-checkout-customer > h2"))); //Customer Details header
            for (int i = 0; i < productsCount; i++)
            {
                IWebElement shoppingCartHeader = driver.FindElement(By.CssSelector("#box-checkout-cart > h2"));
                removeButton.Click();
                wait.Until(ExpectedConditions.StalenessOf(shoppingCartHeader));
            }
        }

        public void SearchAndOpenProduct(string productName)
        {
            search.SendKeys(productName);
            search.SendKeys(Keys.Enter);
        }

        public void AddProductToTheCart(string expectedQuantity)
        {
            addToCart.Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(cartQty, expectedQuantity));
        }

        public void OpenMainPage()
        {
            driver.Navigate().GoToUrl(Properties.Settings.Default.MainPageURL);
        }

        public void OpenBlueDuckPage()
        {
            driver.Navigate().GoToUrl(Properties.Settings.Default.BlueDuckURL);
        }

        public int GetCartQuantity()
        {
            return Convert.ToInt32(cartQty.Text);
        }

        public string GetEmptyCartMessage()
        {
            return emptyCartMsg.Text;
        }
    }
}
