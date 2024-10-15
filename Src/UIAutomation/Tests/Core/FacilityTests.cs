using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataObjects.Core.Facility;
using UIAutomation.PageObjects.Core;
using UIAutomation.PageObjects.Core.Common;
using UIAutomation.PageObjects.Microsoft;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.Core
{
    [TestClass]
    [TestCategory("Facility"), TestCategory("Core")]
    public class FacilityTests : BaseTest
    {
        private readonly Facility FacilityData = new JsonHelpers<Facility>().DeserializeJsonObject(new FileUtil().GetBasePath() + "/TestData/Core/facility.json");

        [TestMethod]
        public void VerifyThatRecordsAreShownCorrectlyAsPerShowDropdownSelection()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var facility = new FacilityPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            Log.Info("Step 3: Navigate to Facility page and select Show=100, and verify that 100 results are returned");
            leftNav.ClickOnFacilityMenu();
            resultGrid.SwitchToGrid();
            facility.SelectShowDropdownValue("100");
            Assert.AreEqual(100, facility.GetCountOfRows(), "Result count doesn't match");

            Log.Info("Step 4: Select Show=25, and verify that 25 results are returned");

            facility.SelectShowDropdownValue("25");
            Assert.AreEqual(25, facility.GetCountOfRows(), "Result count doesn't match");
        }

        [TestMethod]
        public void VerifyThatRecordSearchByIdIsSuccessful()
        {
            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var facility = new FacilityPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var id = FacilityData.Id;

            Log.Info($"Step 3: Navigate to Facility page and search by Id= {id}");
            leftNav.ClickOnFacilityMenu();
            resultGrid.SwitchToGrid();
            facility.EnterDataToIdSearchTextBoxAndHitEnter(id);

            Log.Info($"Step 4: Verify that 1 result is returned having Id = {id}");
            Assert.AreEqual(1, facility.GetCountOfRows(), "Result count doesn't match");
            Assert.AreEqual(id, facility.GetDataFromNthRowNthColumn(1, 1), "ID doesn't match");
        }

        [TestMethod]
        public void VerifyThatRecordSearchByNameIsSuccessful()
        {

            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var facility = new FacilityPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var name = FacilityData.Name;

            Log.Info($"Step 3: Navigate to Facility page and search by Name= {name}");
            leftNav.ClickOnFacilityMenu();
            resultGrid.SwitchToGrid();
            facility.EnterDataToNameSearchTextBoxAndHitEnter(name);

            Log.Info($"Step 4: Verify that all results are returned having Name = {name}");
            var rowCount = facility.GetCountOfRows();
            for (var i = 1; i <= rowCount; i++)
            {
                Assert.IsTrue(facility.GetDataFromNthRowNthColumn(i, 2).Contains(name), "Name doesn't match");
            }

        }

        [TestMethod]
        public void VerifyThatRecordSearchByCityIsSuccessful()
        {

            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var facility = new FacilityPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var city = FacilityData.City;

            Log.Info($"Step 3: Navigate to Facility page and search by City= {city}");
            leftNav.ClickOnFacilityMenu();
            resultGrid.SwitchToGrid();
            facility.EnterDataToCitySearchTextBoxAndHitEnter(city);

            Log.Info($"Step 4: Verify that all result is returned having City = {city}");
            var rowCount = facility.GetCountOfRows();
            for (var i = 1; i <= rowCount; i++)
            {
                Assert.IsTrue(facility.GetDataFromNthRowNthColumn(i, 6).Contains(city), "City doesn't match");
            }

        }

        [TestMethod]
        public void VerifyThatRecordSearchByStateIsSuccessful()
        {

            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var facility = new FacilityPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var state = FacilityData.State;

            Log.Info($"Step 3: Navigate to Facility page and search by State= {state}");
            leftNav.ClickOnFacilityMenu();
            resultGrid.SwitchToGrid();
            facility.EnterDataToStateSearchTextBoxAndHitEnter(state);

            Log.Info($"Step 4: Verify that all result is returned having State = {state}");
            var rowCount = facility.GetCountOfRows();
            for (var i = 1; i <= rowCount; i++)
            {
                Assert.AreEqual(state, facility.GetDataFromNthRowNthColumn(i, 7), "State doesn't match");
            }

        }

        [TestMethod]
        public void VerifyThatRecordSearchByZipIsSuccessful()
        {

            var microsoftLogin = new LoginPo(Driver);
            var resultGrid = new ResultGridCommonPo(Driver);
            var facility = new FacilityPo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentials.Email} ,  password : {LoginCredentials.Password}");
            microsoftLogin.LoginToApplication(LoginCredentials);

            var zip = FacilityData.Zip;

            Log.Info($"Step 3: Navigate to Facility page and search by Zip Code = {zip}");
            leftNav.ClickOnFacilityMenu();
            resultGrid.SwitchToGrid();
            facility.EnterDataToZipSearchTextBoxAndHitEnter(zip);

            Log.Info($"Step 4: Verify that all result is returned having Zip Code = {zip}");
            var rowCount = facility.GetCountOfRows();
            for (var i = 1; i <= rowCount; i++)
            {
                Assert.AreEqual(zip, facility.GetDataFromNthRowNthColumn(i, 8), "Zip Code doesn't match");
            }

        }

    }
}
