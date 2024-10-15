using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.DataObjects.Device;
using UIAutomation.Enum;
using UIAutomation.Utilities;

namespace UIAutomation.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected IWebDriver Driver;
        protected WebDriverFactory DriverFactory = new WebDriverFactory();
        private readonly FileUtil FileUtil = new FileUtil();
        public static Capabilities Capability = new Capabilities();
        public static AppiumLocalService AppiumLocalService;

        //Core
        protected static string CoreUrl;
        protected static Login LoginCredentials = new Login();
        protected static Login LoginCredentialsForAudit = new Login();
        protected static Login LoginCredentialsForSpecialist = new Login();
        protected static Login LoginCredentialsForPayroll = new Login();
        protected static Login LoginCredentialsForAccountReceivable = new Login();

        //Time sheet
        protected static string TimeSheetUrl;
        protected static Login TimeSheetLoginCredentials = new Login();

        //FMS
        public static string FmsUrl;

        //FMP
        public static string FusionMarketPlaceUrl;
        public static string GreenkartUrl;
        protected static Login FusionMarketPlaceLoginCredentials = new Login();


        protected Logger Log { get; set; }

        public TestContext TestContext { get; set; }


        public static string Env { get; set; }
        public static PlatformName PlatformName { get; set; }

        [AssemblyInitialize]
        public static void Init(TestContext testContext)
        {
            //Browser
            Capability.Browser = testContext.Properties["browser"]?.ToString();
            Env = testContext.Properties["env"]?.ToString();
            Capability.PlatformName = testContext.Properties["PlatformName"]?.ToString();
            PlatformName = Capability.PlatformName.ToEnum<PlatformName>();

            if (PlatformName != PlatformName.Web)
            {
                var args = new OptionCollector()
                 .AddArguments(GeneralOptionList.PreLaunch())
                 .AddArguments(new KeyValuePair<string, string>("--relaxed-security", string.Empty))
                 .AddArguments(new KeyValuePair<string, string>("--allow-insecure", string.Empty))
                 .AddArguments(new KeyValuePair<string, string>("chromedriver_autodownload", string.Empty));
                AppiumLocalService = new AppiumServiceBuilder().WithArguments(args).UsingAnyFreePort().Build();
            }

            //Core
            CoreUrl = $"https://core-{Env}.fusionmedstaff.com";
            LoginCredentials.Email = testContext.Properties["email"]?.ToString();
            LoginCredentials.Password = testContext.Properties["password"]?.ToString();
            LoginCredentials.Name = testContext.Properties["name"]?.ToString();
            LoginCredentialsForAudit.Email = testContext.Properties["auditEmail"]?.ToString();
            LoginCredentialsForAudit.Password = testContext.Properties["auditPassword"]?.ToString();
            LoginCredentialsForAudit.Name = testContext.Properties["auditName"]?.ToString();

            LoginCredentialsForSpecialist.Email = testContext.Properties["specialistEmail"]?.ToString();
            LoginCredentialsForSpecialist.Password = testContext.Properties["specialistPassword"]?.ToString();
            LoginCredentialsForSpecialist.Name = testContext.Properties["specialistName"]?.ToString();

            LoginCredentialsForPayroll.Email = testContext.Properties["payrollEmail"]?.ToString();
            LoginCredentialsForPayroll.Password = testContext.Properties["payrollPassword"]?.ToString();
            LoginCredentialsForPayroll.Name = testContext.Properties["payrollName"]?.ToString();

            LoginCredentialsForAccountReceivable.Email = testContext.Properties["AccountReceivableEmail"]?.ToString();
            LoginCredentialsForAccountReceivable.Password = testContext.Properties["AccountReceivablePassword"]?.ToString();
            LoginCredentialsForAccountReceivable.Name = testContext.Properties["AccountReceivableName"]?.ToString();

            //TimeSheet
            TimeSheetUrl = $"https://traveler-{Env}-wa.azurewebsites.net/#/";
            TimeSheetLoginCredentials.Email = testContext.Properties["tsEmail"]?.ToString();
            TimeSheetLoginCredentials.Password = testContext.Properties["tsPassword"]?.ToString();
            TimeSheetLoginCredentials.Name = testContext.Properties["tsName"]?.ToString();

            //FMS
            FmsUrl = $"https://marketplace-{Env}.fusionmedstaff.com/";

            //FMP
            GreenkartUrl = "https://rahulshettyacademy.com/seleniumPractise/#/";
            FusionMarketPlaceUrl = $"https://{Env}.fusionmarketplace.com/";
            FusionMarketPlaceLoginCredentials.Email = testContext.Properties["FMPUserEmail"]?.ToString();
            FusionMarketPlaceLoginCredentials.Password = testContext.Properties["FMPUserPassword"]?.ToString();
            FusionMarketPlaceLoginCredentials.Name = testContext.Properties["FMPUserName"]?.ToString();

            Capability.DeviceName = testContext.Properties["DeviceName"]?.ToString();
            Capability.PlatformVersion = testContext.Properties["PlatformVeriosn"]?.ToString();
            Capability.AutomationName = testContext.Properties["AutomationName"]?.ToString();

        }


        [TestInitialize]
        public void Setup()
        {
            Log = new Logger($"{FileUtil.GetBasePath()}/Resources/Logs/{SetFileName("Log")}.log");
            Log.Info($"Starting test {TestContext.TestName}");
            
            //Note: Run for Android Native app
            if (PlatformName.Equals(PlatformName.AndroidApp))
            {
                CleanChromeProfile();
            }
            Driver = DriverFactory.InitDriver(AppiumLocalService, Capability);
        }

        [AssemblyCleanup]
        public static void CleanUp()
        {
            if (PlatformName != PlatformName.Web)
            {
                AppiumLocalService.Dispose();
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            Log.Info($"Result - {TestContext.TestName} {TestContext.CurrentTestOutcome.ToString()}");

            if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                try
                {
                    var screenshotsPath =
                        $"{FileUtil.GetBasePath()}/Resources/Screenshots/{SetFileName("IMG")}.png";
                    Driver.TakeScreenShot(screenshotsPath);
                    TestContext.AddResultFile(screenshotsPath);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
            try
            {
                TestContext.AddResultFile(Log.LogPath);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            DriverFactory.CloseDriver();
        }

        private string SetFileName(string type)
        {
            var fullyQualifiedTestClassName = TestContext.FullyQualifiedTestClassName.Split('.');
            var className = fullyQualifiedTestClassName[^1];
            var filename = $"[Test]_[{type}]_{new CSharpHelpers().GenerateRandomNumber()}_{className}_{TestContext.TestName}_{DateTime.Now.ToString("yy-MM-dd HH.mm.ss")}";
            if (filename.Length > 70)
            {
                filename = filename.Substring(0, 70);
            }
            return filename;
        }

        private void CleanChromeProfile()
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, Capability.DeviceName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, Capability.PlatformName);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, Capability.PlatformVersion);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, "chrome");
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, Capability.AutomationName);
            appiumOptions.AddAdditionalCapability("unicodeKeyboard", "true");
            appiumOptions.AddAdditionalCapability("resetKeyboard", "true");
            appiumOptions.AddAdditionalCapability("appWaitForLaunch", "true");
            Driver = new AndroidDriver<AndroidElement>(AppiumLocalService, appiumOptions);
        }
    }
}
