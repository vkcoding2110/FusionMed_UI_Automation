using System;

namespace UIAutomation.DataObjects.FMP.Traveler.Profile
{
    public class Document
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeCode { get; set; }
        public DateTime DocumentUploadedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
