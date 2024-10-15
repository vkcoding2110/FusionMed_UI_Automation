using UIAutomation.DataObjects.FMP.Home;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMP.Home
{
    public static class ContactUsDataFactory
    {
        public static ContactUsForm EnterDetailsInContactUsForm()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new ContactUsForm
            {
                Name = "Test Testing",
                Email = "test" + randomNumber+ "@yopmail.com",
                Phone = "987654" + randomNumber.Remove(4),
                Message = "Is there a vacancy for medical staff as an assistant nurse?"
            };
        }
    }
}
