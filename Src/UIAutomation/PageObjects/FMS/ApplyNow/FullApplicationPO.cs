using System;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMS.ApplyNow;
using UIAutomation.Enum;
using UIAutomation.PageObjects.Components;
using UIAutomation.PageObjects.Mobile;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMS.ApplyNow
{
    internal class FullApplicationPo : FmsBasePo
    {
        public FullApplicationPo(IWebDriver driver) : base(driver)
        {

        }


        private readonly By FullAppFirstName = By.CssSelector("input#input_6");
        private readonly By FullAppMiddleName = By.CssSelector("input#input_7");
        private readonly By FullAppLastName = By.CssSelector("input#input_8");
        private readonly By FullAppSelectBirthdayDateDiv = By.XPath("//label[contains(text(),'Birthday*')]//parent::div");
        private readonly By FullAppSelectCategory = By.CssSelector("select#input_3");
        private readonly By FullAppSelectSpecialty = By.CssSelector("select#input_4");
        private readonly By FullAppCstYesRadioButton = By.XPath("//label[contains(text(),'your CST?*')]//parent::div//label[text()='Yes']/span");
        private readonly By FullAppCstNoRadioButton = By.XPath("//label[contains(text(),'your CST?*')]//parent::div//label[text()='No']/span");
        private readonly By FullAppMailingAddress = By.CssSelector("input#input_9");
        private readonly By FullAppCity = By.CssSelector("input#input_10");
        private readonly By FullAppState = By.CssSelector("select#input_11");
        private readonly By FullAppZip = By.CssSelector("input#input_12");
        private readonly By FullAppPhoneNumber = By.CssSelector("input#input_13");
        private readonly By FullAppAlterNativeNumber = By.CssSelector("input#input_15");
        private readonly By FullAppCallTime = By.CssSelector("input#input_16");
        private readonly By FullAppEmail = By.CssSelector("input#input_17");
        private readonly By FullAppReferredBy = By.XPath("//div[contains(@class,'InputGroupStyled')]/input[@id='input_19']");
        private readonly By FullAppChooseFile = By.CssSelector("input#input_18");
        private readonly By FullAppEmergencyContact = By.CssSelector("input#input_20");
        private readonly By FullAppRelationship = By.CssSelector("input#input_21");
        private readonly By FullAppEmergencyPhoneNumber = By.CssSelector("input#input_22");
        private readonly By FullAppSchoolName = By.CssSelector("input#input_28");
        private readonly By FullAppSchoolType = By.CssSelector("select#input_27");
        private readonly By FullAppSchoolCity = By.CssSelector("input#input_29");
        private readonly By FullAppSchoolState = By.CssSelector("select#input_30");
        private readonly By FullAppDegreeOrDiploma = By.CssSelector("input#input_31");
        private readonly By FullAppCertification = By.CssSelector("input#input_39");
        private readonly By FullAppLicenseState = By.CssSelector("select#input_43");
        private readonly By FullAppExperienceSpecialist = By.CssSelector("input#input_48");
        private readonly By FullAppYearsOfExperience = By.CssSelector("input#input_49");
        private readonly By FullAppDrugScreenYes = By.XPath("//label[contains(text(),'drug screen?*')]//parent::div//label[text()='Yes']/span");
        private readonly By FullAppDrugScreenNo = By.XPath("//label[contains(text(),'drug screen?*')]//parent::div//label[text()='No']/span");
        private readonly By FullAppCriminalBackgroundYes = By.XPath("//label[contains(text(),'background check?*')]//parent::div//label[text()='Yes']/span");
        private readonly By FullAppCriminalBackgroundNo = By.XPath("//label[contains(text(),'background check?*')]//parent::div//label[text()='No']/span");
        private readonly By FullAppLimitationsYes = By.XPath("//label[contains(text(),'Do you have any limitations')]//parent::div//label[text()='Yes']/span");
        private readonly By FullAppLimitationsNo = By.XPath("//label[contains(text(),'Do you have any limitations')]//parent::div//label[text()='No']/span");
        private readonly By FullAppLimitations = By.CssSelector("textarea#input_84");
        private readonly By FullAppPastEmployeeFacility = By.CssSelector("input#input_60");
        private readonly By FullAppPastEmployeeDepartment = By.CssSelector("input#input_61");
        private readonly By FullAppPastEmployeeSupervisorName = By.CssSelector("input#input_62");
        private readonly By FullAppPastEmployeeCity = By.CssSelector("input#input_65");
        private readonly By FullAppPastEmployeeState = By.CssSelector("select#input_66");
        private readonly By FullAppPastEmployeeHours = By.CssSelector("select#input_67");
        private readonly By FullAppPastEmployeePhone = By.CssSelector("input#input_68");
        private readonly By FullAppAgreeTermConditionCheckBox = By.XPath("//div[contains(text(),'I agree to the terms')]//parent::*//span");
        private readonly By FullAppSubmitButton = By.CssSelector("button#button_51");
        private readonly By SomeoneReferredMeCheckbox = By.XPath("//div[contains(@class,'FormGroup')]/div[contains(@class,'CheckboxItemStyled')]/label/input/following-sibling::span");
        
        public void AddFullApplicationData(FullApplication fullApp)
        {
            EnterFirstName(fullApp.FirstName);
            Wait.UntilElementVisible(FullAppMiddleName).EnterText(fullApp.MiddleName);
            EnterLastName(fullApp.LastName);
            EnterBirthDate(fullApp);
            SelectCategory(fullApp.Category);
            SelectSpecialty(fullApp.Speciality); 
            if (fullApp.Cst)
            {
                Wait.UntilElementClickable(FullAppCstYesRadioButton).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(FullAppCstNoRadioButton).ClickOn();
            }
            EnterMailingAddress(fullApp.MailingAddress);
            EnterCity(fullApp.City);
            SelectState(fullApp.State);
            EnterZipCode(fullApp.Zip);
            EnterMobilePhone(fullApp.PhoneNumber);
            Wait.UntilElementVisible(FullAppAlterNativeNumber).EnterText(fullApp.AlterNativeNumber);
            EnterBestTimeToCall(fullApp);
            EnterEmail(fullApp.Email);
            ClickOnSomeoneReferredMe(fullApp.SomeoneReferredMe);
            EnterReferredByText(fullApp.ReferredBy);
            switch (BaseTest.PlatformName)
            {
                case PlatformName.Web:
                    Wait.UntilElementClickable(FullAppChooseFile).SendKeys(fullApp.FilePath);
                    break;
                case PlatformName.Android:
                    Driver.JavaScriptClickOn(FullAppChooseFile);
                    new MobileFileSelectionPo(Driver).SelectFileFromDevice(fullApp.FilePath);
                    break;
                case PlatformName.Ios:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            EnterEmergencyContact(fullApp.EmergencyContact);
            EnterRelationShip(fullApp.Relationship);
            EnterEmergencyContactNumber(fullApp.EmergencyPhoneNumber);
            Wait.UntilElementVisible(FullAppSchoolName).EnterText(fullApp.School);
            Wait.UntilElementClickable(FullAppSchoolType).SelectDropdownValueByText(fullApp.SchoolType);
            Wait.UntilElementVisible(FullAppSchoolCity).EnterText(fullApp.SchoolCity);
            Wait.UntilElementClickable(FullAppSchoolState).SelectDropdownValueByText(fullApp.SchoolState);
            Wait.UntilElementVisible(FullAppDegreeOrDiploma).EnterText(fullApp.DegreeOrDiploma);
            //Commenting out non mandatory date fields as test exceeds 15 min
            //datePicker.SelectDate(fullApp.DateGraduated, FullApp_DateGraduate);
            Wait.UntilElementVisible(FullAppCertification).EnterText(fullApp.Certification);
            //datePicker.SelectDate(fullApp.CertificationExpirationDate, FullApp_CertificateExpirationDate);
            Wait.UntilElementClickable(FullAppLicenseState).SelectDropdownValueByText(fullApp.LicenseState);
            //datePicker.SelectDate(fullApp.LicenseIssueDate, FullApp_LicenseIssueDate);
            //datePicker.SelectDate(fullApp.LicenseExpirationDate, FullApp_LicenseExpirationDate);
            Wait.UntilElementVisible(FullAppExperienceSpecialist).EnterText(fullApp.ExperiencesSpeciality);
            Wait.UntilElementVisible(FullAppYearsOfExperience).EnterText(fullApp.ExperiencesYearsOfExperience);
            SelectDrugScreenTest(fullApp);
            SelectCriminalBackgroundCheck(fullApp);
            SelectLimitation(fullApp);
            Wait.UntilElementVisible(FullAppPastEmployeeFacility).EnterText(fullApp.PastEmployeeFacility);
            Wait.UntilElementVisible(FullAppPastEmployeeDepartment).EnterText(fullApp.PastEmployeeDepartment);
            Wait.UntilElementVisible(FullAppPastEmployeeSupervisorName).EnterText(fullApp.PastEmployeeSupervisorName);
            //datePicker.SelectDate(fullApp.PastEmployeeStartDate, FullApp_PastEmployeeStartDate);
            //datePicker.SelectDate(fullApp.PastEmployeeEndDate, FullApp_PastEmployeeEndDate);
            Wait.UntilElementVisible(FullAppPastEmployeeCity).EnterText(fullApp.PastEmployeeCity);
            Wait.UntilElementClickable(FullAppPastEmployeeState).SelectDropdownValueByText(fullApp.PastEmployeeState);
            Wait.UntilElementClickable(FullAppPastEmployeeHours).SelectDropdownValueByText(fullApp.PastEmployeeHours);
            Wait.UntilElementVisible(FullAppPastEmployeePhone).EnterText(fullApp.PastEmployeePhone);
            SelectTermsAndCondition();
            //datePicker.SelectDate(fullApp.GeneralDate, FullApp_Date);
        }

        public void ClickOnSubmitButton()
        {
            Wait.UntilElementClickable(FullAppSubmitButton).ClickOn();
            Wait.WaitTillElementCountIsLessThan(FullAppSubmitButton, 1);
        }

        public void WaitUntilFirstNameDisplayed()
        {
            Wait.UntilElementVisible(FullAppFirstName, 10);
        }

        public void EnterFirstName(string firstname)
        {
            Wait.UntilElementVisible(FullAppFirstName).EnterText(firstname, true);
        }

        public void EnterLastName(string lastname)
        {
            Wait.UntilElementVisible(FullAppLastName).EnterText(lastname, true);
        }

        public void EnterBirthDate(FullApplication fullApp)
        {
            var datePicker = new DatePickerPo(Driver);
            datePicker.SelectDate(fullApp.BirthDay, FullAppSelectBirthdayDateDiv);
        }
        public void SelectCategory(string category)
        {
            Wait.UntilElementVisible(FullAppSelectCategory).SelectDropdownValueByText(category);
        }

        public void SelectSpecialty(string specialty)
        {
            Wait.UntilElementVisible(FullAppSelectSpecialty).SelectDropdownValueByText(specialty);
        }

        public void ClearCategory()
        {
            Wait.UntilElementVisible(FullAppSelectCategory).SelectDropdownValueByIndex(0);
        }

        public void EnterMailingAddress(string mailingAddress)
        {
            Wait.UntilElementVisible(FullAppMailingAddress).EnterText(mailingAddress, true);
        }

        public void EnterCity(string city)
        {
            Wait.UntilElementVisible(FullAppCity).EnterText(city, true);
        }

        public void SelectState(string state)
        {
            Driver.JavaScriptScrollToElement(Wait.UntilElementExists(FullAppCity));
            Wait.UntilElementVisible(FullAppState).SelectDropdownValueByText(state);
        }

        public void ClearState()
        {
            Wait.UntilElementVisible(FullAppState).SelectDropdownValueByIndex(0);
        }

        public void EnterZipCode(string zip)
        {
            Wait.UntilElementVisible(FullAppZip).EnterText(zip, true);
        }

        public void EnterMobilePhone(string mobilePhone)
        {
            Wait.UntilElementVisible(FullAppPhoneNumber).EnterText(mobilePhone, true);
        }

        public void EnterBestTimeToCall(FullApplication fullApp)
        {
            var callTime = fullApp.CallTime.ToString("HH:mm");
            Wait.UntilElementVisible(FullAppCallTime).EnterText(callTime, true);
        }

        public void ClearBestTimeToCall(string callTime)
        {
            Wait.UntilElementVisible(FullAppCallTime).EnterText(callTime, true);
        }

        public void EnterEmail(string email)
        {
            Wait.UntilElementVisible(FullAppEmail).EnterText(email, true);
        }

        public void EnterEmergencyContact(string emergencyContact)
        {
            Wait.UntilElementVisible(FullAppEmergencyContact).EnterText(emergencyContact, true);
        }

        public void EnterRelationShip(string relationShip)
        {
            Wait.UntilElementVisible(FullAppRelationship).EnterText(relationShip, true);
        }

        public void EnterEmergencyContactNumber(string emergencyContactNumber)
        {
            Wait.UntilElementVisible(FullAppEmergencyPhoneNumber).EnterText(emergencyContactNumber, true);
        }

        public void EnterReferredByText(string referredBy)
        {
            Wait.UntilElementVisible(FullAppReferredBy).EnterText(referredBy,true);
        }

        public bool IsReferredByFieldDisplayed()
        {
            return Wait.IsElementPresent(FullAppReferredBy,5);
        }
        public void SelectDrugScreenTest(FullApplication fullApp)
        {
            if (fullApp.DrugScreen)
            {
                Wait.UntilElementClickable(FullAppDrugScreenYes).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(FullAppDrugScreenNo).ClickOn();
            }
        }

        public void SelectCriminalBackgroundCheck(FullApplication fullApp)
        {
            if (fullApp.CriminalBackground)
            {
                Wait.UntilElementClickable(FullAppCriminalBackgroundYes).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(FullAppCriminalBackgroundNo).ClickOn();
            }
        }

        public void SelectLimitation(FullApplication fullApp)
        {
            if (fullApp.Limitations)
            {
                Wait.UntilElementClickable(FullAppLimitationsYes).ClickOn();
                Wait.UntilElementVisible(FullAppLimitations).EnterText(fullApp.LimitationList);
            }
            else
            {
                Wait.UntilElementClickable(FullAppLimitationsNo).ClickOn();
            }
        }
        public void SelectTermsAndCondition()
        {
            Wait.UntilElementVisible(FullAppAgreeTermConditionCheckBox).ClickOn();
        }

        public void NavigateToPage()
        {
            NavigateToUrl($"{BaseTest.FmsUrl}apply/full-application/");
            WaitUntilMpPageLoadingIndicatorInvisible();
        }
        public void ClickOnSomeoneReferredMe(bool select)
        {
            Wait.UntilElementExists(SomeoneReferredMeCheckbox).Check(select);
        }
        public bool IsReferredByCheckboxSelected()
        {
            return Wait.UntilElementExists(SomeoneReferredMeCheckbox).IsElementSelected();
        }
    }
}


