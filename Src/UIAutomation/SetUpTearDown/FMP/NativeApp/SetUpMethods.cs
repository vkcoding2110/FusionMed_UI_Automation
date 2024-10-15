using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.NativeApp.More;
using UIAutomation.PageObjects.FMP.Traveler.Profile.EducationAndCertification.NativeApp;
using UIAutomation.PageObjects.FMP.Traveler.Profile.ProfileSharing.NativeApp;
using UIAutomation.PageObjects.FMP.Traveler.Profile.Licensing.NativeApp;
using UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments.NativeApp;
using UIAutomation.PageObjects.FMP.Traveler.Profile.NativeApp;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.SetUpTearDown.FMP.NativeApp
{
    public class SetUpMethods : BaseTest
    {
        public SetUpMethods(TestContext testContext)
        {
            Log = new Logger(testContext);
            var fileUtil = new FileUtil();
            Log = new Logger(
                $"{fileUtil.GetBasePath()}/Resources/Logs/[LOG]_{testContext.FullyQualifiedTestClassName}_{DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss")}.log");
            Driver = DriverFactory.InitDriver(AppiumLocalService, Capability);
        }
        public void DeleteAllDocumentDetails(Login login)
        {
            try
            {

                var fmpLogin = new FmpLoginPo(Driver);
                var homepagePo = new HomePagePo(Driver);
                var documentDetailPagePo = new DocumentDetailPo(Driver);
                var moreMenu = new MoreMenuPo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                homepagePo.OpenHomePage();
                fmpLogin.LoginToApplication(login);
                fmpLogin.WaitUntilAppLoadingIndicatorInvisible();

                Log.Info("Step 2: Click on profile icon and delete the document details");
                homepagePo.ClickOnMoreMenuButton();
                moreMenu.ClickOnProfileOption();
                documentDetailPagePo.DeleteAllDocumentsDetails();
                homepagePo.WaitUntilAppLoadingIndicatorInvisible();
            }
            catch
            {
                //Nothing 
            }
            finally
            {
                Driver.Quit();
            }
        }

        public void DeleteAllCertificationDetails(Login login)
        {
            try
            {
                var fmpLogin = new FmpLoginPo(Driver);
                var homepagePo = new HomePagePo(Driver);
                var certification = new CertificationPo(Driver);
                var moreMenu = new MoreMenuPo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                homepagePo.OpenHomePage();
                fmpLogin.LoginToApplication(login);
                fmpLogin.WaitUntilAppLoadingIndicatorInvisible();

                Log.Info("Step 2: Click on 'Profile' icon and delete the 'Certification' details");
                homepagePo.ClickOnMoreMenuButton();
                moreMenu.ClickOnProfileOption();
                certification.DeleteCertification();
            }
            catch
            {
                //Nothing 
            }
            finally
            {
                Driver.Quit();
            }
        }

        public void DeleteProfileSharingDetails(Login login)
        {
            try
            {
                var fmpLogin = new FmpLoginPo(Driver);
                var homepagePo = new HomePagePo(Driver);
                var moreMenu = new MoreMenuPo(Driver);
                var profile = new ProfileDetailPagePo(Driver);
                var profileSharing = new ProfileSharingPo(Driver);

                Log.Info($"Step 1: Open FMP App & Login to the application");
                homepagePo.OpenHomePage();
                fmpLogin.LoginToApplication(login);

                Log.Info("Step 2: Click on 'Manage My Profile Sharing' button , click on 'Add Recipient via Email' button, Click on 'Delete' Link");
                homepagePo.ClickOnMoreMenuButton();
                moreMenu.ClickOnProfileOption();
                profile.ClickOnShareMyProfileButton();
                profileSharing.DeleteAllProfileSharingEmails();
            }
            catch
            {
                //Nothing 
            }
            finally
            {
                Driver.Quit();
            }
        }

        public void DeleteLicenseDetails(Login login)
        {
            try
            {
                var fmpLogin = new FmpLoginPo(Driver);
                var homepagePo = new HomePagePo(Driver);
                var license= new LicenseDetailsPo(Driver);
                var moreMenu = new MoreMenuPo(Driver);

                Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
                homepagePo.OpenHomePage();
                fmpLogin.LoginToApplication(login);
                fmpLogin.WaitUntilAppLoadingIndicatorInvisible();

                Log.Info("Step 2: Click on 'Profile' icon and delete the 'License' details");
                homepagePo.ClickOnMoreMenuButton();
                moreMenu.ClickOnProfileOption();
                license.DeleteLicense();
            }
            catch
            {
                //Nothing 
            }
            finally
            {
                Driver.Quit();
            }
        }
    }
}
