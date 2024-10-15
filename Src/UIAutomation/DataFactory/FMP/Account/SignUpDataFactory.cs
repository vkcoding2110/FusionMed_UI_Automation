
using UIAutomation.DataObjects.FMP.Account;
using UIAutomation.Utilities;

namespace UIAutomation.DataFactory.FMP.Account
{
    public static class SignUpDataFactory
    {
        public static SignUp GetDataForSignUpForm()
        {
            var randomNumber = new CSharpHelpers().GenerateRandomNumber().ToString();
            return new SignUp
            {
                FirstName = "Test",
                LastName = "Testing",
                Email = "Test" + randomNumber + "@yopmail.com",
                Phone = "9898" + randomNumber.Remove(6),
                Password = "#Test@1234",
            };
        }
    }
}
