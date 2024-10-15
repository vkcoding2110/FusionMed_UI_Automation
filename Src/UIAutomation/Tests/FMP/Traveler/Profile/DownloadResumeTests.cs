using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.Enum;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class DownloadResumeTests : FmpBaseTest
    {
        [TestMethod]
        [TestCategory("Smoke"), TestCategory("MobileReady"), TestCategory("IosNotReady")]
        [DoNotParallelize]
        public void VerifyThatTravelerResumeDownloadedSuccessfully()
        {
            var headerHomePagePo = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var editProfile = new EditAboutMePo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var fileUtil = new FileUtil();

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            headerHomePagePo.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{FusionMarketPlaceLoginCredentials.Email}, password:{FusionMarketPlaceLoginCredentials.Password}");
            fmpLogin.LoginToApplication(FusionMarketPlaceLoginCredentials);
            editProfile.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Delete existing file from folder, click on 'Download My Resume' button & click on 'Select All' label and verify Resume is downloaded successfully");
            var downloadPath = fileUtil.GetDownloadPath();
            var filename = FusionMarketPlaceLoginCredentials.Name.RemoveWhitespace() + "_Resume";
            fileUtil.DeleteFileInFolder(downloadPath, filename, ".pdf");
            profileDetails.ClickOnDownloadMyResumeButton();
            Assert.IsTrue(
                PlatformName != PlatformName.Web
                    ? new MobileFileSelectionPo(Driver).IsFilePresentOnDevice(filename)
                    : fileUtil.DoesFileExistInFolder(downloadPath, filename, ".pdf", 15),
                $"File - {filename} not found!");
        }
    }
}
