using System;
using System.Collections.Generic;
using UIAutomation.DataObjects.FMP.Traveler.Profile;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMP.Traveler.Profile
{
    public static class DocumentsDataFactory
    {
        public static List<Document> UploadDocumentFile()
        {
            var path = new FileUtil().GetBasePath() + "/TestData/FMP/TestDocuments/";

            return new List<Document>
            {
                new Document
                {
                    FilePath = path,
                    FileName = "Certification.pdf",
                    DocumentType = "Other",
                    DocumentTypeCode= "pdf",
                    DocumentUploadedDate = DateTime.UtcNow
                },
                new Document
                {
                    FilePath = path,
                    FileName = "Resume.pdf",
                    DocumentType = "Resume",
                    DocumentTypeCode= "pdf",
                    DocumentUploadedDate = DateTime.UtcNow
                }
            };
        }

        public static Document EditDocumentFile()
        {
            return new Document
            {
                FileName = "Testing",
                DocumentTypeCode = ".pdf",
                DocumentType = "Skills",
                DocumentUploadedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.Now.AddMonths(3)
            };
        }
    }
}
