using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        IWebDriver driver = new ChromeDriver();
        ExtentReports extent = new ExtentReports();

        string dateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string reportFilePath = Path.Combine(Directory.GetCurrentDirectory(), "/Users/dishakhan/SQAT Project/Report/Report_File.html");
        ExtentSparkReporter htmlreporter = new ExtentSparkReporter(reportFilePath);

        extent.AttachReporter(htmlreporter);
        ExtentTest test = extent.CreateTest("Parabank Registration & Login Automation", "Register, logout, login test");

        try
        {
            OpenUrl(driver, test, "https://parabank.parasoft.com/parabank/index.htm");

            string csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "users.csv");
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<UserData>().ToList();
                foreach (var record in records)
                {
                    RegisterUser(driver, test, record);
                    Logout(driver, test);
                    PerformLogin(driver, test, record.Username, record.Password);
                    ValidateLogin(driver, test);
                    Logout(driver, test);
                    Thread.Sleep(1000);
                }
            }
        }
        catch (Exception ex)
        {
            test.Log(Status.Fail, $"Test failed: {ex.Message}");
            Console.WriteLine($"Error: {ex}");
        }
        finally
        {
            extent.Flush();
            driver.Quit();
        }
    }

    static void OpenUrl(IWebDriver driver, ExtentTest test, string url)
    {
        test.Log(Status.Info, "Automation Testing - Parabank Registration & Login Automation");
        driver.Navigate().GoToUrl(url);
        driver.Manage().Window.Maximize();
        test.Log(Status.Info, $"Navigated to {url}");
    }

    static void RegisterUser(IWebDriver driver, ExtentTest test, UserData data)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        driver.FindElement(By.LinkText("Register")).Click();

        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("customer.firstName")));
        driver.FindElement(By.Id("customer.firstName")).SendKeys(data.FirstName ?? string.Empty);
        driver.FindElement(By.Id("customer.lastName")).SendKeys(data.LastName);
        driver.FindElement(By.Id("customer.address.street")).SendKeys(data.Address);
        driver.FindElement(By.Id("customer.address.city")).SendKeys(data.City);
        if (!string.IsNullOrEmpty(data.State))
        {
            driver.FindElement(By.Id("customer.address.state")).SendKeys(data.State);
        }
        driver.FindElement(By.Id("customer.address.zipCode")).SendKeys(data.ZipCode);
        driver.FindElement(By.Id("customer.phoneNumber")).SendKeys(data.Phone);
        driver.FindElement(By.Id("customer.ssn")).SendKeys(data.SSN);
        driver.FindElement(By.Id("customer.username")).SendKeys(data.Username);
        driver.FindElement(By.Id("customer.password")).SendKeys(data.Password);
        driver.FindElement(By.Id("repeatedPassword")).SendKeys(data.Password);

        driver.FindElement(By.CssSelector("input[value='Register']")).Click();
        test.Log(Status.Pass, $"User '{data.Username}' registered successfully.");
    }

    static void Logout(IWebDriver driver, ExtentTest test)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("Log Out")));
            driver.FindElement(By.LinkText("Log Out")).Click();
            test.Log(Status.Info, "Logged out successfully.");
        }
        catch (NoSuchElementException)
        {
            test.Log(Status.Fail, "Logout failed: 'Log Out' link not found.");
        }
        catch (Exception ex)
        {
            test.Log(Status.Fail, $"Logout failed: {ex.Message}");
        }
    }

    static void PerformLogin(IWebDriver driver, ExtentTest test, string username, string password)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("username")));

            driver.FindElement(By.Name("username")).SendKeys(username);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.CssSelector("input[value='Log In']")).Click();
            test.Log(Status.Info, $"Attempted login for user: {username}");
        }
        catch (Exception ex)
        {
            test.Log(Status.Fail, $"Login failed for user {username}: {ex.Message}");
            throw;
        }
    }

    static void ValidateLogin(IWebDriver driver, ExtentTest test)
    {
        try
        {
            driver.FindElement(By.LinkText("Log Out"));
            test.Log(Status.Pass, "Login successful ✅");
        }
        catch (NoSuchElementException)
        {
            test.Log(Status.Fail, "Login failed ❌ - 'Log Out' link not found.");
            throw new Exception("Login validation failed.");
        }
    }
}

public class UserData
{
    public string? FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public required string City { get; set; }
    public string? State { get; set; }
    public required string ZipCode { get; set; }
    public required string Phone { get; set; }
    public required string SSN { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}