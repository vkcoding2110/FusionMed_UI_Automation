using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace UIAutomation.Utilities
{
    internal class WaitHelpers
    {
        private readonly IWebDriver Driver;
        private readonly WebDriverWait Wait;

        public WaitHelpers(IWebDriver driver, int timeOut = 45)
        {
            this.Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
        }

        /// <summary>
        ///  Waits until an element can be found with the supplied locator. The element is not necessarily displayed.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>The located element</returns>
        public IWebElement UntilElementExists(By locator)
        {
            var element = Wait.Until((webDriver) => webDriver.FindElement(locator));

            return element;
        }

        /// <summary>
        /// Waits until the element can be located with the supplied locator, and that the element is displayed and enabled.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="duration"></param>
        /// <returns>The located element</returns>
        public IWebElement UntilElementClickable(By locator, int duration = 15)
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(duration));
            var element = localWait.Until(ExpectedConditions.ElementToBeClickable(locator));
            return element;
        }

        /// <summary>
        /// Waits until the element visible.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="duration"></param>
        /// <returns>The located element</returns>
        public IWebElement UntilElementVisible(By locator, int duration = 15)
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(duration));
            var element = localWait.Until(ExpectedConditions.ElementIsVisible(locator));
            return element;
        }

        public void WaitUntilTextRefreshed(By locator, int duration = 15)
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(duration));
            try
            {
                var i = 1;
                IWebElement element;
                do
                {
                    HardWait(1000);
                    element = localWait.Until(ExpectedConditions.ElementIsVisible(locator));
                    i++;
                    if (!i.Equals(5)) continue;
                    break;
                } while (element.GetText().Length == 0);
            }
            catch
            {
                //Do nothing
            }
        }

        /// <summary>
        /// Waits until the element invisible.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="duration"></param>
        public void UntilElementInVisible(By locator, int duration = 15)
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(duration));
            localWait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }


        /// <summary>
        /// Locate all the elements that match the locator and return them in a list
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="duration"></param>
        /// <returns>IList of elements that were located</returns>
        public IList<IWebElement> UntilAllElementsLocated(By locator, int duration = 15)
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(duration));
            IList<IWebElement> elements = localWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            return elements;
        }

        /// <summary>
        /// This waits for a specific amount of time. This should be used only if necessary.
        /// </summary>
        /// <param name="milliseconds"></param>
        public void HardWait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public void WaitUntilUrlMatched(string url, int duration = 10)
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(duration));

            localWait.Until(ExpectedConditions.UrlToBe(url));
        }
        public bool WaitUntilUrlContains(string url, int duration = 10)
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(duration));

            return localWait.Until(ExpectedConditions.UrlContains(url));
        }
        public IWebElement WaitIncaseElementClickable(By locator, int duration = 15)
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(duration));

            try
            {
                return localWait.Until(ExpectedConditions.ElementToBeClickable(locator));
            }
            catch
            {
                return null;
            }
        }

        public IWebElement WaitIncaseElementExists(By locator, int duration = 15)
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(duration));

            try
            {
                return localWait.Until(ExpectedConditions.ElementExists(locator));
            }
            catch
            {
                return null;
            }
        }

        public IWebElement WaitIncaseElementClickable(IWebElement e, int duration = 15)
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(duration));

            try
            {
                return localWait.Until(ExpectedConditions.ElementToBeClickable(e));
            }
            catch
            {
                return null;
            }
        }

        public void WaitTillElementCountIsLessThan(By by, int elementCount)
        {
            int count;
            do
            {
                try
                {
                    count = Driver.FindElements(by).Count;
                    HardWait(1000);
                }
                catch
                {
                    count = 0;
                }
            }
            while (count > elementCount);
        }

        public bool IsElementPresent(By locator, int duration = 15, bool forceWait = false)
        {
            if (!forceWait) return WaitIncaseElementExists(locator, duration)?.Displayed ?? false;

            var staleElementCount = 1;
            do
            {
                try
                {
                    return UntilElementExists(locator)?.Displayed ?? false;
                }
                catch (Exception)
                {
                    staleElementCount++;
                    Thread.Sleep(1000);
                }

                if (staleElementCount != 5) continue;
                break;
            } while (true);
            return false;
        }

        public bool IsElementDisplayed(By locator, int duration = 15)
        {
            return UntilAllElementsLocated(locator, duration).Any(e => e.Displayed);
        }


        public bool IsElementEnabled(By locator, int duration = 15)
        {
            return WaitIncaseElementExists(locator, duration)?.Enabled ?? false;
        }

    }
}
