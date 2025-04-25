
# 🧪 Parabank Registration & Login Automation (macOS Friendly)

This is a Selenium automation project for testing **user registration** and **login** on [Parabank](https://parabank.parasoft.com/parabank/index.htm).  
It uses **C# (.NET)**, **Selenium WebDriver**, **ExtentReports**, and **CsvHelper** to read test data from a CSV file.

---

## ✅ Features

- Register users using data from a CSV file
- Perform login/logout actions
- Validate successful login
- Generate a styled HTML report with test steps and statuses

---

## 🛠️ Requirements

- macOS (tested on Ventura and Sonoma)
- [.NET SDK](https://dotnet.microsoft.com/en-us/download) installed
- Google Chrome browser
- ChromeDriver (make sure it matches your Chrome version and is in your PATH)

---

## 🚀 How to Run (macOS)

**1. Clone the repository**

```bash
git clone https://github.com/your-username/Parabank-Automation.git
cd Parabank-Automation
```

**2. Restore NuGet packages**

```bash
dotnet restore
```

**3. Run the project**

```bash
dotnet run
```

📂 The HTML test report will be generated at:

```
/Users/dishakhan/SQAT Project/Report/Report_File.html
```

Open this file in your browser to view test results.

---

## 📁 Project Structure

```
Parabank-Automation/
├── users.csv               # Input file for user data
├── Report/                 # Output folder for test reports
│   └── Report_File.html    # Generated test report
├── Program.cs              # Main automation script
└── README.md               # This file
```

---

## 📸 Report Preview

Test actions are logged using **ExtentReports** with status info (Pass/Fail), user details, and screenshots (optional if implemented).

---

## 👤 Author

**Disha Khan**  
📧 Email: nlkdisha31@gmail.com.com  

---



