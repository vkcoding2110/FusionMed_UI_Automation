using System;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System.Linq;
using UIAutomation.Utilities;
using OpenQA.Selenium.Appium.iOS;
using UIAutomation.Enum;
using UIAutomation.Tests;

namespace UIAutomation.PageObjects.Mobile
{
    internal class MobileFileSelectionPo : BasePo
    {
        public MobileFileSelectionPo(IWebDriver driver) : base(driver)
        {
        }

        private readonly By UsingTheAppDeviceConfirmation = By.XPath("*//android.widget.Button[@text='While using the app']");
        private readonly By ChooseFileDeviceButton = By.XPath("*//android.widget.TextView[@text='Files']");
        private readonly By MenuHeader = By.XPath("//android.widget.ImageButton[@content-desc='Show roots']/following-sibling::android.widget.TextView");
        private readonly By MenuIcon = By.XPath("//android.widget.ImageButton[@content-desc='Show roots']");
        private readonly By DownloadFolder = By.XPath("//*[@class='android.widget.TextView'][@text='Downloads']");

        private readonly By PhotoLibraryButtoniOs = By.XPath("//XCUIElementTypeButton[@name='Photo Library']");
        private readonly By FirstPhoto = By.XPath("//XCUIElementTypeImage[contains(@name,'Photo')][1]");
        private readonly By ChoosePhotoButtoniOs = By.XPath("//XCUIElementTypeStaticText[@name='Choose']");
        private readonly By SharePopUp = By.XPath("//*[@resource-id='android:id/title']");
        private readonly By SharedFileTitle = By.XPath("//*[@resource-id='android:id/content_preview_filename']");

        //Upload Image
        private readonly By ChooseImageFromDevice = By.XPath("//*[@class='android.widget.Button'][@text='Choose from Device']");
        private readonly By ChromePermission = By.XPath("*//android.widget.Button[@text='Allow']");
        private readonly By BrowseButton = By.XPath("*//android.widget.TextView[@text='Browse']");
        private readonly By ListOrGridViewButton= By.XPath("*//android.widget.TextView[contains(@content-desc,'view')]");

        private static By SelectFileButton(string fileName) => By.XPath($"*//android.widget.TextView[@text='{fileName}']");

        //Device Download button
        private readonly By DownloadButton = By.XPath("//*[@class='android.widget.Button'][@text='Download']");

        public void SelectFileFromDevice(string filePath)
        {
            var fileName = filePath.Split('/').Last();
            var androidDriver = (AndroidDriver<AndroidElement>)Driver;
            androidDriver.Context = "NATIVE_APP";
            androidDriver.PushFile($"/sdcard/Download/{fileName}", new FileInfo(filePath));

            if (Wait.IsElementPresent(UsingTheAppDeviceConfirmation, 3))
            {
                Wait.UntilElementClickable(UsingTheAppDeviceConfirmation).ClickOn();
                Wait.UntilElementClickable(ChooseFileDeviceButton).ClickOn();
            }
            else
            {
                if (Wait.IsElementPresent(ChooseImageFromDevice, 3))
                {
                    Wait.UntilElementClickable(ChooseImageFromDevice, 120).ClickOn();
                }
                if (Wait.IsElementPresent(ChromePermission, 3))
                {
                    Wait.UntilElementClickable(ChromePermission).ClickOn();
                }
                if (Wait.IsElementPresent(BrowseButton, 3))
                {
                    Wait.UntilElementClickable(BrowseButton).ClickOn();
                }
            }

            var menuHeaderText = Wait.UntilElementVisible(MenuHeader).GetAttribute("text");
            if (!menuHeaderText.Equals("Downloads"))
            {
                Wait.UntilElementClickable(MenuIcon).ClickOn();
                Wait.UntilElementClickable(DownloadFolder).ClickOn();
            }

            var listOrGridIcon = Wait.UntilElementVisible(ListOrGridViewButton).GetAttribute("content-desc");
            if (listOrGridIcon.Equals("List view"))
            {
                Wait.UntilElementVisible(ListOrGridViewButton).Click();
            }
            Wait.UntilElementClickable(SelectFileButton(fileName)).ClickOn();
            if(BaseTest.Capability.Browser.ToEnum<BrowserName>().Equals(BrowserName.Chrome) && BaseTest.PlatformName.Equals(PlatformName.Android))
            {
                androidDriver.Context = "CHROMIUM";
            }
        }

        public void ClickOnDownloadButton()
        {
            var androidDriver = (AndroidDriver<AndroidElement>)Driver;
            androidDriver.Context = "NATIVE_APP";
            Wait.UntilElementClickable(DownloadButton).ClickOn();
            Wait.UntilElementInVisible(DownloadButton,5);
            androidDriver.Context = "CHROMIUM";
        }

        public bool IsFilePresentOnDevice(string filePath)
        {
            var fileName = filePath.Split('/').Last();
            var androidDriver = (AndroidDriver<AndroidElement>)Driver;
            androidDriver.Context = "NATIVE_APP";
            try
            {
                var file = androidDriver.PullFile($"/sdcard/Download/{fileName}" + ".pdf");
                androidDriver.Context = "CHROMIUM";
                return file.Length > 0;
            }
            catch (WebDriverException)
            {
                androidDriver.Context = "CHROMIUM";
                return false;
            }
        }
        public void SelectPhotoFromiOs()
        {
            var iOsDriver = (IOSDriver<IOSElement>)Driver;
            iOsDriver.Context = "NATIVE_APP";
            Wait.UntilElementExists(PhotoLibraryButtoniOs).ClickOn();
            Wait.HardWait(10000);
            Wait.UntilElementExists(FirstPhoto).ClickOn();
            Wait.UntilElementExists(ChoosePhotoButtoniOs).ClickOn();
        }
        public bool IsSharePopUpDisplayed()
        {
            return Wait.IsElementPresent(SharePopUp,5);
        }

        public string GetSharedFileName()
        {
            return Wait.UntilElementVisible(SharedFileTitle).GetText();
        }
        public void DeleteExistingFileFromEmulator(string fileName)
        {
            if (BaseTest.PlatformName != PlatformName.Android) return;
            var uniqueNumber = DateTime.Now.ToString("yyMMddHHmmssf");
            var batFileName = new FileUtil().GetBasePath() + "/TestData/FMP/RemoveFileCommand" + uniqueNumber + ".bat";
            using (var batFile = new StreamWriter(batFileName))
            {
                batFile.WriteLine($"adb shell cd \"sdcard && cd download && rm {fileName}\"");
            }
            var processInfo = new ProcessStartInfo("cmd.exe", "/c" + batFileName)
            {
                UseShellExecute = true,
                CreateNoWindow = false
            };
            var process = new Process { StartInfo = processInfo };
            process.Start();
            process.WaitForExit();
        }
    }
}