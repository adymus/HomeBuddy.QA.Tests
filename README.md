# HomeBuddy QA Engineer Assignment

This repository contains my QA Engineer take-home assignment for the [HomeBuddy Kitchen Remodeling staging application](https://hb-test.stage.sirenltd.dev/).

The assessment covers exploratory testing, bug identification and reporting, regression test design, and automated UI testing using **C#, Playwright, and NUnit**.

## 1. Manual Testing & Documentation

I explored the kitchen remodeling landing page and lead generation wizard, covering positive, negative, and edge-case scenarios.

I also inspected browser Console and Network activity to investigate application behavior and API responses.

All testing documentation is located in the `Docs` folder:

| Document | Description |
|---|---|
| `01-Exploratory-Testing.md` | Testing approach, areas explored, and findings |
| `02-Bug-List.md` | Identified issues grouped by severity |
| `03-Bug-Reports.md` | Three detailed bug reports with reproduction steps, expected and actual results |
| `04-QA-Observations-and-Business-Questions.md` | Observations and requirements requiring PO/business clarification |
| `05-Regression-Suite.md` | High-level regression coverage for the application |
| `06-Test-Cases.md` | 10 detailed manual test cases with execution results |
| `07-Automation-Selection.md` | Three automation candidates, priorities, and ROI justification |

The documentation separates confirmed defects from observations and requirements that need clarification.

## 2. Test Automation

### Technology Stack

- C# / .NET 10
- Microsoft Playwright for .NET
- NUnit
- Visual Studio 2026
- Microsoft Edge

### Automated Scenario

**TC-010 — End-to-End Lead Submission and Confirmation Accuracy**

The automated test completes the kitchen remodeling wizard, submits customer details, handles the optional phone confirmation step, and verifies the final Thank You page.

The primary assertion checks that the displayed customer name matches the submitted information.

**Related defect:** HB-005 — Incorrect customer name on the Thank You page.

### Automation Structure

| File / Folder | Purpose |
|---|---|
| `Data/LeadTestData.cs` | Loads customer and project data from the test configuration |
| `Pages/LandingPage.cs` | Opens the application and starts the estimate |
| `Pages/*Step.cs` | Page Objects containing locators and actions for individual wizard steps |
| `Pages/ThankYouPage.cs` | Retrieves the final confirmation message |
| `Setup/BaseTest.cs` | Shared Playwright/NUnit test setup |
| `Tests/LeadSubmissionTests.cs` | TC-010 execution, conditional handling, and assertions |
| `HomeBuddy.runsettings` | Browser configuration, application URL, and test parameters |

The implementation follows the **Page Object Model (POM)** pattern, keeping UI interactions separate from test assertions.

Locators are based on elements inspected in the staging environment, with preference given to the application's `data-autotest` attributes.

## 3. Setup & Execution

### Prerequisites

- Visual Studio 2026 or a compatible .NET 10 development environment
- .NET 10 SDK
- Microsoft Edge
- Internet access to the staging application

### Installation

1. Clone or download the repository.
2. Open the solution in Visual Studio.
3. Restore NuGet packages and build the solution.
4. Review `HomeBuddy.runsettings` and configure valid staging test data.

### Test Configuration

The `HomeBuddy.runsettings` file contains:

| Parameter | Purpose |
|---|---|
| `AppUrl` | Staging application URL |
| `HB_TEST_ZIP` | ZIP code accepted by the wizard |
| `HB_TEST_FULL_NAME` | Customer name used for verification |
| `HB_TEST_EMAIL` | Test email address |
| `HB_TEST_PHONE` | 10-digit US test phone number |

Use approved test contact details. The automated scenario submits a lead to the staging environment and may trigger notifications.

### Running the Test

**Visual Studio**

1. Open the solution and build the project.
2. Navigate to **Test → Configure Run Settings** and select `HomeBuddy.runsettings`.
3. Open **Test Explorer**.
4. Run `TC010_LeadSubmission_ShouldDisplayCorrectCustomerName`.

**Command Line**

From the folder containing `HomeBuddy.QA.Tests.csproj`:

```powershell
dotnet restore
dotnet build
dotnet test --settings HomeBuddy.runsettings
```

Results and failure details can be reviewed in Visual Studio Test Explorer or the terminal.

### Known Limitations

- Only TC-010 is implemented as an automated test; the remaining cases are documented for manual and future automation coverage.
- Phone confirmation is conditional and may not appear for every submission.
- The known customer-name defect may cause TC-010 to fail at its final assertion.
- Execution depends on staging availability and valid test data.

## 4. AI Usage

I used **ChatGPT** as a development assistant for documentation, C# Playwright implementation, code review, and troubleshooting.

The exploratory findings, test scenarios, application behavior, and inspected HTML elements came from my own testing. I reviewed and adjusted the generated code and documentation based on actual observations and execution results.

I remained responsible for test selection, defect investigation, expected results, and validation of the implementation.