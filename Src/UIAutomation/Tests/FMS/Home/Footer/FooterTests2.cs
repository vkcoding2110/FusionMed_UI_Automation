using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.Common;
using UIAutomation.PageObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMS.Home.Footer
{
    [TestClass]
    [TestCategory("Footer"), TestCategory("FMS")]
    public class FooterTests2 : BaseTest
    {
        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyBlogLinkWorkSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Click on Blog link & verify blog page gets open");
            const string expectedBlogHref = "https://blog.fusionmedstaff.com/";
            var actualBlogPageHref = footer.GetBlogHref();
            Assert.AreEqual(expectedBlogHref, actualBlogPageHref, "Blog page url is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifySubscribeToOurNewsletterWorksSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Enter email, click on arrow button & verify 'Thank You' page gets open");
            var emailId = UserDataFactory.AddUserInformation();
            footer.EnterNewsLetterEmail(emailId.Email);
            footer.ClickOnNewsLetterArrow();
            const string expectedThankYouMessage = "Aw, yeah! Let's party! Celebrate in style and look for your gift card soon!";
            var actualThankYouMessage = footer.GetNewsletterRepliedSuccessText();
            Assert.AreEqual(expectedThankYouMessage.RemoveWhitespace(), actualThankYouMessage.RemoveWhitespace(), "Newsletter Success Text is not matched");
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyValidationMessageIsDisplayedForSubscribeToOurNewsletterWorkSuccessfully()
        {
            var footer = new FooterPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FmsUrl}");
            Driver.NavigateTo(FmsUrl);
            footer.WaitUntilMpPageLoadingIndicatorInvisible();

            Log.Info("Step 2: Do not enter email ,click on arrow button & verify Newsletter success text");
            footer.EnterNewsLetterEmail(" ");
            footer.ClickOnNewsLetterArrow();
            Assert.IsFalse(footer.IsNewsletterRepliedSuccessTextDisplayed(), "Newsletter success text is displayed");
        }
    }
}

