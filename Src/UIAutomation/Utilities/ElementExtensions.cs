using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using UIAutomation.Enum;
using UIAutomation.Tests;

namespace UIAutomation.Utilities
{
    internal static class ElementExtensions
    {
        public static void EnterText(this IWebElement element, string text, bool forceClear = false)
        {
            var staleElementCount = 1;
            bool staleElement;
            do
            {
                try
                {
                    if (forceClear)
                    {
                        if (BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari))
                        {
                            var inputText = element.GetAttribute("value");
                            if (inputText != null)
                            {
                                for (var i = 0; i < inputText.Length; i++)
                                {
                                    element.SendKeys(Keys.Backspace);
                                }
                            }
                        }
                        else
                        {
                            element.SendKeys(Keys.Control + "a");
                            element.SendKeys(Keys.Delete);
                            element.SendKeys(Keys.Control + "a");
                            element.SendKeys(Keys.Delete);
                        }
                    }
                    else
                    {
                        element.Clear();
                    }
                    element.SendKeys(text);
                    staleElement = false;
                }
                catch (StaleElementReferenceException)
                {
                    staleElementCount++;
                    staleElement = true;
                    Thread.Sleep(1000);
                }

                if (!staleElementCount.Equals(5)) continue;
                break;
            } while (staleElement);
        }
        public static void PressEscKey(this IWebElement element)
        {
            var staleElementCount = 1;
            bool staleElement;
            do
            {
                try
                {
                    element.SendKeys(Keys.Escape);
                    staleElement = false;
                }
                catch (StaleElementReferenceException)
                {
                    staleElementCount++;
                    staleElement = true;
                    Thread.Sleep(1000);
                }
                if (!staleElementCount.Equals(5)) continue;
                break;
            } while (staleElement);
        }
        public static string GetText(this IWebElement element)
        {
            return element.Text.Trim();
        }

        public static bool IsDisplayed(this IWebElement element)
        {
            bool result;
            try
            {
                result = element.Displayed;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static void ClickOn(this IWebElement element)
        {
            var i = 1;
            bool staleElement;
            do
            {
                try
                {
                    element.Click();
                    staleElement = false;
                }
                catch (StaleElementReferenceException)
                {
                    i++;
                    staleElement = true;
                    Thread.Sleep(1000);
                }

                if (!i.Equals(10)) continue;
                break;

            } while (staleElement);
        }

        public static void SelectDropdownValueByText(this IWebElement element, string text, IWebDriver driver = null)
        {
            if (BaseTest.PlatformName == PlatformName.Ios)
            {
                element.Click();
                new WaitHelpers(driver).UntilAllElementsLocated(By.XPath($"//*[text()='{text}']")).First(e => e.IsDisplayed()).ClickOn();
            }
            else
            {
                var oSelect = new SelectElement(element);
                oSelect.SelectByText(text);
            }
        }

        public static void SelectDropdownValueByIndex(this IWebElement element, int index)
        {
            var oSelect = new SelectElement(element);
            oSelect.SelectByIndex(index);
        }

        public static void SelectDropdownValueByValue(this IWebElement element, string text)
        {
            var oSelect = new SelectElement(element);
            oSelect.SelectByValue(text);
        }

        public static string SelectDropdownGetSelectedValue(this IWebElement element)
        {
            var oSelect = new SelectElement(element);
            return oSelect.SelectedOption.Text.Trim();
        }

        public static void Check(this IWebElement element, bool check = true, IWebDriver driver = null)
        {
            if (check && !element.Selected || !check && element.Selected)
            {
                if (BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Safari) && driver != null)
                {
                    driver.JavaScriptClickOn(element);
                }
                else
                {
                    element.Click();
                }
            }
        }

        public static bool IsElementSelected(this IWebElement element)
        {
            return element.Selected;
        }

        public static bool IsElementAttributePresent(this IWebElement element, string attributeName)
        {
            return element.GetAttribute(attributeName).Length != 0;
        }

        public static bool IsElementAttributeValueMatched(this IWebElement element, string attributeName, string expectedValue)
        {
            return element.GetAttribute(attributeName).Equals(expectedValue);
        }
    }
}
