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
    public class MainPage
    {
        public IWebDriver driver;

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
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
    }
}
