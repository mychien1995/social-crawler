using Crawler.Core;
using Crawler.Providers.Crawlers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
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
        private FacebookCrawlerConfiguration Configuration;

        private IWebDriver _driver;

        private ICrawlerConfigurationHelper _crawlerConfigurationHelper;

        public FacebookCrawlerAgent(FacebookCrawlerConfiguration configuration) : base(SocialPlatform.Facebook)
        {
            Configuration = configuration;
            _crawlerConfigurationHelper = new CrawlerConfigurationHelper();
        }

        public override DataOutputBase RetrieveData(DataInputBase input)
        {
            if (!(input is FacebookCrawlerDataInput)) return null;
            this.IsActive = true;
            var facebookInput = input as FacebookCrawlerDataInput;
            if (_driver == null)
            {
                var browserDirectory = ConfigurationManager.AppSettings["Selenium.ChromeDirectory"];
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--disable-notifications");
                _driver = new ChromeDriver(browserDirectory, options);
                DoLogin(facebookInput);
            }
            _driver.Url = facebookInput.PageUrl;
            var htmlProperties = _crawlerConfigurationHelper.LoadHtmlProperties(Configuration);
            var htmlPropertiesDict = new Dictionary<string, object>();
            foreach (var htmlProperty in htmlProperties)
            {
                htmlPropertiesDict.Add(htmlProperty.Property, htmlProperty.GetValue(_driver, input));
            }
            this.IsActive = false;
            var output = new FacebookCrawlerDataOutput();
            output.PropertyBag = htmlPropertiesDict;
            return output;
        }

        private void DoLogin(FacebookCrawlerDataInput facebookInput)
        {
            _driver.Url = Configuration.LoginUrl;
            _driver.Manage().Window.Maximize();
            IWebElement emailTextBox = _driver.FindElement(By.XPath(Configuration.EmailXPathQuery));
            emailTextBox.SendKeys(Configuration.Username);
            IWebElement passwordTextBox = _driver.FindElement(By.XPath(Configuration.PasswordXPathQuery));
            passwordTextBox.SendKeys(Configuration.Password);
            IWebElement loginBtn = _driver.FindElement(By.XPath(Configuration.LoginButtonXPathQuery));
            loginBtn.Click();
        }
    }
}
