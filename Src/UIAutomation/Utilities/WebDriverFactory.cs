using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using UIAutomation.DataObjects.Device;
using UIAutomation.Enum;
using UIAutomation.Tests;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace UIAutomation.Utilities
{
    public class WebDriverFactory
    {
        private IWebDriver Driver;

        public IWebDriver InitDriver(AppiumLocalService appiumLocalService, Capabilities capabilities)
        {
            var platformName = capabilities.PlatformName.ToEnum<PlatformName>();
            var browserName = capabilities.Browser.ToEnum<BrowserName>();
            var appiumOptions = new AppiumOptions();
            var env = BaseTest.Env;

            switch (platformName)
            {
                case PlatformName.Web:
                    switch (browserName)
                    {
                        case BrowserName.FireFox:
                            var firefoxOptions = new FirefoxOptions();
                            firefoxOptions.SetPreference("browser.download.dir", new FileUtil().GetDownloadPath());
                            firefoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                            new DriverManager().SetUpDriver(new FirefoxConfig());
                            Driver = new FirefoxDriver(firefoxOptions);
                            break;

                        case BrowserName.Chrome:
                            var options = new ChromeOptions();
                            var experimentalFlags = new List<string> { "calculate-native-win-occlusion@2" };
                            options.AddLocalStatePreference("browser.enabled_labs_experiments", experimentalFlags);
                            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                            Driver = new ChromeDriver(options);
                            break;

                        case BrowserName.Safari:
                            Driver = new SafariDriver();
                            break;
                    }
                    Driver.Manage().Window.Maximize();
                    break;

                case PlatformName.Android:
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, capabilities.DeviceName);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, capabilities.PlatformName);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, capabilities.PlatformVersion);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, capabilities.Browser);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, capabilities.AutomationName);
                    appiumOptions.AddAdditionalCapability("unicodeKeyboard", "true");
                    appiumOptions.AddAdditionalCapability("resetKeyboard", "true");
                    appiumOptions.AddAdditionalCapability("appWaitForLaunch", "false");
                    Driver = new AndroidDriver<AndroidElement>(appiumLocalService, appiumOptions);
                    break;

                case PlatformName.AndroidApp:
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, capabilities.DeviceName);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, capabilities.PlatformName);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, capabilities.PlatformVersion);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, capabilities.AutomationName);
                    appiumOptions.AddAdditionalCapability("unicodeKeyboard", "true");
                    appiumOptions.AddAdditionalCapability("resetKeyboard", "true");
                    appiumOptions.AddAdditionalCapability("appWaitForLaunch", "false");
                    appiumOptions.AddAdditionalCapability("fullReset", "false");
                    appiumOptions.AddAdditionalCapability("app", new FileUtil().GetBasePath() + "/TestData/FMP/APKs/FMP_" + env + ".apk");
                    appiumOptions.AddAdditionalCapability("newCommandTimeout", 60);
                    Driver = new AndroidDriver<AndroidElement>(appiumLocalService, appiumOptions, TimeSpan.FromMinutes(2));
                    break;

                case PlatformName.IosApp:
                    break;

                case PlatformName.Ios:
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, capabilities.DeviceName);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, capabilities.PlatformName);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, capabilities.PlatformVersion);
                    appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, capabilities.Browser);
                    Driver = new IOSDriver<IOSElement>(appiumLocalService, appiumOptions);
                    break;
            }

            if (!BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.None))
            {
                Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
                Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(180);
            }
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return Driver;
        }

        public void CloseDriver()
        {
            Driver.Quit();
        }
    }
}
