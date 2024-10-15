using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataObjects.Core.Facility;
using UIAutomation.PageObjects.Core.Candidate;
using UIAutomation.PageObjects.Core.Common;
using UIAutomation.PageObjects.Microsoft;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.Core
{
    [TestClass]
    [TestCategory("Core"), TestCategory("Candidate")]
    public class CandidateTests : BaseTest
    {
        private readonly Candidate CandidateData = new JsonHelpers<Candidate>().DeserializeJsonObject(new FileUtil().GetBasePath() + "/TestData/Core/candidate.json");

        [TestMethod]
        public void VerifyThatRecordsAreShownCorrectlyAsPerShowDropdownSelection()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var candidate = new CandidateResultsGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info("Step 3: Navigate to Candidate page and select Show=100, and verify that 100 results are returned");
            leftNav.ClickOnCandidateMenu();
            resultGrid.SwitchToGrid();
            candidate.SelectShowDropdownValue("100");
            Assert.AreEqual(100, candidate.GetCountOfRows(), "Result count doesn't match");

        }

        [TestMethod]
        public void VerifyThatCandidateDetailsPageOpenedSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var candidate = new CandidateResultsGridPo(Driver);
            var candidateDetails = new CandidateDetailsPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info("Step 3: Navigate to Candidate page and noting down first candidate id,name");
            leftNav.ClickOnCandidateMenu();
            resultGrid.SwitchToGrid();
            var candidateId = candidate.GetDataFromNthRowNthColumn(1, 1);
            var candidateName = candidate.GetDataFromNthRowNthColumn(1, 2);

            Log.Info("Step 4: Navigate to First Candidate and Verify Candidate details page is opened and it has correct details for Id,name");
            candidate.ClickOnNthRowNthColumn(1, 2);
            candidateDetails.SwitchToIFrame();
            var actual = candidateDetails.GetHeaderText();
            var expected = "Candidate | " + candidateName + " | " + candidateId;
            Assert.AreEqual(expected, actual, "Candidate Header info does not match");
        }

        [TestMethod]
        public void VerifyThatRecordSearchByIdIsSuccessful()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var candidate = new CandidateResultsGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var id = CandidateData.Id;

            Log.Info($"Step 3: Navigate to Candidate page and search by Id= {id}");
            leftNav.ClickOnCandidateMenu();
            resultGrid.SwitchToGrid();
            candidate.EnterDataToIdSearchTextBoxAndHitEnter(id);

            Log.Info($"Step 4: Verify that 1 result is returned having Id = {id}");
            Assert.AreEqual(1, candidate.GetCountOfRows(), "Result count doesn't match");
            Assert.AreEqual(id, candidate.GetDataFromNthRowNthColumn(1, 1), "ID doesn't match");
        }

        [TestMethod]
        public void VerifyThatRecordSearchByNameIsSuccessful()
        {

            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var candidate = new CandidateResultsGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var name = CandidateData.Name;

            Log.Info($"Step 3: Navigate to Candidate page and search by Name= {name}");
            leftNav.ClickOnCandidateMenu();
            resultGrid.SwitchToGrid();
            candidate.EnterDataToNameSearchTextBoxAndHitEnter(name);

            Log.Info($"Step 4: Verify that all results are returned having Name = {name}");
            var rowCount = candidate.GetCountOfRows();
            for (var i = 1; i <= rowCount; i++)
            {
                Assert.IsTrue(candidate.GetDataFromNthRowNthColumn(i, 2).Contains(name), "Name doesn't match");
            }
        }

        [TestMethod]
        public void VerifyThatRecordSearchByCityIsSuccessful()
        {

            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var candidate = new CandidateResultsGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);
            var city = CandidateData.City;

            Log.Info($"Step 3: Navigate to Candidate page and search by City= {city}");
            leftNav.ClickOnCandidateMenu();
            resultGrid.SwitchToGrid();
            candidate.EnterDataToCitySearchTextBoxAndHitEnter(city);

            Log.Info($"Step 4: Verify that all result is returned having City = {city}");
            var rowCount = candidate.GetCountOfRows();
            for (var i = 1; i <= rowCount; i++)
            {
                Assert.IsTrue(candidate.GetDataFromNthRowNthColumn(i, 5).Contains(city), "City doesn't match");
            }

        }

        [TestMethod]
        public void VerifyThatRecordSearchByStateIsSuccessful()
        {

            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var candidate = new CandidateResultsGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var state = CandidateData.State;

            Log.Info($"Step 3: Navigate to Candidate page and search by State= {state}");
            leftNav.ClickOnCandidateMenu();
            resultGrid.SwitchToGrid();
            candidate.EnterDataToStateSearchTextBoxAndHitEnter(state);

            Log.Info($"Step 4: Verify that all result is returned having State = {state}");
            var rowCount = candidate.GetCountOfRows();
            for (var i = 1; i <= rowCount; i++)
            {
                Assert.AreEqual(state, candidate.GetDataFromNthRowNthColumn(i, 6), "State doesn't match");
            }

        }

        [TestMethod]
        public void VerifyThatRecordSearchByRecruiterIsSuccessful()
        {

            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var candidate = new CandidateResultsGridPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var recruiter = CandidateData.Recruiter;

            Log.Info($"Step 3: Navigate to Candidate page and search by Recruiter = {recruiter}");
            leftNav.ClickOnCandidateMenu();
            resultGrid.SwitchToGrid();
            candidate.EnterDataToRecruiterSearchTextBoxAndHitEnter(recruiter);
  
            Log.Info($"Step 4: candidate that all result is returned having Recruiter = {recruiter}");
            var rowCount = candidate.GetCountOfRows();
            for (var i = 1; i <= rowCount; i++)
            {
                Assert.AreEqual(recruiter, candidate.GetDataFromNthRowNthColumn(i, 8), "Recruiter doesn't match");
            }

        }
    }
}
