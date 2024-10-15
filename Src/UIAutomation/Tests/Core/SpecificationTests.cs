using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.PageObjects.Core.Common;
using UIAutomation.PageObjects.Core.Specification;
using UIAutomation.PageObjects.Microsoft;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.Core
{
    [TestClass]
    [TestCategory("Core"), TestCategory("Specification")]
    public class SpecificationTests: BaseTest
    {
        public void VerifyThatSpecificationsAuditorPendingButtonWorkSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var dashboard = new DashboardPo(Driver);
            var specifications = new SpecificationGridPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAudit.Email} ,  password : {LoginCredentialsForAudit.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAudit);

            Log.Info("Step 3: Click on 'Pending' button and verify page is opened successfully and all records has status = Pending");
            dashboard.SwitchToIFrame();
            dashboard.ClickOnPendingButton();
            specifications.WaitUntilCoreProcessingTextInvisible();
            specifications.SwitchToIFrame();
            var count = specifications.GetCountOfRows();
            const string expectedStatus = "Pending";
            for (var i = 1; i <= count; i++)
            {
                var status = specifications.GetNthRowStatus(i);
                Assert.IsTrue(status.Equals(expectedStatus), $"Status is not matched. Expected:{expectedStatus}, Actual: {status}");
            }

        }

        [TestMethod]
        public void VerifyThatSpecificationsAuditorDeniedButtonWorkSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var dashboard = new DashboardPo(Driver);
            var specifications = new SpecificationGridPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAudit.Email} ,  password : {LoginCredentialsForAudit.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAudit);

            Log.Info("Step 3: Click on 'Denied' red button and verify page is opened successfully and all rows have status= Denied");
            dashboard.SwitchToIFrame();
            dashboard.ClickOnDeniedButton();
            specifications.WaitUntilCoreProcessingTextInvisible();
            specifications.SwitchToIFrame();
            var count = specifications.GetCountOfRows();
            const string expectedStatus = "Denied";
            for (var i = 1; i <= count; i++)
            {
                var status = specifications.GetNthRowStatus(i);
                Assert.IsTrue(status.Equals(expectedStatus), $"Status is not matched. Expected:{expectedStatus}, Actual: {status}");
            }

        }

        [TestMethod]
        public void VerifyThatSpecificationsAuditorNaButtonWorkSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var dashboard = new DashboardPo(Driver);
            var specifications = new SpecificationGridPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAudit.Email} ,  password : {LoginCredentialsForAudit.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAudit);

            Log.Info("Step 3: Click on 'NA' button and verify page is opened successfully and all rows have status= N/A");
            dashboard.SwitchToIFrame();
            dashboard.ClickOnNAButton();
            specifications.SwitchToIFrame();
            specifications.WaitUntilCoreProcessingTextInvisible();
            var count = specifications.GetCountOfRows();
            const string expectedStatus = "N/A";
            for (var i = 1; i <= count; i++)
            {
                var status = specifications.GetNthRowNthColumnData(i, 8);
                Assert.IsTrue(status.Equals(expectedStatus), $"Status is not matched. Expected:{expectedStatus}, Actual: {status}");
            }
        }

        [TestMethod]
        public void NARecords_VerifyThatApproveButtonWorksSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var dashboard = new DashboardPo(Driver);
            var specifications = new SpecificationGridPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAudit.Email} ,  password : {LoginCredentialsForAudit.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAudit);

            Log.Info("Step 3: Click on 'NA' button");
            dashboard.SwitchToIFrame();
            dashboard.ClickOnNAButton();
            specifications.SwitchToIFrame();

            Log.Info("Step 4: Click on first record's 'Approved' button & verify status is changed to approved");
            specifications.ClickOnNthRowNthApproveButton(1);
            const string expectedResult = "Approved";
            var actualResult = specifications.GetNthRowStatus(1);
            Assert.AreEqual(expectedResult, actualResult, "Status is not changed");
        }

        [TestMethod]
        public void NARecords_VerifyThatPendingButtonWorksSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var dashboard = new DashboardPo(Driver);
            var specifications = new SpecificationGridPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAudit.Email} ,  password : {LoginCredentialsForAudit.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAudit);

            Log.Info("Step 3: Click on 'NA' button");
            dashboard.SwitchToIFrame();
            dashboard.ClickOnNAButton();
            specifications.SwitchToIFrame();

            Log.Info("Step 4: Click on first record's 'Pending' button & verify status is changed to 'Pending' ");
            specifications.ClickOnNthRowPendingButton(1);
            var actualStatus = specifications.GetNthRowStatus(1);
            const string expectedStatus = "Pending";
            Assert.AreEqual(expectedStatus, actualStatus, "Status is not changed");
        }

        [TestMethod]
        public void NARecords_VerifyThatDeniedActionAuditorEmailNotificationPersonSelectionWorkSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var dashboard = new DashboardPo(Driver);
            var specifications = new SpecificationGridPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAudit.Email} ,  password : {LoginCredentialsForAudit.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAudit);

            Log.Info("Step 3: Click on 'NA' button");
            dashboard.SwitchToIFrame();
            dashboard.ClickOnNAButton();
            specifications.SwitchToIFrame();

            Log.Info("Step 4: Click on 'Denied' button, click on 'Email Notification' dropdown then select first value from list and verify value is selected successfully");
            specifications.ClickOnNthRowDeniedButton(1);
            var expectedValue = specifications.AuditorNotesSelectFirstDropDownValue();
            var actualValue = specifications.GetAuditNotesDropDownFirstSelectedValue();
            Assert.AreEqual(expectedValue, actualValue, "Person name is not matched");
        }

        [TestMethod]
        public void NARecords_VerifyThatDeniedActionAuditorAddNoteWorksSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var dashboard = new DashboardPo(Driver);
            var specifications = new SpecificationGridPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAudit.Email} ,  password : {LoginCredentialsForAudit.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAudit);

            Log.Info("Step 3: Click on 'NA' button");
            dashboard.SwitchToIFrame();
            dashboard.ClickOnNAButton();
            specifications.SwitchToIFrame();

            Log.Info("Step 4: Click on 'Denied' button, add note & click on 'Add Note' button");
            specifications.ClickOnNthRowDeniedButton(1);
            var textNote = "Test" + new CSharpHelpers().GenerateRandomNumber();
            specifications.AuditorNotesAddNotesInTextArea(textNote);
            specifications.AuditorNotesClickOnAddNoteButton();

            Log.Info("Step 5: Verify note added successfully");
            var actualNote = specifications.AuditorNotesGetNthRowNthColumnText(1, 6);
            Assert.AreEqual(textNote, actualNote, "Text is not matched");
        }

        [TestMethod]
        public void NARecords_VerifyThatAuditorNotesCloseButtonWorkSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var dashboard = new DashboardPo(Driver);
            var specifications = new SpecificationGridPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAudit.Email} ,  password : {LoginCredentialsForAudit.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAudit);

            Log.Info("Step 3: Click on 'NA' button");
            dashboard.SwitchToIFrame();
            dashboard.ClickOnNAButton();
            specifications.SwitchToIFrame();

            Log.Info("Step 4: Click on 'Denied' button, add note & click on 'Add Note' button ");
            specifications.ClickOnNthRowDeniedButton(1);
            var textNote = "Test" + new CSharpHelpers().GenerateRandomNumber();
            specifications.AuditorNotesAddNotesInTextArea(textNote);
            specifications.AuditorNotesClickOnAddNoteButton();

            Log.Info("Step 5: Click on 'Close' button & verify status is changed to 'Denied'");
            specifications.AuditorNotesClickOnCloseButton();
            var actualStatus = specifications.GetNthRowStatus(1);
            const string expectedStatus = "Denied";
            var actualNotes = specifications.GetNthRowNthColumnData(1, 14);
            Assert.AreEqual(expectedStatus, actualStatus, "Status is not matched");
            Assert.IsTrue(actualNotes.EndsWith(textNote), $"Added Notes is not matched. Expected : {textNote}, Actual : {actualNotes}");
        }

        public void NARecords_VerifyThatStatusChangedToDeniedInDeniedRecords()
        {
            var microsoftLogin = new LoginPo(Driver);
            var dashboard = new DashboardPo(Driver);
            var specifications = new SpecificationGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForAudit.Email} ,  password : {LoginCredentialsForAudit.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForAudit);

            Log.Info("Step 3: Click on 'NA' button");
            dashboard.SwitchToIFrame();
            dashboard.ClickOnNAButton();
            specifications.SwitchToIFrame();

            Log.Info("Step 4: Fetch First Record Id, name & click first record's 'Denied' button ");
            var id = specifications.GetNthRowNthColumnData(1, 1);
            var name = specifications.GetNthRowNthColumnData(1, 2);
            specifications.ClickOnNthRowDeniedButton(1);

            Log.Info("Step 5: Add notes, click on 'Add Note' button & close Auditor Note popup, close Audit-NA Menu ");
            var textNote = "Test" + new CSharpHelpers().GenerateRandomNumber();
            specifications.AuditorNotesAddNotesInTextArea(textNote);
            specifications.AuditorNotesClickOnAddNoteButton();
            specifications.AuditorNotesClickOnCloseButton();
            specifications.AuditNAMenuClickOnCloseButton();

            Log.Info("Step 6: Navigate to dashboard, click on 'denied' button & search for denied record id");
            leftNav.ClickOnDashboardMenu();
            Driver.RefreshPage();
            dashboard.SwitchToIFrame();
            dashboard.ClickOnDeniedButton();
            specifications.SwitchToIFrame();
            specifications.EnterDataToGridInputTextBoxNthColumn(1, id);

            Log.Info("Step 7 : Verify id, name & status is matched");
            var actualId = specifications.GetNthRowNthColumnData(1, 1);
            var actualName = specifications.GetNthRowNthColumnData(1, 2);
            var actualStatus = specifications.GetNthRowStatus(1);
            Assert.AreEqual(id, actualId, "Id is not matched.");
            Assert.AreEqual(name, actualName, "Name is not matched");
            Assert.AreEqual("Denied", actualStatus, "Status is not matched");
        }
    }
}
