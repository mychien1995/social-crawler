using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crawler.Providers
{
    public static class WebDriverExtensions
    {
        public static void ScrollToBottom(this IWebDriver driver, long sleepTime = 500)
        {
            long scrollHeight = 0;

            do
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");

                if (newScrollHeight == scrollHeight)
                {
                    break;
                }
                else
                {
                    scrollHeight = newScrollHeight;
                    Thread.Sleep((int)sleepTime);
                }
            } while (true);
        }
    }
}
