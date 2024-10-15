using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UIAutomation.PageObjects.Core;
using UIAutomation.PageObjects.Core.Common;
using UIAutomation.PageObjects.Microsoft;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.Core
{
    [TestClass]
    [TestCategory("Core"), TestCategory("Holiday Time")]
    public class HolidayTimeTests : BaseTest
    {      
        [TestMethod]
        public void VerifyHolidayTimePageOpenedSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var holidayTime = new HolidayTimePo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForPayroll.Email} ,  password : {LoginCredentialsForPayroll.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForPayroll);

            Log.Info("Step 3: Click on 'HolidayTime' from left menu & verify 'Holiday Time Sheet' is opened");
            leftNav.ClickOnHolidayTimeMenu();
            holidayTime.SwitchToIFrame();
            var actualHeaderText = holidayTime.GetHolidayTimeHeaderText();
            Assert.AreEqual("Holiday Time Sheet", actualHeaderText, "Header text is not matched");
            
     }

        [TestMethod]
        public void VerifyHolidayTimeDateSelectionWorkSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var holidayTime = new HolidayTimePo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForPayroll.Email} ,  password : {LoginCredentialsForPayroll.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForPayroll);

            Log.Info("Step 3: Click on 'Holiday Time' From Left Menu, select tomorrow's date & verify date selected successfully");           
            var tomorrowDateTime = DateTime.Today.AddDays(1); 
            var tomorrowDate= tomorrowDateTime.ToString("dd");           
            if (tomorrowDate.Equals("30") || tomorrowDate.Equals("31"))
            {
                tomorrowDate = "01";
                tomorrowDateTime = tomorrowDateTime.AddMonths(1);
            }

            var expectedDate = tomorrowDateTime.ToString("MM") + "/" + tomorrowDate + "/" + tomorrowDateTime.ToString("yyyy");

            leftNav.ClickOnHolidayTimeMenu();
            holidayTime.SwitchToIFrame();  
            holidayTime.SelectTomorrowDateFromCalendar();
            var actualDate = holidayTime.GetHolidayDateAttribute();
            Assert.AreEqual(expectedDate, actualDate, "Date is not matched");

        }

        [TestMethod]
        public void VerifyHolidayTimeChooseWorkSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var holidayTime = new HolidayTimePo(Driver);
            var leftNav = new LeftNavPo(Driver);

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForPayroll.Email} ,  password : {LoginCredentialsForPayroll.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForPayroll);

            Log.Info("Step 3: Click on 'Holiday Time' from left menu, select tomorrow's date from calendar");
            leftNav.ClickOnHolidayTimeMenu();
            holidayTime.SwitchToIFrame();      
            holidayTime.SelectTomorrowDateFromCalendar();

            Log.Info($"Step 4: Click On 'Choose File' button, select file and verify selected file attached successfully");
            var path = new FileUtil().GetBasePath() + "/TestData/Core/HolidayTIme.XLSX";
            holidayTime.ChooseFile(path);
            var actualFileName = holidayTime.GetSelectedFileAttribute();
            Assert.IsTrue(actualFileName.Contains("HolidayTIme.XLSX"), "File is not matched");

        }

        [TestMethod]
        public void VerifyHolidayTimeUploadFileButtonWorkSuccessfully()
        {
            var microsoftLogin = new LoginPo(Driver);
            var holidayTime = new HolidayTimePo(Driver);
            var leftNav = new LeftNavPo(Driver);
            var fileUtil = new FileUtil();

            Log.Info($"Step 1: Navigate and login to application at: {CoreUrl}");
            Driver.NavigateTo(CoreUrl);

            Log.Info($"Step 2: Login to application with credentials - Email : {LoginCredentialsForPayroll.Email} ,  password : {LoginCredentialsForPayroll.Password}");
            microsoftLogin.LoginToApplication(LoginCredentialsForPayroll);

            Log.Info("Step 3: Click on 'Holiday Time' from left menu, select tomorrow's date from 'Calendar'");
            leftNav.ClickOnHolidayTimeMenu();
            holidayTime.SwitchToIFrame();
            holidayTime.SelectTomorrowDateFromCalendar();

            Log.Info($"Step 4: Click On 'Choose File' and select 'holidayTime' file");
            var path = new FileUtil().GetBasePath() + "/TestData/Core/HolidayTIme.XLSX";
            holidayTime.ChooseFile(path);

            Log.Info($"Step 5: Delete existing file from folder, click on 'UploadFile' button and verify new file is downloaded");          
            var downloadPath = fileUtil.GetDownloadPath();
            var filename = "HolidayTIme";
            fileUtil.DeleteFileInFolder(downloadPath, filename, ".XLSX");
            holidayTime.ClickOnUploadFileButton();
            Assert.IsTrue(fileUtil.DoesFileExistInFolder(downloadPath, filename, ".XLSX", 30), "File not downloaded");
        }

    }
}
