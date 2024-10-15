using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UIAutomation.DataObjects.Core.Facility;
using UIAutomation.PageObjects.Core.Assignment;
using UIAutomation.PageObjects.Core.Common;
using UIAutomation.PageObjects.Microsoft;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.Core
{
    [TestClass]
    [TestCategory("Core"), TestCategory("Assignment")]
    public class AssignmentTest : BaseTest
    {
        private readonly Assignment AssignmentData = new JsonHelpers<Assignment>().DeserializeJsonObject(new FileUtil().GetBasePath() + "/TestData/Core/assignment.json");

        [TestMethod]
        public void VerifyThatRecordsAreShownCorrectlyAsPerShowDropdownSelection()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info("Step 3: Navigate to Assignment page and select Show=100, and verify that 100 results are returned");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.SelectShowDropdownValue("100");
            var totalRows = assignment.GetCountOfRows();
            Assert.AreEqual(100, totalRows, "Result count doesn't match");

        }

        [TestMethod]
        public void VerifyThatRecordSearchByAssignIsSuccessful()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var assign = AssignmentData.Assign;

            Log.Info($"Step 3: Navigate to Assignment page and search by Id= {assign}");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToAssignSearchTextBoxAndHitEnter(assign);
           
            Log.Info($"Step 4: Verify that 1 result is returned having Id = {assign}");
            var totalRows = assignment.GetCountOfRows();
            Assert.AreEqual(1, totalRows, "Result count doesn't match");
            Assert.AreEqual(assign, assignment.GetDataFromNthRowNthColumn(1, 1), "ID doesn't match");
        }

        [TestMethod]
        public void VerifyThatRecordSearchByNameIsSuccessful()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var name = AssignmentData.Name;

            Log.Info($"Step 3: Navigate to Assignment page and search by Name= {name}");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToNameSearchTextBoxAndHitEnter(name);

            Log.Info($"Step 4: Verify that all results are returned having Name = {name}");
            var totalRows = assignment.GetCountOfRows();
            for (var i = 1; i <= totalRows; i++)
            {
                Assert.IsTrue(assignment.GetDataFromNthRowNthColumn(i, 4).Contains(name), "Name doesn't match");
            }
        }

        [TestMethod]
        public void VerifyThatRecordSearchBySpecialityIsSuccessful()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var speciality = AssignmentData.Speciality;

            Log.Info($"Step 3: Navigate to Assignment page and search by Name= {speciality}");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToSpecialitySearchTextBoxAndHitEnter(speciality);
            var totalRows = assignment.GetCountOfRows();

            Log.Info($"Step 4: Verify that all results are returned having Name = {speciality}");
            for (var i = 1; i <= totalRows; i++)
            {
                Assert.IsTrue(assignment.GetDataFromNthRowNthColumn(i, 7).Contains(speciality), "Specialty doesn't match");
            }
        }

        [TestMethod]
        public void VerifyThatRecordSearchByFacilityIsSuccessful()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var facility = AssignmentData.Facility;

            Log.Info($"Step 3: Navigate to Assignment page and search by Facility = {facility}");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToFacilitySearchTextBoxAndHitEnter(facility);

            Log.Info($"Step 4: Verify that all results are returned having Name = {facility}");
            var totalRows = assignment.GetCountOfRows();
            for (var i = 1; i <= totalRows; i++)
            {
                Assert.IsTrue(assignment.GetDataFromNthRowNthColumn(i, 9).Contains(facility), "facility doesn't match");
            }
        }

        [TestMethod]
        public void VerifyThatRecordSearchByRecruiterIsSuccessful()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var recruiter = AssignmentData.Recruiter;

            Log.Info($"Step 3: Navigate to Assignment page and search by Facility = {recruiter}");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToRecruiterSearchTextBoxAndHitEnter(recruiter);

            Log.Info($"Step 4: Verify that all results are returned having Name = {recruiter}");
            var totalRows = assignment.GetCountOfRows();
            for (var i = 1; i <= totalRows; i++)
            {
                Assert.IsTrue(assignment.GetDataFromNthRowNthColumn(i, 10).Contains(recruiter), "facility doesn't match");
            }
        }

        [TestMethod]
        public void VerifyThatRecordSearchByClientManagerIsSuccessful()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var clientManager = AssignmentData.ClientManager;

            Log.Info($"Step 3: Navigate to Assignment page and search by ClientManager = {clientManager}");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToClientManagerSearchTextBoxAndHitEnter(clientManager);

            Log.Info($"Step 4: Verify that all results are returned having ClientManager = {clientManager}");
            var totalRows = assignment.GetCountOfRows();
            for (var i = 1; i <= totalRows; i++)
            {
                Assert.IsTrue(assignment.GetDataFromNthRowNthColumn(i, 11).Contains(clientManager), "ClientManager doesn't match");
            }
        }

        [TestMethod]
        public void VerifyThatRecordSearchByCsIsSuccessful()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var cs = AssignmentData.Cs;

            Log.Info($"Step 3: Navigate to Assignment page and search by CS = {cs}");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToCsSearchTextBoxAndHitEnter(cs);
            var totalRows = assignment.GetCountOfRows();

            Log.Info($"Step 4: Verify that all results are returned having CS = {cs}");
            for (var i = 1; i <= totalRows; i++)
            {
                Assert.IsTrue(assignment.GetDataFromNthRowNthColumn(i, 12).Contains(cs), "CS doesn't match");
            }
        }

        public void VerifyThatRecordSearchByBackgroundIsSuccessful()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var background = AssignmentData.Background;

            Log.Info($"Step 3: Navigate to Assignment page and search by Background = {background}");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToBackgroundSearchTextBoxAndHitEnter(background);
            var totalRows = assignment.GetCountOfRows();

            Log.Info($"Step 4: Verify that all results are returned having Background = {background}");
            for (var i = 1; i <= totalRows; i++)
            {
                Assert.IsTrue(assignment.GetDataFromNthRowNthColumn(i, 13).Contains(background), "Background doesn't match");
            }
        }

        [TestMethod]
        public void VerifyThatFileAssignedToSpecificationViaChooseCandidateFilesOption()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);
            var assignmentDetail = new AssignmentDetailPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info($"Step 3: Navigate to Assignment page and search Id");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToAssignSearchTextBoxAndHitEnter(AssignmentData.Assign);

            Log.Info($"Step 4: Click On Assign# and verify that assignment page opened successfully");
            assignment.ClickOnNthRowNthColumn(1, 1);
            var actual = assignmentDetail.GetHeaderText();
            var expected = "Assignment | " + AssignmentData.Assign + " | " + AssignmentData.Name;
            Assert.AreEqual(expected, actual, "Page Header info didn't match");

            Log.Info($"Step 5: On results grid, click on magnifying glass for first record and verify that file model opened successfully");
            assignmentDetail.ClickOnMagnifyingGlass(1,8);
            Assert.IsTrue(assignmentDetail.IsFilesModelOpened(), "File model isn't opened successfully");

            Log.Info($"Step 6: Click On 'Choose From Candidate Files' button and verify that unassigned file model opened successfully");
            assignmentDetail.ClickOnChooseCandidateFileButton();
            Assert.IsTrue(assignmentDetail.IsUnAssignedFilesModelOpened(), "Unassigned File model isn't opened successfully");

            Log.Info($"Step 7: Click On 'Assign' button for first file and click on Close button");
            var fileName = assignmentDetail.UnAssignedFilesGetDataFromNthRowNthColumn(1, 2);
            assignmentDetail.UnAssignedFilesClickOnAssignButton(1);
            assignmentDetail.UnAssignedFilesClickOnCloseButton();

            Log.Info($"Step 8: Verify that file is assigned successfully");
            Assert.IsTrue(assignmentDetail.FilesIsFilePresent(fileName), "File isn't assigned");
        }

        [TestMethod]
        public void VerifyThatFileAssignedToSpecificationSuccessfullyViaUploadFileOption()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);
            var assignmentDetail = new AssignmentDetailPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info($"Step 3: Navigate to Assignment page and search Id");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToAssignSearchTextBoxAndHitEnter(AssignmentData.Assign);

            Log.Info($"Step 4: Click On Assign#");
            assignment.ClickOnNthRowNthColumn(1, 1);

            Log.Info($"Step 5: On results grid, click on magnifying glass for first record ");
            assignmentDetail.SwitchToIFrame();
            assignmentDetail.ClickOnMagnifyingGlass(1,8);

            Log.Info($"Step 6: Click On 'Browse' button, select file and click on 'Upload File' button");
            var path = new FileUtil().GetBasePath() + "/TestData/Core/sample.pdf";
            assignmentDetail.FilesChooseFileAndUploadFile(path);

            Log.Info($"Step 8: Verify that file is assigned successfully");
            var splitedPath = path.Split('\\');
            var expectedFileName = splitedPath[splitedPath.Length-1].Replace("/TestData/Core/", "");
            var actualFile = assignmentDetail.FilesGetDataFromNthRowNthColumn(1, 4);
            Assert.AreEqual(expectedFileName, actualFile, "File is not assigned ");
        }

        [TestMethod]
        public void VerifyThatFileRemovalFromSpecificationSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);
            var assignmentDetail = new AssignmentDetailPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info($"Step 3: Navigate to Assignment page and search Id");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToAssignSearchTextBoxAndHitEnter(AssignmentData.Assign);

            Log.Info($"Step 4: Click On Assign#");
            assignment.ClickOnNthRowNthColumn(1, 1);
            assignmentDetail.WaitUntilAssignmentProcessingInvisible();

            Log.Info($"Step 5: Click on magnifying glass for first record. Removing all files and adding a new file.");
            assignmentDetail.SwitchToIFrame();
            assignmentDetail.ClickOnMagnifyingGlass(2,8);
            assignmentDetail.FileRemoveAllExistingFiles();
            assignmentDetail.ClickOnChooseCandidateFileButton();
            assignmentDetail.UnAssignedFilesClickOnAssignButton(1);
            assignmentDetail.UnAssignedFilesClickOnCloseButton();

            Log.Info($"Step 6: Click on 'remove' button of first file and verify file gets removed");
            var removedFileName = assignmentDetail.FilesGetDataFromNthRowNthColumn(1, 3);
            assignmentDetail.ClickOnRemoveFileButtonAndAcceptAlert(1);
            Assert.IsFalse(assignmentDetail.FilesIsFilePresent(removedFileName), "File is not removed");

        }

        [TestMethod]
        public void VerifyThatFileAssignedToSpecificationCanBeActiveInActiveSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);
            var assignmentDetail = new AssignmentDetailPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info($"Step 3: Navigate to Assignment page and search Id");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToAssignSearchTextBoxAndHitEnter(AssignmentData.Assign);

            Log.Info($"Step 4: Click On Assign#");
            assignment.ClickOnNthRowNthColumn(1, 1);

            Log.Info($"Step 5: Click on magnifying glass for first record. Removing all files and adding a new file.");
            assignmentDetail.SwitchToIFrame();
            assignmentDetail.ClickOnMagnifyingGlass(2,8);
            assignmentDetail.FileRemoveAllExistingFiles();
            assignmentDetail.ClickOnChooseCandidateFileButton();
            assignmentDetail.UnAssignedFilesClickOnAssignButton(1);
            assignmentDetail.UnAssignedFilesClickOnCloseButton();

            Log.Info($"Step 5: Click on 'InActive/Active' button and click on 'close' button ");
            assignmentDetail.ClickOnActiveInActiveButton(1);
            var expectedStatus = assignmentDetail.GetTextFromActiveInActiveButton(1);
            assignmentDetail.FilesClickOnCloseButton();

            Log.Info($"Step 6: Click on 'magnifying' button and verify InActive/Active status changed successfully");
            assignmentDetail.SwitchToIFrame();
            assignmentDetail.ClickOnMagnifyingGlass(2,8);
            var actualStatus = assignmentDetail.GetTextFromActiveInActiveButton(1);
            Assert.AreEqual(expectedStatus, actualStatus, "InActive/Active Status didn't match");
        }

        [TestMethod]
        public void VerifyThatBinocularsFileOpenedSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);
            var assignmentDetail = new AssignmentDetailPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info($"Step 3: Navigate to Assignment page and search Id");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToAssignSearchTextBoxAndHitEnter(AssignmentData.Assign);

            Log.Info($"Step 4: Click on 'first' id ");
            assignment.ClickOnNthRowNthColumn(1, 1);

            Log.Info($"Step 5: Click on 'binoculars' icon and verify PDF preview is opened with correct details");
            assignmentDetail.SwitchToIFrame();
            var expectedFileName = assignmentDetail.GetFileNameOfBinocular(1);
            assignmentDetail.ClickOnBinocularsIcon(1);
            var actualFileName = assignmentDetail.GetBinocularsHeaderText();
            var str = expectedFileName.Replace(".pdf","");
            Assert.IsTrue(assignmentDetail.IsBinocularsPopupOpened(), "File popup isn't opened");
            Assert.AreEqual(str, actualFileName, "File name doesn't match");

            Log.Info($"Step 6: Click on 'X' button and verify that PDF preview is closed");         
            assignmentDetail.BinocularsFileClickOnCloseButton();
            Assert.IsFalse(assignmentDetail.IsBinocularsPopupOpened(), "File popup isn't closed");
        }

        [TestMethod]
        public void VerifyThatFileOpenedInNewTabSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);
            var assignmentDetail = new AssignmentDetailPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info($"Step 3: Navigate to Assignment page and search Id");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToAssignSearchTextBoxAndHitEnter(AssignmentData.Assign);

            Log.Info($"Step 4: Click on 'first' id ");
            assignment.ClickOnNthRowNthColumn(1, 1);

            Log.Info($"Step 5: Click on 'file' icon and verify PDF preview is opened in new tab with correct details");
            assignmentDetail.SwitchToIFrame();
            assignmentDetail.ClickOnFileIcon(1);
            Driver.SelectWindowByIndex(1);
            var url = Driver.GetCurrentUrl();
            var startUrl = CoreUrl + "/GetFile/";
            Assert.IsTrue(url.StartsWith(startUrl), $"Url doesn't start with {startUrl}, actual url is : {url}");
        }

        [TestMethod]
        public void VerifyCsNotesAddedSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);
            var assignmentDetail = new AssignmentDetailPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info($"Step 3: Navigate to Assignment page and search Id");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToAssignSearchTextBoxAndHitEnter(AssignmentData.Assign);

            Log.Info($"Step 4: Click on 'first' id ");
            var name = assignment.GetDataFromNthRowNthColumn(1, 4);
            assignment.ClickOnNthRowNthColumn(1, 1);

            Log.Info($"Step 5: Click on 'CS Note' and verify CS note popup opened");
            assignmentDetail.ClickOnCsNoteButton(1);
            var expectedHeaderText = "CS Notes | " + name + "|";
            var actualHeardText = assignmentDetail.GetCsNotePopupHeaderText();
            Assert.AreEqual(expectedHeaderText.RemoveWhitespace(), actualHeardText.RemoveWhitespace(), "CS Notes header text doesn't match");

            Log.Info($"Step 6: Click on 'Email Notification' dropdown then select first value from list and verify value is selected successfully");
            var emailName = AssignmentData.EmailNotificationPerson;
            assignmentDetail.CsNoteSelectDropDownValue(emailName);
            var actualValue = assignmentDetail.GetCsNoteDropDownFirstSelectedValue();
            Assert.AreEqual(emailName, actualValue, "Person name is not matched");

            Log.Info($"Step 7: Add text in Note text box, click on 'Add Note' button and verify note is displayed on grid successfully");         
            var textNote = "Test" + new CSharpHelpers().GenerateRandomNumber();
            assignmentDetail.CsNoteAddTextToNoteTextArea(textNote);
            assignmentDetail.CsNoteClickOnAddNoteButton();

            var actualNote = assignmentDetail.GetCsNotesNthRowNthColumnText(1,6);
            Assert.AreEqual(textNote, actualNote, "Note doesn't match");
        }

        [TestMethod]
        public void VerifySpecificationCheckBoxSelectDeselectWorksSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);
            var assignmentDetail = new AssignmentDetailPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info($"Step 3: Navigate to Assignment page and search Id");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToAssignSearchTextBoxAndHitEnter(AssignmentData.Assign);

            Log.Info($"Step 4: Click on 'first' id ");
            assignment.ClickOnNthRowNthColumn(1, 1);

            Log.Info($"Step 5: Click on 'checkbox' and verify status is changes to select/unselect");
            assignmentDetail.SwitchToIFrame();
            var notExpectedStatus = assignmentDetail.GetSpecificationCheclBoxStatus();
            assignmentDetail.ClickOnSpecificationCheckBox(!notExpectedStatus);
            var actualStatus = assignmentDetail.GetSpecificationCheclBoxStatus();
            Assert.AreNotEqual(notExpectedStatus, actualStatus, "Checkbox status is not changed");
        }

        [TestMethod]
        public void VerifyDateCompletedUpdatedSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var assignment = new AssignmentResultGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);
            var assignmentDetail = new AssignmentDetailPo(Driver);
            var header = new HeaderPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info($"Step 3: Navigate to Assignment page and search Id");
            leftNav.ClickOnAssignmentMenu();
            resultGrid.SwitchToGrid();
            assignment.EnterDataToAssignSearchTextBoxAndHitEnter(AssignmentData.Assign);

            Log.Info($"Step 4: Click on 'first' id ");
            assignment.ClickOnNthRowNthColumn(1, 1);

            Log.Info($"Step 5: Click on 'Date Completed' input field and select today's date from calendar");
            assignmentDetail.SwitchToIFrame();
            var todayDate = DateTime.Today.ToString("yyyy-MM-dd");
            assignmentDetail.UpdateCompletedDate(todayDate);
            Driver.SwitchToDefaultIframe();
            header.EnterDataToFindTextBox("");
            assignmentDetail.SwitchToIFrame();
            var actualCompletedDate = assignmentDetail.GetCompletedDateAttribute();
            Assert.AreEqual(todayDate, actualCompletedDate, "Date is not matched");
        }
    }
}
