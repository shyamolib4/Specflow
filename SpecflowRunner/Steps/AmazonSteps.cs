using TechTalk.SpecFlow;
using Utilities_UI;

namespace SpecflowRunner.Steps
{
    [Binding]
    public class AmazonSteps
    {
        [Given(@"the user (.*) is logged in")]
        public void GivenTheUserIsLoggedIn(string username)
        {
            Utilities.InitializeDriver();
            Scenarios.Login();
        }

        [Given(@"the search term is (.*)")]
        public void GivenTheSearchTermIs(string searchTerm)
        {
            Scenarios.Search();
        }

        [When(@"(.*) button is clicked")]
        public void WhenButtonIsClicked(string button)
        {
            Scenarios.AddToCart();
        }

        [Then(@"the selected item should be added to cart")]
        public void ThenTheSelectedItemShouldBeAddedToCart()
        {
            Scenarios.Logout();
            Utilities.WebDriver.Quit();
        }
    }
}
