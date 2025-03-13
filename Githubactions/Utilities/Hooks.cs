using System;
using System.IO;
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
        private static string screenshotsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "Screenshots");

        // Hook to initialize the browser before each scenario
        [BeforeScenario]
        public void BeforeScenario()
        {
            // Chrome options for headless mode
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--window-size=1920x1080");

            // Initialize ChromeDriver with options
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }

        // Hook to close the browser after each scenario
        [AfterScenario]
        public void AfterScenario()
        {
            // Take a screenshot if the scenario fails
            if (ScenarioContext.Current.TestError != null)
            {
                TakeScreenshot();
            }

            if (driver != null)
            {
                driver.Quit();  // Close and dispose the driver
            }
        }

        // Method to take a screenshot
        private void TakeScreenshot()
        {
            try
            {
                // Ensure the screenshots directory exists
                if (!Directory.Exists(screenshotsFolderPath))
                {
                    Directory.CreateDirectory(screenshotsFolderPath);
                }

                // Create a unique filename with timestamp
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string screenshotFilePath = Path.Combine(screenshotsFolderPath, $"screenshot_{timestamp}.png");

                // Capture the screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotFilePath);

                Console.WriteLine($"Screenshot saved to: {screenshotFilePath}");

                // You can also upload the file as part of the artifacts, but sending it by email needs an extra step in GitHub Actions
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: " + ex.Message);
            }
        }
    }
}
