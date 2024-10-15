using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomation.DataFactory.FMP.Traveler.Profile;
using UIAutomation.DataObjects.Common.Account;
using UIAutomation.PageObjects.FMP.Account;
using UIAutomation.PageObjects.FMP.Header;
using UIAutomation.PageObjects.FMP.Traveler.Profile;
using UIAutomation.PageObjects.FMP.Traveler.Profile.MyDocuments;
using UIAutomation.SetUpTearDown.FMP;
using UIAutomation.Utilities;

namespace UIAutomation.Tests.FMP.Traveler.Profile.MyDocuments
{
    [TestClass]
    [TestCategory("TravelerProfile"), TestCategory("FMP")]
    public class MyDocumentsTests2 : FmpBaseTest
    {
        private static readonly Login UserLogin = GetLoginUsersByType("MyDocumentsTest");

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
            var setup = new SetUpMethods(testContext);
            setup.DeleteAllDocumentsDetails(UserLogin);
        }

        [TestMethod]
        [TestCategory("Smoke")]
        public void VerifyThatAddMoreDocumentsButtonWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var fmpHeader = new HeaderPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var myDocumentsDetails = new MyDocumentsDetailsPo(Driver);
            var addDocumentsPopUp = new AddDocumentPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            profileDetails.NavigateToPage();

            Log.Info("Step 5: Click on 'My Document' tab & click on 'Add Document' button");
            myDocumentsDetails.ClickOnMyDocumentsTabButton();

            Log.Info("Step 6: Click on 'upload documents from your device' link, Select multiple document files & verify multiple document files selected on 'Select FileType1 of Document' popup");
            var uploadDocument = DocumentsDataFactory.UploadDocumentFile();
            for (var i = 0; i < uploadDocument.Count; i++)
            {
                addDocumentsPopUp.UploadDocumentFromDevice(uploadDocument[i], i + 1);
                if (i == uploadDocument.Count - 1)
                {
                    break;
                }
                addDocumentsPopUp.ClickOnAddMoreButton();
            }

            var rowCountDroppedFile = 1;
            foreach (var fileName in uploadDocument)
            {
                var documentsPopUpDroppedFiles = addDocumentsPopUp.GetDroppedFilesFromSelectDocumentsPopUp(rowCountDroppedFile);
                Assert.AreEqual(fileName.FileName, documentsPopUpDroppedFiles.First().FileName, "File name doesn't match");
                Assert.AreEqual(fileName.DocumentType, documentsPopUpDroppedFiles.First().DocumentType, "Document type doesn't match");
                rowCountDroppedFile++;
            }
            addDocumentsPopUp.ClickOnUploadDocumentsButton();
            
            Log.Info("Step 7: Verify that multiple documents uploaded on document profile detail page successfully");
            var uploadedDocumentRowCount = 1;
            foreach (var document in uploadDocument)
            {
                var actualUploadedDocumentDetails = myDocumentsDetails.GetDocumentDetailsFromDetailPage(uploadedDocumentRowCount);
                Assert.AreEqual(document.FileName.ToLowerInvariant(), actualUploadedDocumentDetails.First().FileName.ToLowerInvariant(), "File name doesn't match");
                Assert.AreEqual(document.DocumentType.ToLowerInvariant(), actualUploadedDocumentDetails.First().DocumentType.ToLowerInvariant(), "Document type doesn't match");
                Assert.AreEqual(document.DocumentUploadedDate.ToString("MM/dd/yyyy"), actualUploadedDocumentDetails.First().DocumentUploadedDate.ToString("MM/dd/yyyy"), $"The document uploaded 'Date' is not matched Expected: {document.DocumentUploadedDate} & Actual: {actualUploadedDocumentDetails.First().DocumentUploadedDate}");
                Assert.AreEqual(document.DocumentTypeCode.ToLowerInvariant(), actualUploadedDocumentDetails.First().DocumentTypeCode.ToLowerInvariant(), "The document uploaded 'Date' is not matched");
                uploadedDocumentRowCount++;
            }

            try
            {
                myDocumentsDetails.DeleteAllDocumentsDetails();
            }
            catch
            {
                //Do nothing
            }
        }

        [TestMethod]
        [TestCategory("MobileReady")]
        public void VerifyThatAddDocumentsPopUpCloseIconWorksSuccessfully()
        {
            var fmpLogin = new FmpLoginPo(Driver);
            var fmpHeader = new HeaderPo(Driver);
            var profileMenu = new ProfileMenuPo(Driver);
            var profileDetails = new ProfileDetailPagePo(Driver);
            var myDocumentsDetails = new MyDocumentsDetailsPo(Driver);
            var addDocumentsPopUp = new AddDocumentPo(Driver);

            Log.Info($"Step 1: Navigate to application at: {FusionMarketPlaceUrl}");
            Driver.NavigateTo(FusionMarketPlaceUrl);

            Log.Info("Step 2: Click on 'Log In' button");
            fmpHeader.ClickOnLogInButton();

            Log.Info($"Step 3: Login to application with credentials - Email:{UserLogin.Email}, password:{UserLogin.Password}");
            fmpLogin.LoginToApplication(UserLogin);
            fmpLogin.WaitUntilFmpTextLoadingIndicatorInvisible();

            Log.Info("Step 4: Navigate to Profile Details page");
            fmpHeader.ClickOnProfileIcon();
            profileMenu.ClickOnProfileArrow();
            profileDetails.WaitUntilFmpPageLoadingIndicatorInvisible();

            Log.Info("Step 5: Click on 'My Document' tab & click on 'Add Document' button");
            myDocumentsDetails.ClickOnMyDocumentsTabButton();

            Log.Info("Step 6: Click on 'Close' icon & verify popup gets close");
            addDocumentsPopUp.ClickOnAddDocumentPopUpCloseIcon();
            Assert.IsFalse(addDocumentsPopUp.IsAddDocumentsPopUpOpened(), "The 'Add Document' popup is still open");
        }
    }
}