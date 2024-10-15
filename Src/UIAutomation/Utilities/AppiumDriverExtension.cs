using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;

namespace UIAutomation.Utilities
{
    public static class AppiumDriverExtension
    {
        /**
      * Scroll the page based on screen percentage.
      * @param posX is horizontal point, should be in 0 -1 range.
      * @param startY is starting point to be set, should be in 0 -1 range.
     * @param endY end point to be set, should be in 0 -1 range
      * @param duration set scroll duration in miliseconds
      * Example : Scroll down > scroll(0.5, 0.80, 0.20, 2000)
      */
        private static readonly By BackSpaceButtonDevice = By.XPath("//XCUIElementTypeKey[@name='delete']");
        private static readonly By SelectAllButtonDevice = By.XPath("//XCUIElementTypeMenuItem[@name='Select All']");

        public static void Scroll(this IWebDriver driver, double posX, double startY, double endY, int duration)
        {
            var androidDriver = (AndroidDriver<AndroidElement>)driver;
            var size = androidDriver.Manage().Window.Size;

            //x position set to mid-screen horizontally
            var width = (int)(size.Width * posX);

            //Starting y location set to 80% of the height (near bottom)
            var startPoint = (int)(size.Height * startY);
            //Ending y location set to 20% of the height (near top)
            var endPoint = (int)(size.Height * endY);

            var touchAction = new TouchAction(androidDriver);
            touchAction.Press(width, startPoint).Wait(duration).MoveTo(width, endPoint).Release().Perform();
        }

        public static void ScrollToElementByText(this IWebDriver driver, string elementText)
        {
            new WaitHelpers(driver).HardWait(2000);
            var locatorText = "new UiScrollable(new UiSelector().scrollable(true)).scrollIntoView(new UiSelector().text(\"" + elementText + "\"));";
            new WaitHelpers(driver).UntilElementExists(MobileBy.AndroidUIAutomator(locatorText));
        }

        public static string GetNativeAppWebViewUrl(this IWebDriver driver)
        {
            var androidDriver = (AndroidDriver<AndroidElement>)driver;
            new WaitHelpers(driver).HardWait(3000);
            androidDriver.Context = "WEBVIEW_chrome";
            var url = androidDriver.GetCurrentUrl();
            androidDriver.Context = "NATIVE_APP";
            return url;
        }
        public static void NativeAppEnterText(this IWebDriver driver, By element, string jobDescription)
        {
            var iOsDriver = (IOSDriver<IOSElement>)driver;
            new WaitHelpers(driver).HardWait(2000);
            iOsDriver.Context = "NATIVE_APP";
            new WaitHelpers(driver).UntilElementVisible(element).EnterText(jobDescription);
            iOsDriver.Context = iOsDriver.Contexts.ToList().First(x => x.Contains("WEB"));
        }

        public static void NativeAppClickOn(this IWebDriver driver, By element)
        {
            var androidDriver = (AndroidDriver<AndroidElement>)driver;
            new WaitHelpers(driver).HardWait(1000);
            androidDriver.Context = "NATIVE_APP";
            new WaitHelpers(driver).UntilElementVisible(element).ClickOn();
            androidDriver.Context = "CHROMIUM";   
        }

        public static void NativeAppClearText(this IWebDriver driver, By element)
        {
            var iOsDriver = (IOSDriver<IOSElement>)driver;
            var waitHelper = new WaitHelpers(driver);
            waitHelper.HardWait(2000);
            iOsDriver.Context = "NATIVE_APP";
                string text;
                do
                {
                    var actions = new Actions(driver);
                    waitHelper.UntilElementClickable(element).Click();
                    actions.MoveToElement(waitHelper.UntilElementClickable(element)).DoubleClick().Perform();
                    if (waitHelper.IsElementPresent(SelectAllButtonDevice))
                    {
                        waitHelper.UntilElementClickable(SelectAllButtonDevice).ClickOn();
                    }  
                    waitHelper.UntilElementClickable(BackSpaceButtonDevice).ClickOn();
                    text = waitHelper.UntilElementVisible(element).GetAttribute("value");
                } while (text != null);
            iOsDriver.Context = iOsDriver.Contexts.ToList().First(x => x.Contains("WEB"));
        }
    }
}
