using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Agencies.Recruiters.Admin;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Agencies.Recruiter;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Agencies.Recruiters
{
    [TestClass]
    [TestCategory("RecruiterProfile"), TestCategory("FMP")]
    public class RecruitersEditAboutMeTests : FmpBaseTest
    {
        private static readonly Login RecruiterEmail = GetLoginUsersByType("RecruitersEditAboutMe");
        public string AccountNavText = "Profile";
        
        [TestMethod]
        [TestCategory("Smoke")]
        public void RecruiterProfile_AboutMe_VerifyRecruiterCanEditDetailsSuccessfully()
        {
            var fmpHeader = new HeaderPo(Driver);
            var fmpLogin = new FmpLoginPo(Driver);
            var profileMenu = new ProfileMenuPo(Driver);
            var recruiterDetail = new RecruiterDetailsPo(Driver);
            var editAboutMe = new RecruitersEditAboutMePo(Driver);
            var profileImage = new EditProfileImagePo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{RecruiterEmail.Email}, password:{RecruiterEmail.Password}");
            fmpLogin.LoginToApplication(RecruiterEmail);
            fmpLogin.WaitUntilFmpPageLoadingIndicatorInvisible();
            fmpHeader.ClickOnLogInBadge();

            Log.Info("Step 4: Click on 'Profile' button, click on 'Edit About Me' button and verify 'Edit About Me' pop up open Successfully");
            profileMenu.ClickOnAccountNavMenuItem(AccountNavText);
            recruiterDetail.ClickOnEditAboutMeButton();
            Assert.IsTrue(editAboutMe.IsEditAboutMePopUpPresent(), "Edit About Me pop up is not opened");

            Log.Info("Step 5: Click on 'Close' and 'cancel' button,verify 'Edit About Me' pop up close Successfully");
            editAboutMe.ClickOnCloseIcon();
            Assert.IsFalse(editAboutMe.IsEditAboutMePopUpPresent(), "Edit About Me pop up is still present.");

            recruiterDetail.ClickOnEditAboutMeButton();
            editAboutMe.ClickOnCancelButton();
            Assert.IsFalse(editAboutMe.IsEditAboutMePopUpPresent(), "Edit About Me pop up is still present.");

            Log.Info("Step 6: Clear 'Department', verify validation message for 'Department' and verify 'Firstname', 'Lastname' & 'Email' field is disable");
            recruiterDetail.ClickOnEditAboutMeButton();
            editAboutMe.ClickOnDepartmentCloseIcon();
            editAboutMe.ClickOnSaveButton();
            Assert.AreEqual(FmpConstants.MandatoryFieldValidationMessage,editAboutMe.GetDepartmentValidationMessage(),"Validation message is not matched");
            Assert.AreEqual(RecruiterEmail.Name.Split(" ")[0], editAboutMe.GetFirstNameInputBoxText(), "First name is not matched");
            Assert.AreEqual(RecruiterEmail.Name.Split(" ")[1], editAboutMe.GetLastNameInputBoxText(), "Last name is not matched");
            Assert.IsFalse(editAboutMe.IsEmailInputBoxEnabled(), "Email Input Box is enabled");

            Log.Info("Step 7: Update 'Edit About Me' details");
            var editRecruitersDetail = RecruiterDataFactory.EditRecruitersDetails();
            editAboutMe.EnterRecruitersDetails(editRecruitersDetail);
            editAboutMe.ClickOnSaveButton();
           
            Log.Info("Step 8: Verify details updated on 'Recruiter' profile page");
            var recruiterDetails = recruiterDetail.GetRecruiterDetail();
            var expectedPhoneNumber = Convert.ToInt64(editRecruitersDetail.PhoneNumber).ToString("(###) ###-####");
            Assert.AreEqual(editRecruitersDetail.Department.ToString(), recruiterDetails.Specialty.ToString(), "Recruiter's specialty is not matched");
            Assert.AreEqual(editRecruitersDetail.AboutMe.RemoveWhitespace().ToLowerInvariant(), recruiterDetails.AboutMe.RemoveWhitespace().ToLowerInvariant(), "Recruiter's About Me is not matched");
            Assert.AreEqual(expectedPhoneNumber, recruiterDetails.PhoneNumber, "Recruiter's Phone Number is not matched");

            Log.Info("Step 9: Click on 'Pencil' button & verify pop up open successfully");
            editAboutMe.ClickOnEditProfileAvatarButton();
            Assert.IsTrue(editAboutMe.IsAvatarPopUpPresent(), "Edit Profile Avatar pop up is not opened");

            Log.Info("Step 10: Click on avatar 'Close' and 'cancel' button & verify pop up close Successfully");
            editAboutMe.ClickOnAvatarCloseIcon();
            Assert.IsFalse(editAboutMe.IsEditAboutMePopUpPresent(), "Edit Profile Avatar pop up is still present.");

            editAboutMe.ClickOnEditProfileAvatarButton();
            editAboutMe.ClickOnAvatarCancelButton();
            Assert.IsFalse(editAboutMe.IsEditAboutMePopUpPresent(), "Edit Profile Avatar pop up is still present.");

            Log.Info("Step 11: Click on 'Pencil' button, click on 'Choose New Photo' button, edit profile image & verify profile image gets updated");
            var editImage = AboutMeDataFactory.EditProfileDetails();
            profileImage.EditProfileImage(editImage);
            profileImage.ClickOnSaveButton();
            var expectedUrl = "blob:" + FusionMarketPlaceUrl;
            Assert.IsTrue(profileImage.GetUploadedProfileImage().StartsWith(expectedUrl), "The image is updated successfully");
        }
    }
}
