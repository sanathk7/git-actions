using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Githubactions.Utilities
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver driver;
        public readonly string _screenshotPath = "screenshots/";

        // Constructor to initialize WebDriver
        public Hooks()
        {
            if (driver == null)
            {
                // Set up headless browser options
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--headless");  // Run Chrome in headless mode
                options.AddArgument("--disable-gpu");  // Disable GPU acceleration in headless mode
                options.AddArgument("--window-size=1920x1080"); // Set screen resolution for headless mode
                options.AddArgument("--no-sandbox"); // Disable sandbox feature for CI environments

                // Initialize ChromeDriver with the specified options
                driver = new ChromeDriver(options);

                // Ensure the screenshots directory exists
                if (!System.IO.Directory.Exists(_screenshotPath))
                {
                    System.IO.Directory.CreateDirectory(_screenshotPath);
                }
            }
        }

        // Before Feature Hook - Initialize the browser before a feature starts
        [BeforeFeature]
        public static void StartBrowserForLogin()
        {
            // This is executed once before all scenarios in the feature
            Console.WriteLine("Executing Feature Setup...");
        }

        // Before Scenario Hook - Set up the browser before each scenario
        [BeforeScenario]
        public void BeforeScenario()
        {
            // Navigate to the desired URL for each scenario
            driver.Navigate().GoToUrl("https://yourapp.com");
            Console.WriteLine("Starting test scenario...");
        }

        // After Scenario Hook - Capture screenshot on failure and close browser
        [AfterScenario]
        public void AfterScenario()
        {
            if (driver != null && ScenarioContext.Current.TestError !=null)
            {
                TakeScreenshot();
            }
            Console.WriteLine("Scenario executed.");
        }

        // After Feature Hook - Clean up after all scenarios in the feature
        [AfterFeature]
        public static void CloseBrowser()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
                Console.WriteLine("Browser closed after feature.");
            }
        }

        // Method to capture a screenshot when a scenario fails
        private void TakeScreenshot()
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var screenshotFile = System.IO.Path.Combine(_screenshotPath, $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            screenshot.SaveAsFile(screenshotFile);
            Console.WriteLine($"Screenshot taken and saved at {screenshotFile}");
        }

        // GetDriver method to access the WebDriver if needed
        public IWebDriver GetDriver()
        {
            return driver;
        }
    }
}
