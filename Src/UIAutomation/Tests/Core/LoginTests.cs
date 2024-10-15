using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.Core.Common;
using UIAutomation.PageObjects.Microsoft;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.Core
{
    [TestClass]
    [TestCategory("Core"), TestCategory("Login")]
    public class LoginTests: BaseTest
    {
        [TestMethod]
        public void VerifyThatUserIsLoggedInSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var header = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info("Step 3: Verify that logged in user name displayed on top left side of the page");
            Assert.AreEqual(LoginCredentials.Name, header.GetProfileName(), "Profile Name didn't match");
        }
    }
}
