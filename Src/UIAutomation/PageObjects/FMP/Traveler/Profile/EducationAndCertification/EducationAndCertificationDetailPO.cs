using System;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Enum;
using UIAutomation.Tests;
using UIAutomation.Utilities;

namespace UIAutomation.PageObjects.FMP.Traveler.Profile.EducationAndCertification
{
    internal class EducationAndCertificationDetailPo : FmpBasePo
    {
        public EducationAndCertificationDetailPo(IWebDriver driver) : base(driver)
        {
        }

        // Add Education or Certification Button
        private readonly By EducationAndCertificationSectionTabButton = By.XPath("//button[contains(@class,'SectionTab')]/span[text()='Education & Certifications']");
        private readonly By AddEducationOrCertificationButton = By.XPath("//button[contains(@class,'AddItemButton')]/span[text()='Add Education or Certification']");

        //Edit Button
        private readonly By EditButton = By.XPath("//button[contains(@class,'ContentEditButton')]/span[text()='edit']");

        //Verify Education Details 
        private readonly By InstitutionNameLabel = By.XPath("//div[contains(@class,'ItemHeader')]//label[contains(@class,'ItemHeaderTitleText')]");
        private readonly By GraduateDate = By.XPath("//div[contains(@class,'ItemHeaderColumn')]//label[contains(@class,'ItemHeaderTitleSubText')]");
        private readonly By FieldOfStudyLabel = By.XPath("//label[text()='Field of Study']//following-sibling::label");
        private readonly By DegreeLabel = By.XPath("//label[text()='Degree']//following-sibling::label");

        //Verify Certification Details
        private readonly By CertificationLabel = By.CssSelector("div[class*='ItemHeaderColumn'] label:nth-child(1)");
        private readonly By ExpirationDateLabel = By.CssSelector("label[class*='ExpirationText']");
        private readonly By CategorySpecialtyLabel = By.CssSelector("label[class*='ItemHeaderDescriptionText']");

        //Device elements
        private readonly By AddEducationOrCertificationButtonDevice = By.XPath("//button[contains(@class,'EducationCertificationEditButton')]/span[text()='Add Education or Certification']");
        private readonly By EditButtonDevice = By.XPath("//button[contains(@class,'ContentEditButton')]");
        private readonly By GraduateDateLabelDevice = By.XPath("//div[contains(@class,'ItemContent')]//preceding-sibling::label[contains(@class,'ItemHeaderTitleSubText')]");

        public void ClickOnEducationAndCertificationSectionTabButton()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilElementVisible(AddEducationOrCertificationButtonDevice);
                Wait.UntilElementClickable(AddEducationOrCertificationButtonDevice).ClickOn();
                new CertificationPo(Driver).WaitTillAddEducationHeaderTextDisplay();
            }
            else
            {
                Wait.UntilElementClickable(EducationAndCertificationSectionTabButton, 20).ClickOn();
                Wait.UntilElementClickable(AddEducationOrCertificationButton).ClickOn();
            }
            Wait.HardWait(1000); //To avoid test flakiness
        }

        public void ClickOnAddEducationOrCertificationButton()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilElementClickable(AddEducationOrCertificationButtonDevice).ClickOn();
            }
            else
            {
                Wait.UntilElementClickable(AddEducationOrCertificationButton).ClickOn();
            }
        }

        public void ClickOnEditButton()
        {
            if (BaseTest.PlatformName != PlatformName.Web)
            {
                Wait.UntilAllElementsLocated(EditButtonDevice).Last(e => e.Displayed).ClickOn();
            }
            else
            {
                Wait.UntilAllElementsLocated(EditButton).Last(e => e.Displayed).ClickOn();
            }
        }

        public bool IsAddEducationOrCertificationButtonDisplayed()
        {
            if (BaseTest.PlatformName == PlatformName.Web)
            {
                Wait.UntilElementClickable(EducationAndCertificationSectionTabButton).ClickOn();
            }

            return Wait.IsElementPresent(BaseTest.PlatformName == PlatformName.Web ? AddEducationOrCertificationButton : AddEducationOrCertificationButtonDevice);
        }

        //Verify Education Details 
        public Education GetEducationDetailsFromProfileDetailPage()
        {
            var institutionName = Wait.UntilAllElementsLocated(InstitutionNameLabel).Where(e => e.Displayed).Select(e => e.GetText()).Last();
            var fieldOfStudy = Wait.UntilAllElementsLocated(FieldOfStudyLabel).Where(e => e.Displayed).Select(e => e.GetText()).Last();
            var graduateDateLabel = GraduateDate;
            if (!BaseTest.PlatformName.Equals(PlatformName.Web))
            {
                graduateDateLabel = GraduateDateLabelDevice;
            }
            var graduatedDate = Wait.UntilAllElementsLocated(graduateDateLabel).Where(e => e.Displayed).Select(e => e.GetText()).Last();
            var degreeDiploma = Wait.UntilAllElementsLocated(DegreeLabel).Where(e => e.Displayed).Select(e => e.GetText()).Last();

            return new Education
            {
                InstitutionName = institutionName,
                FieldOfStudy = fieldOfStudy,
                GraduatedDate = DateTime.ParseExact(graduatedDate, "MM/yyyy", CultureInfo.InvariantCulture),
                DegreeDiploma = degreeDiploma,
            };
        }

        public bool IsEducationHeaderTextDisplayed(string headerText)
        {
            if (!Wait.IsElementPresent(InstitutionNameLabel)) return false;
            var instituteNameList = Wait.UntilAllElementsLocated(InstitutionNameLabel).Where(e => e.Displayed).ToList();
            foreach (var e in instituteNameList)
            {
                return e.GetText().Trim().Equals(headerText);
            }
            return false;
        }

        public void DeleteAllEducationOrCertification()
        {
            var eduPo = new EducationPo(Driver);
            var certPo = new CertificationPo(Driver);
            if (BaseTest.PlatformName == PlatformName.Web)
            {
                ClickOnEducationAndCertificationSectionTabButton();
            }

            var button = EditButton;
            if (!BaseTest.PlatformName.Equals(PlatformName.Web))
            {
                button = EditButtonDevice;
            }

            var allElement = Wait.UntilAllElementsLocated(button).Where(e => e.Displayed).ToList().Count;
            for (var i = 0; i < allElement; i++)
            {
                Driver.JavaScriptClickOn(Wait.UntilAllElementsLocated(button).First(e => e.Displayed));
                Wait.HardWait(2000);
                if (eduPo.IsDeleteEducationButton())
                {
                    eduPo.ClickOnDeleteEducationButton();
                }
                else
                {
                    certPo.ClickOnDeleteCertificationButton();
                }
                Driver.RefreshPage();
                Wait.HardWait(1000);
                WaitUntilFmpTextLoadingIndicatorInvisible();
                if (BaseTest.PlatformName.Equals(PlatformName.Web))
                {
                    ClickOnEducationAndCertificationSectionTabButton();
                }
            }
        }
        public Certification GetCertificationDetailsFromDetailsPage()
        {
            var certificationLabel = Wait.UntilElementVisible(CertificationLabel).GetText();
            var expirationDateLabel = Wait.UntilElementVisible(ExpirationDateLabel).GetText();
            var categoryLabel = Wait.UntilElementVisible(CategorySpecialtyLabel).GetText();

            return new Certification
            {
                CertificationFullName = certificationLabel,
                ExpirationDate = DateTime.ParseExact(expirationDateLabel, "MM/yyyy", CultureInfo.InvariantCulture),
                Category = categoryLabel,
            };
        }

        public void ScrollToEducationAndCertificationTab()
        {
            if (BaseTest.PlatformName == PlatformName.Web)
            {
                Driver.JavaScriptScrollToElement(Wait.UntilElementExists(EducationAndCertificationSectionTabButton));
            }
        }
    }
}
