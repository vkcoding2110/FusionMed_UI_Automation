using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Interactions;

namespace UIAutomation.Utilities
{
    public static class DriverExtensions
    {
        public static void NavigateTo(this IWebDriver driver, string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch 
            {
                driver.Navigate().Refresh();
            }
        }

        public static void RefreshPage(this IWebDriver driver)
        {
            try
            {
                driver.Navigate().Refresh();
            }
            catch //If there is a WebDriverException, attempt refreshing page again.
            {
                driver.Navigate().Refresh();
            }
        }

        public static void Back(this IWebDriver driver)
        {
            driver.Navigate().Back();
        }

        public static string GetCurrentUrl(this IWebDriver driver)
        {
            return driver.Url;
        }
        public static bool IsUrlMatched(this IWebDriver driver, string url, int duration = 10)
        {
            try
            {
                new WaitHelpers(driver).WaitUntilUrlMatched(url, duration);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsUrlContains(this IWebDriver driver, string url, int duration = 10)
        {
            try
            {
                return new WaitHelpers(driver).WaitUntilUrlContains(url, duration);
            }
            catch
            {
                return false;
            }
        }

        public static string GetPageTitle(this IWebDriver driver)
        {

            var title = driver.Title.Trim();
            for (var i=0; i < 30; i++)
            {
                //Specific to marketplace - to avoid chat bot window title
                if (title.Contains("Fusion Medical Staffing says"))
                {
                    new WaitHelpers(driver).HardWait(1000);
                    title = driver.Title.Trim();
                }
                else
                {
                    break;
                }
            }
            return title;
           
        }

        public static void TakeScreenShot(this IWebDriver driver, string filepath)
        {
            if (!(driver is ITakesScreenshot ssDriver)) return;
            var screenShot = ssDriver.GetScreenshot();
            screenShot.SaveAsFile(filepath, ScreenshotImageFormat.Png);
        }

        public static void SwitchToIframe(this IWebDriver driver, IWebElement element)
        {
            driver.SwitchTo().Frame(element);
        }

        public static void SwitchToDefaultIframe(this IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }

        public static void AcceptAlert(this IWebDriver driver)
        {
            driver.SwitchTo().Alert().Accept();
        }

        public static IWebDriver SelectWindowByTitle(this IWebDriver driver, string title)
        {
            foreach (var item in driver.WindowHandles.Where(item => driver.SwitchTo().Window(item).Title.Equals(title)))
            {
                driver.SwitchTo().Window(item);
                new WaitHelpers(driver).HardWait(1000);
                break;
            }

            return driver;
        }

        public static void CloseWindowByTitle(this IWebDriver driver, string title)
        {
            foreach (var item in driver.WindowHandles.Where(item => driver.SwitchTo().Window(item).Title.Contains(title)))
            {
                driver.SwitchTo().Window(item);
                new WaitHelpers(driver).HardWait(1000);
                driver.Close();
                break;
            }
        }

        public static void SwitchToLastWindow(this IWebDriver driver)
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        public static void SwitchToFirstWindow(this IWebDriver driver)
        {
            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        public static void SelectWindowByIndex(this IWebDriver driver, int index)
        {
            var windows = driver.WindowHandles;
            driver.SwitchTo().Window(windows[index]);
        }
        public static void CloseWindowByIndex(this IWebDriver driver, int index)
        {
            var windows = driver.WindowHandles;
            driver.SwitchTo().Window(windows[index]);
            driver.Close();
        }
        public static void JavaScriptClickOn(this IWebDriver driver, By by)
        {
            var element = new WaitHelpers(driver).UntilElementExists(by);
            driver.JsExecutor().ExecuteScript("arguments[0].click();", element);
        }

        public static void JavaScriptClickOn(this IWebDriver driver, IWebElement element)
        {
            driver.JsExecutor().ExecuteScript("arguments[0].click();", element);
        }

        public static void MoveToElement(this IWebDriver driver, IWebElement element)
        {
           new Actions(driver).MoveToElement(element).Build().Perform();
        }

        public static void JavaScriptSetValue(this IWebDriver driver, IWebElement element, string value)
        {
            var script = $"arguments[0].value='{value}';";
            driver.JsExecutor().ExecuteScript(script, element);
        }

        public static IJavaScriptExecutor JsExecutor(this IWebDriver driver)
        {
            return driver as IJavaScriptExecutor;
        }

        public static IWebElement JavaScriptScrollToElement(this IWebDriver driver, IWebElement element, bool top = true)
        {
            driver.JsExecutor().ExecuteScript($"arguments[0].scrollIntoView({top.ToString().ToLower()});", element);

            return element;
        }

        public static void JavaScriptScroll(this IWebDriver driver, string horizontal, string vertical)
        {
            var script = "scroll(" + horizontal + "," + vertical + ")";
            driver.JsExecutor().ExecuteScript(script);
        }
    }
}
