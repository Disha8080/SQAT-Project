# 🧪 Parabank Registration & Login Automation (Selenium + ExtentReports + CsvHelper)

This project automates the registration, login, and logout functionalities of the [Parabank](https://parabank.parasoft.com/parabank/index.htm) demo website using **Selenium WebDriver** with **ChromeDriver**. It reads user data from a CSV file and generates an HTML report using **ExtentReports**.

---

---

## ✅ Features

- Automated browser testing with **Selenium WebDriver**
- Reads multiple test data sets from **CSV file** using CsvHelper
- Validates:
  - User Registration
  - Logout after registration
  - Login with newly registered credentials
  - Successful Logout after login
- Real-time **test reporting with ExtentReports** (HTML format)
- Uses **explicit waits** for reliable DOM interaction

---

## 🧑‍💻 Tech Stack

- C# (.NET)
- Selenium WebDriver
- ChromeDriver
- CsvHelper
- ExtentReports
- WebDriverWait (Explicit Waits)

---

## 🛠️ Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- Google Chrome
- [ChromeDriver](https://sites.google.com/chromium.org/driver/)
- NuGet packages:
  - `Selenium.WebDriver`
  - `Selenium.WebDriver.ChromeDriver`
  - `CsvHelper`
  - `ExtentReports`
  - `Selenium.Support`

---

## 📄 CSV Format

Make sure your `users.csv` file is properly formatted like below:

```csv
FirstName,LastName,Address,City,State,ZipCode,Phone,SSN,Username,Password
Jane,Doe,123 Elm Street,Springfield,IL,62704,1234567890,123-45-6789,janedoe01,password123
John,Smith,456 Oak Avenue,Centerville,CA,90210,9876543210,987-65-4321,johnsmith22,password321




