# HomeBuddy — Automation Scenario Selection

**Project:** HomeBuddy — Kitchen Remodeling  
**Environment:** https://hb-test.stage.sirenltd.dev/  
**Framework:** Playwright with C# and NUnit  
**Assessment:** Part 3 — Test Automation

---

## 1. Objective

The objective is to identify the test cases with the highest return on investment (ROI) for automation from the ten manual test cases documented in `06-Test-Cases.md`.

The selection considers:

- **Business impact:** Importance of the functionality to lead generation.
- **Regression risk:** Likelihood and consequences of functionality breaking after changes.
- **Execution frequency:** Value of running the scenario repeatedly.
- **Automation effort:** Implementation complexity and maintenance cost.
- **Reliability:** Stability of test data, UI elements, and external dependencies.

## 2. Selected Automation Candidates

| Test ID | Scenario | ROI | Priority |
|---|---|---|---|
| TC-010 | End-to-End Lead Submission and Confirmation Accuracy | Very High | 1 |
| TC-002 | ZIP Code Input Validation | High | 2 |
| TC-007 | Kitchen Size — Not sure Progression | High | 3 |

### TC-010 — End-to-End Lead Submission and Confirmation Accuracy

**Selection rationale**

This scenario covers the application's primary business objective: converting a landing-page visitor into a successfully submitted remodeling lead.

The automated journey starts on the landing page, enters a valid ZIP code, completes the remodeling wizard, submits customer contact information, confirms the phone number, and reaches the Thank-you page.

The most important validation is that the displayed customer name matches the submitted name.

During exploratory testing, the Thank-you page displayed **Andrew** despite the lead retrieval API returning the correct customer name.

**Benefits**
- Covers the core business journey.
- Validates integration between multiple wizard steps.
- Detects broken navigation and submission flows.
- Verifies customer-facing confirmation accuracy.
- Provides high-value regression coverage after releases.

**Limitations**

End-to-end tests are more expensive to maintain and execute than isolated validation tests.

This scenario requires a reliable contractor-matching ZIP code, approved synthetic customer information, and controls to prevent unintended real-world communications or uncontrolled test lead creation.

**Decision:** Selected for implementation in the assessment.

### TC-002 — ZIP Code Input Validation

**Selection rationale**

ZIP code entry is the first step toward generating a customer lead.

If validation breaks, users may be unable to access the wizard or may enter invalid location information.

This scenario is suitable for data-driven automation because the same form can be checked using multiple input values.

**Benefits**
- Fast execution.
- Low automation complexity.
- Easy to repeat with multiple datasets.
- Good coverage of input validation and boundary conditions.
- Detects regressions at the beginning of the conversion funnel.

**Decision:** Selected as a high-ROI automation candidate.

### TC-007 — Kitchen Size: Not sure Progression

**Selection rationale**

Exploratory testing identified a reproducible functional issue where selecting Not sure enables the Next button, but the wizard does not advance.

This is particularly important because users requesting an initial estimate may not know their kitchen measurements.

**Benefits**
- Covers a known customer-blocking defect.
- Focused and straightforward functional assertion.
- Valuable regression protection after the defect is fixed.
- Supports both UI validation and optional API diagnostics.

**Current limitation**

The scenario currently fails due to the identified defect HB-001.

Automation can be developed to document the issue, but it should become a passing regression test only after the application behavior is corrected.

**Decision:** Selected for future regression automation.

---

## 3. Final Automation Decision

**TC-010 — End-to-End Lead Submission and Confirmation Accuracy** was selected for the assessment's automation implementation.

The test will use C#, Playwright, and NUnit within the existing `HomeBuddy.QA.Tests` project.

### Planned Automated Flow

1. Open the HomeBuddy landing page.
2. Enter a valid ZIP code with available contractors.
3. Start the Kitchen Remodeling wizard.
4. Select Kitchen cabinets.
5. Select Replace all or most cabinets.
6. Select Single-family home.
7. Select No for manufactured/mobile/modular home.
8. Confirm homeowner authorization.
9. Enter a valid numeric kitchen size.
10. Select a supported budget range.
11. Enter approved test customer name and email.
12. Enter and submit an approved phone number.
13. Confirm the phone number.
14. Verify navigation to `/thank-you`.
15. Assert that the displayed customer name matches the submitted test name.

### Expected Result

The wizard completes successfully, and the Thank-you page displays the correct customer information.

The test must assert the expected customer name rather than the incorrect value currently displayed by the application.

### Failure Investigation

If the test fails, Playwright screenshots, traces, and relevant Network/Console information can assist in determining whether the failure originated from:

- Wizard navigation or validation.
- A failed API request.
- Incorrectly persisted customer information.
- Incorrect data displayed on the Thank-you page.

The final test implementation will use locators verified against actual staging components.

---

## 4. Conclusion

The selected scenarios provide a balance between business-critical end-to-end coverage, efficient validation testing, and regression protection for a confirmed functional defect.

TC-010 was prioritized for implementation because it covers the complete lead-generation journey and validates the accuracy of the final customer confirmation.

TC-002 and TC-007 remain strong candidates for expanding the automated regression suite.

---

**End of Automation Scenario Selection**