using System.Globalization;
using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LoginAutomation;

public class LoginTests
{
    public class TestData
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Expected { get; set; } = "";
    }

    public static IEnumerable<object[]> GetTestData()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "LoginTestData.csv");
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        foreach (var r in csv.GetRecords<TestData>().ToList())
            yield return new object[] { r.Username, r.Password, r.Expected };
    }

    private static IWebDriver CreateDriver()
    {
        var options = new ChromeOptions();
        // Set environment variable HEADLESS=1 to run without opening a browser window (e.g. in CI)
        if (Environment.GetEnvironmentVariable("HEADLESS") == "1")
            options.AddArgument("--headless=new");
        return new ChromeDriver(options);   // Selenium Manager downloads a matching chromedriver automatically
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Login_ShowsExpectedMessage(string username, string password, string expected)
    {
        using IWebDriver driver = CreateDriver();

        var page = Path.Combine(AppContext.BaseDirectory, "login.html");
        driver.Navigate().GoToUrl(new Uri(page).AbsoluteUri);

        driver.FindElement(By.Id("username")).SendKeys(username);
        driver.FindElement(By.Id("password")).SendKeys(password);
        driver.FindElement(By.Id("loginButton")).Click();
        Thread.Sleep(5000);

        var message = driver.FindElement(By.Id("message")).Text;
        Assert.Equal(expected, message);
    }
}
