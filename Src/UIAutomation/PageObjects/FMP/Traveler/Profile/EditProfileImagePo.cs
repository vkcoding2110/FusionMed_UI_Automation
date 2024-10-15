using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile
{
    internal class EditProfileImagePo : FmpBasePo
    {
        public EditProfileImagePo(IWebDriver driver) : base(driver)
        {
        }

        //Upload Profile Image
        private readonly By ProfileImagePencilButton = By.XPath("//button[contains(@class,'PhotoEditButton')]");
        private readonly By ChooseNewPhotoButton = By.XPath("//div[contains(@class,'ChooseNewPhoto')]");
        private readonly By UploadImageFileInput = By.XPath("//div[@class='dropped-files']/preceding-sibling::input");
        private readonly By SaveButton = By.XPath("//button[contains(@class,'CallToActionButton')]");
        private readonly By UploadedProfileImage = By.XPath("//div[contains(@class,'AvatarWrapper')]//img");
        private readonly By ZoomLabel = By.XPath("//div[contains(@class,'ZoomLabel')]");
        private readonly By UploadImageCancelButton = By.XPath("//div[contains(@class,'CancelTouch')]");
        private readonly By UploadImageCloseIcon = By.XPath("//div[contains(@class,'CloseIconWrapper')]");
        private readonly By ImageZoomScrollBar = By.XPath("//div[contains(@class,'ZoomLabel')]//following-sibling::input");
        private readonly By ValidationMessageForFileSize = By.XPath("//div[contains(@class,'ValidFileBox')]");

        //Upload Profile Image
        public void ClickOnProfileImagePencilButton()
        {
            Wait.UntilElementClickable(ProfileImagePencilButton).ClickOn();
            Wait.HardWait(1000);
        }

        public void UploadPhotoFromDevice(DataObjects.FMP.Traveler.Profile.Profile imageFilePath)
        {
            try
            {
                switch (BaseTest.PlatformName)
                {
                    case PlatformName.Web:
                        Wait.UntilElementExists(UploadImageFileInput).SendKeys(imageFilePath.ImageFilePath);
                        break;
                    case PlatformName.Android:
                        new MobileFileSelectionPo(Driver).SelectFileFromDevice(imageFilePath.ImageFilePath);
                        break;
                    case PlatformName.Ios:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception)
            {
                //Nothing
            }

            Wait.UntilElementClickable(SaveButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public void EditProfileImage(DataObjects.FMP.Traveler.Profile.Profile imageFilePath)
        {
            Wait.HardWait(2000);
            Driver.JavaScriptClickOn(ProfileImagePencilButton);
            Driver.JavaScriptClickOn(ChooseNewPhotoButton);
            try
            {
                switch (BaseTest.PlatformName)
                {
                    case PlatformName.Web:
                        Wait.UntilElementExists(UploadImageFileInput).SendKeys(imageFilePath.ImageFilePath);
                        break;
                    case PlatformName.Android:
                        new MobileFileSelectionPo(Driver).SelectFileFromDevice(imageFilePath.ImageFilePath);
                        break;
                    case PlatformName.Ios:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception)
            {
                //Nothing
            }
        }

        public void ClickOnSaveButton()
        {
            Wait.UntilElementClickable(SaveButton).ClickOn();
            WaitUntilFmpPageLoadingIndicatorInvisible();
        }

        public string GetUploadedProfileImage()
        {
            return Wait.UntilElementVisible(UploadedProfileImage).GetAttribute("src");
        }

        public void ClickOnUploadImageCancelButton()
        {
            Wait.UntilElementClickable(UploadImageCancelButton).ClickOn();
        }

        public bool IsZoomLabelDisplayed()
        {
            return Wait.IsElementPresent(ZoomLabel, 5);
        }

        public bool IsValidationMessageDisplayedForFileUpload()
        {
            return Wait.IsElementPresent(ValidationMessageForFileSize, 5);
        }
        public void ClickOnUploadImageCloseIcon()
        {
            Driver.JavaScriptClickOn(UploadImageCloseIcon);
        }

        public void EditProfileImageScrolling(DataObjects.FMP.Traveler.Profile.Profile imageFilePath)
        {
            Wait.UntilElementClickable(ProfileImagePencilButton).ClickOn();
            Wait.HardWait(1000);
            Wait.UntilElementClickable(ChooseNewPhotoButton).ClickOn();
            try
            {
                switch (BaseTest.PlatformName)
                {
                    case PlatformName.Web:
                        Wait.UntilElementExists(UploadImageFileInput).SendKeys(imageFilePath.ImageFilePath);
                        break;
                    case PlatformName.Android:
                        new MobileFileSelectionPo(Driver).SelectFileFromDevice(imageFilePath.ImageFilePath);
                        break;
                    case PlatformName.Ios:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception)
            {
                //Nothing
            }

            var actionBuilder = new Actions(Driver);
            actionBuilder.MoveToElement(Wait.UntilElementExists(ImageZoomScrollBar)).Click()
                .ClickAndHold(Wait.UntilElementVisible(ImageZoomScrollBar)).MoveByOffset(150, 0).Build().Perform();
            Wait.HardWait(3000);
        }

        public string GetImageZoomValue()
        {
            return Wait.UntilElementVisible(ImageZoomScrollBar).GetAttribute("value");
        }
    }
}
