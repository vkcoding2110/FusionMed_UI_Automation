using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account.NativeApp;
using UIAutomation.PageObjects.FMP.Home.NativeApp;
using UIAutomation.PageObjects.FMP.NativeApp.More;
using UIAutomation.PageObjects.FMP.Traveler.Profile.NativeApp;
using UIAutomation.PageObjects.FMP.Traveler.Profile.ProfileSharing.NativeApp;
using UIAutomation.SetUpTearDown.FMP.NativeApp;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.ProfileSharing.NativeApp
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("NativeAppAndroid")]
    public class ProfileSharingTests2 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByTypeAndPlatform("ManageMyProfileSharingTests");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteProfileSharingDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void Profile_VerifyAddMultipleShareMyProfileDetailsWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var homePage = new HomePagePo(Driver);
            var profile = new ProfileDetailPagePo(Driver);
            var moreMenu = new MoreMenuPo(Driver);
            var profileSharing = new ProfileSharingPo(Driver);
            var emailData = new List<string>
            {
               "test" + new CSharpHelpers().GenerateRandomNumber() + "@yopmail.com",
               "test" + new CSharpHelpers().GenerateRandomNumber() + "@yopmail.com",
               "test" + new CSharpHelpers().GenerateRandomNumber() + "@yopmail.com"
            };

            Log.Info("Step 1: Open FMP App & Login to the application");
            homePage.OpenHomePage();
            fmpLogin.LoginToApplication(UserLogin);

            Log.Info("Step 2: Click on 'Profile' icon and Click on 'Share My Profile' button");
            homePage.ClickOnMoreMenuButton();
            moreMenu.ClickOnProfileOption();
            profile.ClickOnShareMyProfileButton();
            profileSharing.DeleteAllProfileSharingEmails();

            Log.Info("Step 3: Click on 'Add Recipient Via Email' button, Add details in 'Share My Profile' form, click on 'Share My Profile' button and verify details added successfully");
            profileSharing.DeleteAllProfileSharingEmails();
            foreach (var email in emailData)
            {
                profileSharing.ClickOAddRecipientViaEmailButton();
                profileSharing.AddShareMyProfileDetails(email);
            }
            var actualAddRecipientViaEmail = profileSharing.GetAllowedEmailsList();
            CollectionAssert.AreEqual(emailData.ToList(), actualAddRecipientViaEmail.ToList(), "Allowed emails list is not matched");

            //Clean up
            try
            {
                profileSharing.DeleteAllProfileSharingEmails();
            }
            catch
            {
                //Do nothing
            }
        }
    }
}

