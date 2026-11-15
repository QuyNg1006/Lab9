# SE Lab 9 – Software Testing (xUnit + Selenium, C# .NET 8)

| Project | Exercise | What it contains |
|---|---|---|
| `BasicMath` | Ex 1 | `BasicMaths` class (Add, Subtract, Multiply, Divide) |
| `BasicMathTests` | Ex 1 + Ex 2 | Inline-data tests (EP + BVA) and CSV data-driven tests |
| `BankAccountApp` | Ex 3 | `BankAccount` class (Debit, Credit, Withdraw) |
| `BankAccountTests` | Ex 3 | Inline-data tests (EP + BVA) and CSV data-driven tests |
| `LoginAutomation` | Ex 4 | Selenium + CsvHelper login test against `login.html` |

## Run
Open `Lab9.sln` in Visual Studio 2022 -> **Test > Run All Tests** (Test Explorer), or:

```bash
dotnet test Lab9.sln
dotnet test BasicMathTests
dotnet test BankAccountTests
dotnet test LoginAutomation            # needs Google Chrome installed
HEADLESS=1 dotnet test LoginAutomation # no browser window (Windows: set HEADLESS=1)
```

HTML test report (for screenshots):
```bash
dotnet test Lab9.sln --logger "html;LogFileName=report.html"
```
Report is written to `<project>/TestResults/report.html`.

## Notes
- Target framework is `net8.0`. If you only have a newer SDK installed, change `<TargetFramework>` in each `.csproj` (e.g. `net10.0`).
- CSV files are copied to the output folder via `CopyToOutputDirectory` in each `.csproj`.
- The original handout mixed MSTest and xUnit and referenced a non-existent `Calculator`/`BankApp` namespace; this solution uses xUnit consistently.
