using Crawler.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Providers.FacebookCrawler
{
    public class FacebookCrawlerAgent : SocialTrackingAgent
    {
        private FacebookCrawlerConfiguration _configuration
        {
            get
            {
                if (this.Configuration is FacebookCrawlerConfiguration) return (FacebookCrawlerConfiguration)this.Configuration;
                return null;
            }
        }

        private IWebDriver _driver;

        public FacebookCrawlerAgent(ISocialAgentConfiguration configuration) : base(configuration, SocialPlatform.Facebook)
        {

        }

        public override DataOutputBase RetrieveData(DataInputBase input)
        {
            if (!(input is FacebookCrawlerDataInput)) return null;
            var facebookInput = input as FacebookCrawlerDataInput;
            if (_driver == null)
            {
                var browserDirectory = ConfigurationManager.AppSettings["Selenium.ChromeDirectory"];
                _driver = new ChromeDriver(browserDirectory);
            }
            _driver.Url = facebookInput.PageUrl;
            _driver.Manage().Window.Maximize();
            IWebElement emailTextBox = _driver.FindElement(By.XPath(_configuration.EmailXPathQuery));
            emailTextBox.SendKeys(_configuration.Username);
            IWebElement passwordTextBox = _driver.FindElement(By.XPath(_configuration.PasswordXPathQuery));
            passwordTextBox.SendKeys(_configuration.Password);
            IWebElement loginBtn = _driver.FindElement(By.XPath(_configuration.LoginButtonXPathQuery));
            loginBtn.Click();
            return null;
        }
    }
}
