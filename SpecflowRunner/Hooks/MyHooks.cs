using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Utilities_UI;

namespace SpecflowRunner.Hooks
{
    [Binding]
    public class MyHooks
    {
        private ScenarioContext _scenarioContext;

        public MyHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void startup()
        {
            Utilities.InitializeDriver();
        }

        [AfterScenario]
        public void teardown()
        {
            Scenarios.Logout();
            Utilities.WebDriver.Quit();
        }
    }
}
