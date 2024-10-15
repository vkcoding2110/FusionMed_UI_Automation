using UIAutomation.DataObjects.FMS.Home;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMS.Home
{
    public static class ContactUsDataFactory
    {
        public static ContactUsForm EnterDetailsInContactUsForm()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new ContactUsForm
            {
                Name = "Testing Test",
                LastName = "Testing",
                Email = "Test" + randomNumber + "@yopmail.com",
                Phone = "9898" + randomNumber.Remove(6),
                Message = "Is there a vacancy for medical staff as an assistant nurse?"
            };
        }
    }
}
