# HomeBuddy — Manual Test Cases

**Project:** HomeBuddy — Kitchen Remodeling  
**Environment:** https://hb-test.stage.sirenltd.dev/  
**Platform:** Web Application  
**Test Type:** Manual Functional / Regression Testing  
**Testing Period:** November 2026  
**Test Cases:** 10

---

## 1. Overview

This document contains ten detailed manual test cases selected from the proposed HomeBuddy regression suite.

The selected scenarios cover the landing page, ZIP code validation, contractor availability, wizard navigation, customer information, and the complete lead-generation journey.

The cases were prioritized based on business impact, repeatability, and issues observed during exploratory testing.

### Test Case Summary

| ID | Test Case | Priority | Exploratory Outcome |
|---|---|---|---|
| TC-001 | Terms of Use navigation | High | Fail |
| TC-002 | ZIP code input validation | High | Partial Fail |
| TC-003 | No-contractor fallback and email validation | High | Pass |
| TC-004 | Photo Gallery navigation | Medium | Requires Retest |
| TC-005 | Kitchen-element selection and Next validation | High | Pass |
| TC-006 | Wizard exit, return, and cancellation | High | Pass |
| TC-007 | Kitchen Size — Not sure progression | High | Fail |
| TC-008 | Budget — Not sure progression | High | Fail |
| TC-009 | Full Name and Email validation | High | Pass |
| TC-010 | End-to-end lead submission and Thank-you accuracy | High | Fail |

**Result classification:** The outcomes summarize observations from the exploratory testing sessions. They are not results from a separately executed formal regression cycle.

### Test Data

| Variable | Description |
|---|---|
| ELIGIBLE_ZIP | Valid ZIP code currently associated with available contractors |
| NO_MATCH_ZIP | Valid ZIP code without available contractors |
| TEST_NAME | Approved synthetic full name, with a known first name |
| TEST_EMAIL | Approved test email address |
| TEST_PHONE | Approved test phone number |

Contractor availability may change between test runs.

Test data for full submissions should be approved for staging to avoid unintended communications with real customers or contractors.

---

# TC-001 — Verify Terms of Use Navigation

| Field | Value |
|---|---|
| Module | Landing Page — Footer |
| Priority | High |
| Type | Functional / Navigation |
| Related Bug | HB-006 |
| Exploratory Outcome | Fail |

### Objective

Verify that the Terms of Use link navigates to the appropriate Terms of Use document rather than the Privacy Policy.

### Preconditions

The landing page is accessible.

### Steps and Expected Results

| Step | Test Action | Expected Result |
|---|---|---|
| 1 | Open the HomeBuddy landing page. | The landing page loads. |
| 2 | Scroll to the footer. | Terms of Use and Privacy Policy links are displayed. |
| 3 | Click Terms of Use. | The corresponding legal page opens. |
| 4 | Review the destination URL, title, and content. | The content represents Terms of Use, not the Privacy Policy. |

### Actual Exploratory Result

**Fail**

Clicking Terms of Use opened the Privacy Policy destination.

The link did not provide access to the intended Terms of Use content.

---

# TC-002 — Verify ZIP Code Input Validation

| Field | Value |
|---|---|
| Module | Landing Page — ZIP Code |
| Priority | High |
| Type | Functional / Negative / Data-Driven |
| Related Finding | ZIP required-field feedback |
| Exploratory Outcome | Partial Fail |

### Objective

Verify that ZIP code inputs correctly handle valid and invalid formats and provide appropriate validation feedback.

### Preconditions

The landing page and ZIP code entry field are visible.

### Steps

1. Open the landing page.
2. Locate the ZIP code input.
3. Enter each test value from the table below.
4. Submit using the available Get Estimate action.
5. Verify the displayed feedback or navigation.
6. Reset the form before the next test value.

### Test Data and Expected Results

| Input | Expected Result |
|---|---|
| Empty value | Submission is prevented or required-field feedback is displayed |
| `1234` | Invalid length; progression is prevented |
| `123456` | Invalid length; progression is prevented |
| `ABCDE` | Alphabetic ZIP code is rejected |
| `10 001` | Internal whitespace is rejected if the input requires five consecutive digits |
| `02108` | Five-digit format, including leading zero, is accepted |
| ELIGIBLE_ZIP | Wizard opens when contractor availability is confirmed |
| Leading/trailing spaces around a valid ZIP | Input is handled consistently according to the accepted formatting rules |

### Expected Overall Result

Only acceptable ZIP code values proceed to availability checking.

Empty or invalid values provide understandable validation feedback or prevent submission.

### Actual Exploratory Result

**Partial Fail**

Invalid ZIP code length, alphabetic input, and internal-whitespace validation behaved as expected during exploration.

The empty ZIP submission did not produce clear visible feedback.

A leading-zero five-digit ZIP was accepted, and leading/trailing spaces around a valid ZIP were accepted during the observed session.

The precise intended whitespace-normalization rule was not established.

---

# TC-003 — Verify No-Contractor Fallback and Email Validation

| Field | Value |
|---|---|
| Module | Contractor Availability |
| Priority | High |
| Type | Functional / Positive / Negative |
| Exploratory Outcome | Pass |

### Objective

Verify the fallback flow when no contractors are available and validate the email notification form.

### Preconditions

- A valid NO_MATCH_ZIP is available.
- An approved test email address is available.

### Steps and Expected Results

| Step | Test Action | Expected Result |
|---|---|---|
| 1 | Enter NO_MATCH_ZIP. | The ZIP format is accepted. |
| 2 | Submit the ZIP code. | A no-contractors-available message is displayed. |
| 3 | Leave the notification email field empty and submit. | Required-field validation prevents submission. |
| 4 | Enter `test` and submit. | Invalid email format is rejected. |
| 5 | Correct the field using TEST_EMAIL. | The field accepts the valid email. |
| 6 | Submit the form. | Successful notification confirmation is displayed. |
| 7 | Select Try another ZIP Code. | The application returns to the landing page. |

### Expected Overall Result

Users without a contractor match can provide a valid email address and receive confirmation.

Invalid or missing email addresses are rejected.

### Actual Exploratory Result

**Pass**

The no-contractor fallback, invalid-email feedback, successful notification submission, and return navigation worked during testing.

**Additional observation:** The invalid-email error disappeared when the field was edited, even while the value remained invalid. This validation-feedback behavior is documented separately.

---

# TC-004 — Verify Photo Gallery Navigation

| Field | Value |
|---|---|
| Module | Landing Page — Photo Gallery |
| Priority | Medium |
| Type | UI / Functional |
| Related Bug | HB-008 |
| Exploratory Outcome | Requires Retest |

### Objective

Verify that users can navigate through gallery images using arrows and pagination controls.

### Preconditions

The landing page is accessible and the gallery contains multiple images.

### Steps and Expected Results

| Step | Test Action | Expected Result |
|---|---|---|
| 1 | Scroll to the Photo Gallery. | Gallery images and navigation controls are visible. |
| 2 | Click the right arrow. | The next gallery image is displayed. |
| 3 | Verify the active pagination indicator. | The indicator corresponds to the displayed image. |
| 4 | Ensure the gallery is beyond its first position, then click the left arrow. | The previous image is displayed. |
| 5 | Click a different pagination indicator. | The selected image is displayed. |
| 6 | Check navigation at the gallery boundaries. | Controls behave consistently without broken navigation. |

### Expected Overall Result

The gallery supports forward, backward, and direct navigation.

### Actual Exploratory Result

**Requires Retest**

The right arrow and pagination indicators worked.

The left arrow did not respond during the initial observed interaction.

The gallery position at the moment of the unsuccessful left-arrow interaction was not sufficiently established, so an additional test from a confirmed non-initial position is required.

---

# TC-005 — Verify Kitchen-Element Selection and Next Button Behavior

| Field | Value |
|---|---|
| Module | Wizard — Kitchen Elements |
| Priority | High |
| Type | Functional / Positive / Negative |
| Exploratory Outcome | Pass |

### Objective

Verify that kitchen remodeling elements support multiple selections and that Next is enabled only when at least one option is selected.

### Preconditions

The wizard is opened using ELIGIBLE_ZIP.

### Steps and Expected Results

| Step | Test Action | Expected Result |
|---|---|---|
| 1 | Open "Which elements of the kitchen would you like to update?" | All eight remodeling options are visible. |
| 2 | Leave every option unselected. | Next is disabled. |
| 3 | Select Kitchen cabinets. | The selection is highlighted and Next becomes enabled. |
| 4 | Select Flooring. | Both options remain selected. |
| 5 | Deselect Kitchen cabinets. | Flooring remains selected and Next stays enabled. |
| 6 | Deselect Flooring. | No options remain selected and Next becomes disabled. |
| 7 | Select all eight options. | All selections are accepted and highlighted. |
| 8 | Deselect all options. | Next becomes disabled after the final option is removed. |

### Expected Overall Result

The wizard accepts multiple kitchen-element selections and prevents progression when none are selected.

### Actual Exploratory Result

**Pass**

Single selection, multiple selection, selection removal, highlighting, and Next button state changes worked as expected.

---

# TC-006 — Verify Wizard Exit, Return to Project, and Cancellation

| Field | Value |
|---|---|
| Module | Wizard — Navigation & State |
| Priority | High |
| Type | Functional / Navigation |
| Exploratory Outcome | Pass |

### Objective

Verify that the wizard exit confirmation allows users to resume their project or cancel it and return to the landing page.

### Preconditions

The wizard is accessible using ELIGIBLE_ZIP.

### Steps and Expected Results

| Step | Test Action | Expected Result |
|---|---|---|
| 1 | Start the wizard using ELIGIBLE_ZIP. | The Kitchen Elements question opens. |
| 2 | Select Kitchen cabinets. | The option is selected and Next becomes enabled. |
| 3 | Click the X close icon. | A project cancellation confirmation screen appears. |
| 4 | Click Return to project. | The user returns to the wizard. |
| 5 | Verify the previously selected option. | Kitchen cabinets remains selected. |
| 6 | Click the X close icon again. | The cancellation confirmation screen reappears. |
| 7 | Click Cancel project. | The project flow closes. |
| 8 | Verify the destination. | The main landing page is displayed. |

### Expected Overall Result

The user can leave the cancellation confirmation without losing the active wizard context or cancel the project and return to the landing page.

### Actual Exploratory Result

**Pass**

The cancellation confirmation displayed both actions.

Return to project restored the wizard, and Cancel project returned to the landing page.

The selected-answer state after resuming was not separately isolated in the original evidence; it remains an explicit assertion for formal regression execution.

---

# TC-007 — Verify Kitchen Size "Not sure" Progression

| Field | Value |
|---|---|
| Module | Wizard — Kitchen Size |
| Priority | High |
| Type | Functional / Negative |
| Related Bug | HB-001 |
| Exploratory Outcome | Fail |

### Objective

Verify that selecting Not sure allows progression without a numeric kitchen measurement.

### Preconditions

The wizard has reached "What is the approximate size of your kitchen in square feet?" using a supported project path.

### Test Data

- Kitchen size: Empty
- Not sure: Selected

### Steps and Expected Results

| Step | Test Action | Expected Result |
|---|---|---|
| 1 | Open the Kitchen Size question. | Numeric input and Not sure checkbox are visible. |
| 2 | Leave the numeric field empty. | Next remains disabled when no answer is provided. |
| 3 | Select Not sure. | Next becomes enabled. |
| 4 | Click Next. | The wizard advances to the Budget step. |
| 5 | Inspect the Network request. | The unknown-size answer is sent and handled successfully. |

### Expected Overall Result

The user can continue without providing a numerical kitchen size when Not sure is selected.

### Actual Exploratory Result

**Fail**

Next became enabled, but the wizard did not advance.

The lead-update request returned HTTP `200 OK` and contained:

- `squareFeet: null`
- `notSure: "yes"`

Repeated attempts left the wizard on the Kitchen Size question.

Entering a numeric value allowed progression.

---

# TC-008 — Verify Budget "Not sure" Progression

| Field | Value |
|---|---|
| Module | Wizard — Budget |
| Priority | High |
| Type | Functional / Input Selection |
| Related Bug | HB-002 |
| Exploratory Outcome | Fail |

### Objective

Verify that Not sure is accepted as an alternative to the predefined budget ranges.

### Preconditions

The wizard has reached the Budget question.

### Test Data

- Budget range: None selected
- Not sure: Selected

### Steps and Expected Results

| Step | Test Action | Expected Result |
|---|---|---|
| 1 | Open "Do you know what your approximate budget is?" | Four budget ranges and Not sure are visible. |
| 2 | Leave all options unselected. | Next remains disabled. |
| 3 | Select Not sure. | The option is selected. |
| 4 | Observe the Next button. | Next becomes enabled. |
| 5 | Click Next. | The wizard advances to the next contact-information step. |

### Expected Overall Result

The user can continue without selecting a specific budget when Not sure is chosen.

### Actual Exploratory Result

**Fail**

Selecting Not sure did not enable Next.

The user could not continue without selecting a budget range.

---

# TC-009 — Verify Full Name and Email Validation

| Field | Value |
|---|---|
| Module | Wizard — Contact Information |
| Priority | High |
| Type | Functional / Positive / Negative |
| Exploratory Outcome | Pass |

### Objective

Verify that invalid full names and email addresses are rejected and that valid contact information allows progression.

### Preconditions

The wizard is accessible at the Full Name and Email question.

### Test Data

| Field | Invalid Value | Valid Value |
|---|---|---|
| Full Name | `ada` | TEST_NAME |
| Email | `name@website.com.` | TEST_EMAIL |

### Steps and Expected Results

| Step | Test Action | Expected Result |
|---|---|---|
| 1 | Open the contact-information question. | Full Name and Email fields are displayed. |
| 2 | Enter `ada` as the full name. | Name validation feedback is displayed when validation is triggered. |
| 3 | Enter `name@website.com.` as the email. | Email validation feedback is displayed when validation is triggered. |
| 4 | Click Next. | Progression is blocked while the values are invalid. |
| 5 | Replace the name with TEST_NAME. | The valid full name is accepted. |
| 6 | Replace the email with TEST_EMAIL. | The valid email is accepted. |
| 7 | Click Next. | The wizard advances to the Phone Number step. |

### Expected Overall Result

Invalid contact information cannot be submitted.

Valid Full Name and Email values allow progression.

### Actual Exploratory Result

**Pass**

The tested invalid values produced validation messages and prevented progression.

Valid input allowed navigation to the next step.

**Additional observation:** Next remained visually orange when data was invalid, even though navigation was correctly blocked.

---

# TC-010 — Verify End-to-End Lead Submission and Confirmation Accuracy

| Field | Value |
|---|---|
| Module | Lead Submission / Thank-you Page |
| Priority | High |
| Type | End-to-End / Functional / API Integration |
| Related Bugs | HB-004, HB-005 |
| Exploratory Outcome | Fail |
| Automation Candidate | Highest ROI |

### Objective

Verify that a customer can complete the kitchen remodeling request from the landing page to the Thank-you page and that the final confirmation displays the correct submitted customer information.

### Preconditions

- ELIGIBLE_ZIP is currently accepted and has available contractors.
- Approved staging contact data is available.
- The application and required APIs are accessible.
- A new lead can be submitted without contacting a real customer.

### Test Data

| Field | Value |
|---|---|
| ZIP Code | ELIGIBLE_ZIP |
| Kitchen Elements | Kitchen cabinets |
| Cabinet Work | Replace all or most cabinets |
| Property Type | Single-family home |
| Manufactured / Mobile / Modular Home | No |
| Homeowner / Authorized Person | Yes |
| Kitchen Size | Valid numeric value |
| Budget | $10K–$30K |
| Full Name | TEST_NAME |
| Email | TEST_EMAIL |
| Phone Number | TEST_PHONE |

### Steps and Expected Results

| Step | Test Action | Expected Result |
|---|---|---|
| 1 | Open the HomeBuddy landing page. | The page loads successfully. |
| 2 | Enter ELIGIBLE_ZIP and submit. | The remodeling wizard opens. |
| 3 | Select Kitchen cabinets and click Next. | The cabinet-work question opens. |
| 4 | Select Replace all or most cabinets and continue. | The wizard proceeds without a minor-repair warning. |
| 5 | Select Single-family home and continue. | The selection is accepted. |
| 6 | Select No for manufactured/mobile/modular home. | The wizard proceeds without an unsupported-property warning. |
| 7 | Select Yes for homeowner authorization. | The wizard advances. |
| 8 | Enter a valid numeric kitchen size. | The value is accepted and the Budget question opens. |
| 9 | Select the $10K–$30K budget range. | The budget is accepted. |
| 10 | Enter TEST_NAME and TEST_EMAIL. | Valid customer details are accepted. |
| 11 | Enter TEST_PHONE and select Submit my request. | Phone confirmation is displayed. |
| 12 | Confirm that the phone number is correct. | The application navigates to `/thank-you`. |
| 13 | Verify the Thank-you page message. | The displayed customer first name matches TEST_NAME. |
| 14 | Inspect the corresponding lead retrieval API response. | `fullName` matches TEST_NAME and the lead is recorded with the appropriate submitted status. |
| 15 | Compare customer and contractor information in the UI and API. | The confirmation displays information consistent with the submitted lead. |

### Expected Overall Result

A supported customer can complete the lead-generation journey and receive an accurate confirmation.

The customer name displayed on the Thank-you page must correspond to the name provided during the submission.

### Actual Exploratory Result

**Fail**

The complete wizard journey reached the Thank-you page.

A `GET /v1/public/leads/{leadId}` request returned HTTP `200 OK` and contained the correctly submitted customer name with lead status `SUBMITTED`.

However, the Thank-you page displayed:

"Thank you Andrew, your contractor QA company will call soon!"

The displayed name Andrew did not match the submitted customer.

A separate HTTP 422 lead-update failure was also observed during exploration. Its precise impact on individual stored fields was not established.

### Automation Value

This scenario represents the application's main business objective and verifies several components in a single customer journey:

- Landing-page entry.
- Contractor-matching ZIP code.
- Wizard navigation.
- Project qualification.
- Customer contact information.
- Phone confirmation.
- Lead submission.
- Thank-you page accuracy.

The key automation assertion should compare the actual customer name on the Thank-you page against the name entered during the test.

The expected result must be derived from the submitted test data, not from the currently incorrect displayed value.

An API comparison provides additional diagnostic evidence when the UI assertion fails.

---

## 2. Exploratory Outcome Summary

| Outcome | Count |
|---|---:|
| Pass | 4 |
| Fail | 4 |
| Partial Fail | 1 |
| Requires Retest | 1 |
| **Total** | **10** |

**Passed:** TC-003, TC-005, TC-006, TC-009  
**Failed:** TC-001, TC-007, TC-008, TC-010  
**Partially Failed:** TC-002  
**Requires Retest:** TC-004

These classifications reflect behavior observed during exploratory testing and do not imply a separate formal execution of every test step.

## 3. Automation Candidates

The following test cases were selected for their anticipated automation return on investment.

| Test ID | Scenario | ROI | Justification |
|---|---|---|---|
| TC-010 | End-to-end lead submission | Very High | Covers the primary revenue-generating flow and checks submitted customer information |
| TC-002 | ZIP code validation | High | Stable, repetitive, data-driven validations suited to automated execution |
| TC-007 | Kitchen Size Not sure | High | Protects a known functional regression risk and is relatively focused |

### Preferred Automation Scenario — TC-010

TC-010 is selected as the primary scenario for implementation using **C#, Playwright, and NUnit**.

The automated test should start on the landing page, enter an accepted ZIP code, complete a supported wizard path, submit approved test contact details, confirm the phone number, and verify the Thank-you page.

The main assertion should validate the displayed customer name against the submitted test name.

A failing assertion caused by an incorrect displayed name is an expected discovery outcome while the defect remains unresolved; the automated test itself should not be designed to accept the defect.

---

## 4. Conclusion

The ten selected cases provide risk-based coverage of the HomeBuddy Kitchen Remodeling application, including core navigation, field validation, contractor availability, wizard behavior, and lead submission.

Exploratory testing demonstrated that several important customer journeys work correctly, while also revealing progression blockers and inaccurate final confirmation data.

The complete lead submission scenario provides the strongest candidate for automation because it exercises the primary business workflow and verifies the accuracy of the resulting customer-facing confirmation.

Additional full-regression coverage is defined in `05-Regression-Suite.md`.

---

**End of Manual Test Cases**