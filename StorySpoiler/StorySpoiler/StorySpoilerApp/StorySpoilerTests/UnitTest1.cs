using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace StorySpoilerTests
{
    public class Tests
    {
        private readonly static string BaseUrl = "https://d24hkho2ozf732.cloudfront.net/";
        private WebDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddArgument("--disable-search-engine-choice-screen");

            driver = new ChromeDriver(chromeOptions);
            driver.Navigate().GoToUrl(BaseUrl); 
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            var loginButton = driver.FindElement(By.CssSelector("a.nav-link[href='/User/Login']"));
            loginButton.Click();

            driver.FindElement(By.Id("username")).SendKeys("test-user");
            driver.FindElement(By.Id("password")).SendKeys("123456");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
        [Test, Order(1)]
        public void CreateSpoilerWithInvalidDataTest()
        {
            driver.FindElement(By.CssSelector("a.nav-link[href='/Story/Add']")).Click();

            var titleInput = driver.FindElement(By.Id("title"));
            titleInput.SendKeys(""); 

            var descriptionInput = driver.FindElement(By.Id("description"));
            descriptionInput.SendKeys(""); 

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.That(driver.Url, Is.EqualTo($"{BaseUrl}Story/Add"), "The user should remain on the same page after invalid submission.");

            var mainErrorMessage = driver.FindElement(By.CssSelector(".validation-summary-errors ul li"));
            Assert.That(mainErrorMessage.Text, Contains.Substring("Unable to add this spoiler!"), "Expected main error message for form submission is not displayed.");

            var titleErrorMessage = driver.FindElement(By.CssSelector(".field-validation-error[data-valmsg-for='Title']"));
            Assert.That(titleErrorMessage.Text, Contains.Substring("The Title field is required."), "Expected error message for empty title is not displayed.");

            var descriptionErrorMessage = driver.FindElement(By.CssSelector(".field-validation-error[data-valmsg-for='Description']"));
            Assert.That(descriptionErrorMessage.Text, Contains.Substring("The Description field is required."), "Expected error message for empty description is not displayed.");
        }

        [Test, Order(2)]
        public void CreateRandomStorySpoilerTest()
        {
            driver.FindElement(By.CssSelector("a.nav-link[href='/Story/Add']")).Click();

            var randomTitle = GenerateRandomString(10);  
            var randomDescription = GenerateRandomString(50);

            driver.FindElement(By.Id("title")).SendKeys(randomTitle);
            driver.FindElement(By.Id("description")).SendKeys(randomDescription);

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.That(driver.Url, Is.EqualTo($"{BaseUrl}"), "User should be redirected to the home page after creating the story spoiler.");

            var spoilers = driver.FindElements(By.CssSelector("h2.display-4")); 
            var lastSpoilerTitle = spoilers.Last().Text; 

            Assert.That(lastSpoilerTitle, Is.EqualTo(randomTitle), "The newly created story spoiler is not listed on the home page or the title does not match.");
        }

        [Test, Order(3)]
        public void EditLastCreatedStorySpoilerTitleTest()
        {
            driver.Navigate().GoToUrl(BaseUrl);

            var spoilers = driver.FindElements(By.CssSelector("div.p-5"));
            var lastSpoiler = spoilers.Last();

            Actions actions = new Actions(driver);
            actions.MoveToElement(lastSpoiler).Perform();

            var editButton = lastSpoiler.FindElement(By.CssSelector("a[href*='/Story/Edit']"));
            editButton.Click();

            var updatedTitle = "UPDATED title";
            var titleInput = driver.FindElement(By.Id("title"));
            titleInput.Clear();
            titleInput.SendKeys(updatedTitle);

            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Assert.That(driver.Url, Is.EqualTo(BaseUrl), "User should be redirected to the home page after editing the story spoiler.");

            spoilers = driver.FindElements(By.CssSelector("h2.display-4"));
            var lastSpoilerTitle = spoilers.Last().Text;

            Assert.That(lastSpoilerTitle, Is.EqualTo(updatedTitle), "The edited story spoiler is not listed on the home page or the title does not match.");
        }

        [Test, Order(4)]
        public void DeleteLastCreatedStorySpoilerTest()
        {
            driver.Navigate().GoToUrl(BaseUrl);

            var spoilers = driver.FindElements(By.CssSelector("div.p-5"));
            Assert.That(spoilers.Count(), Is.AtLeast(1), "There are no story spoilers to delete.");

            var lastSpoiler = spoilers.Last();

            var lastSpoilerTitle = lastSpoiler.FindElement(By.CssSelector("h2.display-4")).Text;

            Actions actions = new Actions(driver);
            actions.ScrollToElement(lastSpoiler).Perform();

            driver.FindElement(By.XPath($"//h2[text()='{lastSpoilerTitle}']/../..//a[text()='Delete']")).Click();

            var updatedSpoilers = driver.FindElements(By.CssSelector("div.p-5"));
            Assert.That(updatedSpoilers.Count(), Is.LessThan(spoilers.Count()), "The number of story spoilers did not decrease after deletion.");

            if (updatedSpoilers.Count > 0) 
            {
                var newLastSpoilerTitle = updatedSpoilers.Last().FindElement(By.CssSelector("h2.display-4")).Text;
                Assert.That(newLastSpoilerTitle, Is.Not.EqualTo(lastSpoilerTitle), "The last story spoiler title has not changed after deletion.");
            }
        }

        [Test, Order(5)]
        public void TryToEditNonExistentStorySpoilerTest()
        {
            driver.Navigate().GoToUrl(BaseUrl);

            string invalidSpoilerId = "non-existent-id"; 
            driver.Navigate().GoToUrl($"{BaseUrl}Story/Edit?storyId={invalidSpoilerId}");

            var errorMessageElement = driver.FindElement(By.CssSelector("pre"));

            Assert.That(errorMessageElement.Text, Contains.Substring("No such spoiler!"), "The expected error message 'No such spoiler!' was not displayed.");
        }

        [Test, Order(6)]
        public void TryToDeleteNonExistentStorySpoilerTest()
        {
            driver.Navigate().GoToUrl(BaseUrl);

            string invalidSpoilerId = "non-existent-id"; 
            driver.Navigate().GoToUrl($"{BaseUrl}Story/Delete?storyId={invalidSpoilerId}");

            var errorMessageElement = driver.FindElement(By.CssSelector("pre"));

            Assert.That(errorMessageElement.Text, Contains.Substring("No such spoiler!"), "The expected error message 'No such spoiler!' was not displayed.");
        }

        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}