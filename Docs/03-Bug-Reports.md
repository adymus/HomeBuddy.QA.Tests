# HomeBuddy — Top 3 Bug Reports

**Project:** HomeBuddy — Kitchen Remodeling  
**Environment:** https://hb-test.stage.sirenltd.dev/  
**Test Period:** November 2026  
**Platform:** Web Application — Landing Page & Lead Generation Wizard  
**Browser:** Chromium-based browser (Microsoft Edge)  
**Testing Method:** Manual Exploratory Testing, Browser DevTools (Console / Network)

---

## Overview

This document contains three selected findings from the HomeBuddy QA assessment.

The findings were selected based on their potential business impact, available reproduction evidence, and representation of different defect categories.

| Bug ID | Title | Type | Severity |
|---|---|---|---|
| HB-001 | Kitchen Size — "Not sure" prevents wizard progression | Functional | High |
| HB-004 | Unhandled HTTP 422 during lead-update request | API / Error Handling | High (Provisional) |
| HB-005 | Incorrect customer name displayed on Thank-you page | UI / Data Display | High |

The complete inventory of identified defects is maintained separately in `02-Bug-List.md`.

---

# Bug Report 1 — Functional

## HB-001 — Kitchen Size: "Not sure" prevents wizard progression

| Field | Details |
|---|---|
| Bug ID | HB-001 |
| Module | Lead Generation Wizard — Kitchen Size |
| Type | Functional / Wizard Navigation |
| Severity | High |
| Status | Confirmed |
| Reproducibility | Reproduced on repeated attempts during testing |
| Environment | HomeBuddy Staging |
| Business Impact | Users unable to continue without providing a kitchen measurement |

### Description

Selecting **Not sure** on the Kitchen Size step enables the Next button, but clicking Next does not advance the wizard.

The application remains on the same step despite the lead-update request receiving an HTTP 200 response.

This prevents users who do not know their kitchen size from completing the intended alternative path.

### Preconditions

- The staging application is accessible.
- A valid ZIP code that opens the remodeling wizard is used.
- The preceding wizard questions have been completed.

### Steps to Reproduce

1. Start a Kitchen Remodeling estimate.
2. Complete the kitchen elements and property qualification questions.
3. Navigate to **"What is the approximate size of your kitchen in square feet?"**
4. Leave the numeric kitchen-size field empty.
5. Select **Not sure**.
6. Verify that the Next button becomes enabled.
7. Click **Next**.
8. Observe the wizard and the corresponding Network request.

### Expected Result

The wizard should accept **Not sure** as a valid answer and advance to the next step without requiring a numerical kitchen size.

### Actual Result

- Next becomes enabled after selecting Not sure.
- Clicking Next triggers a lead-update request.
- The wizard remains on the Kitchen Size step.
- No clear validation error explains why progression failed.
- Repeated clicks produce the same result.
- Entering a numeric kitchen size allows progression.

### Technical Evidence

**Request:**

`PUT /v1/public/leads/{leadId}`

**HTTP Status:**

`200 OK`

**Captured payload (relevant fields):**

```json
{
  "squareFeet": null,
  "notSure": "yes",
  "trustedFormUrl": "https://cert.trustedform.com/undefined"
}
```

The payload represents the selected Not sure option, and the request receives a successful HTTP status.

However, the application does not transition to the next step.

The captured response status alone does not establish whether the response body contained all expected application data.

### Impact Assessment

Kitchen size may not be known to customers before requesting a remodeling estimate.

Providing a Not sure option suggests that an exact measurement is optional. When this option fails, customers must either enter an arbitrary measurement or abandon the process.

This directly affects the core lead-generation journey and creates a risk of lost conversions.

**Severity justification:** High because a legitimate customer path is blocked. Critical severity could be considered if production evidence demonstrates that this failure prevents a substantial portion of users from completing the process without a viable workaround.

### Additional Observation

The submitted TrustedForm URL contains `undefined`. Its relationship to the navigation failure has not been established.

The defect is reported based on the reproducible UI behavior and supporting network evidence, without assigning an unverified root cause.

---

# Bug Report 2 — API / Error Handling

## HB-004 — Lead-update API returns HTTP 422 during wizard progression

| Field | Details |
|---|---|
| Bug ID | HB-004 |
| Module | Lead Generation Wizard — Lead Updates |
| Type | API / Error Handling |
| Severity | High (Provisional) |
| Status | Observed — reproduction and impact investigation pending |
| Reproducibility | Captured during exploratory testing; repeatability not fully established |
| Environment | HomeBuddy Staging |
| Business Impact | Potential inconsistency in saved lead information and inadequate failure handling |

### Description

During progression through the remodeling wizard, a lead-update request returned HTTP **422 Unprocessable Content**.

The browser Console reported an unhandled request failure.

Despite the failed update, the interface continued through subsequent steps during the observed session.

The API rejection itself may be valid if the request payload violates backend validation rules. The concern is that the client did not visibly explain or recover from the failure, creating uncertainty about whether the affected lead information was saved correctly.

### Preconditions

- A wizard session has been started using an accepted ZIP code.
- The preceding remodeling and property questions have been completed.
- Browser DevTools is open, with Network and Console logging enabled.

### Steps to Reproduce — Observed Test Path

1. Started a Kitchen Remodeling estimate.
2. Completed the kitchen elements and property questions.
3. Continued through the kitchen-size and budget stages.
4. Progressed toward the customer contact-information steps.
5. Monitored the lead-update requests in DevTools.
6. Observed a failed `PUT` request returning HTTP 422.
7. Inspected the Console and continued through the subsequent wizard screens.

**Reproduction limitation:** The exact input/state combination triggering the HTTP 422 has not been isolated. The steps above describe the session in which the failure was captured, rather than a verified deterministic reproduction procedure.

### Expected Result

The application should handle lead-update responses consistently.

For a valid update, the server should accept the submitted information.

For a rejected update, the application should handle the error gracefully, display appropriate feedback when necessary, and avoid presenting an inconsistent lead state.

### Actual Result

The Network panel recorded:

```text
PUT /v1/public/leads/{leadId}
422 Unprocessable Content
```

The Console displayed:

```text
Uncaught (in promise) Error:
Request failed with status code 422
```

The wizard continued through subsequent screens during the observed session.

A separate JavaScript exception was also recorded later:

```text
TypeError: Cannot read properties of undefined
(reading 'contractor')
```

The relationship between these two errors has not been established.

### Technical Evidence

One captured request included the following relevant fields:

```json
{
  "budget": "2",
  "trustedFormUrl": "https://cert.trustedform.com/undefined"
}
```

**Observed conditions:**

- Lead-update endpoint returned HTTP 422.
- The client logged an unhandled promise rejection.
- The captured payload included a TrustedForm URL containing `undefined`.
- The browser continued to display subsequent wizard screens.
- A later GET request successfully retrieved a submitted lead.

The HTTP 422 response body was not fully analyzed during the captured session. Therefore, the exact rejected field or validation rule remains unknown.

### Impact Assessment

The application is designed to collect customer information and generate contractor leads.

A failed update during this process may result in incomplete data or an inconsistent experience if the rejected information is required for matching or submission.

However, the test evidence does not establish that the submitted lead was entirely lost.

**Severity justification:** High (provisional) due to its location in a business-critical lead-update process and the unhandled error. The final severity depends on whether the rejected update affects required information, whether users can recover, and whether the failure is reproducible.

### Investigation Notes

The following remain unresolved:

- Which validation rule caused the 422 response?
- Was the rejected update caused by an invalid client payload or an incorrect server response?
- Was the associated field successfully persisted through another request?
- Was the undefined TrustedForm value expected in the staging environment?
- Does the client provide adequate handling for rejected updates?
- Is the later `contractor` exception related to the failed request?

These are investigation questions, not confirmed root causes.

---

# Bug Report 3 — UI / Data Display

## HB-005 — Thank-you page displays an incorrect customer name

| Field | Details |
|---|---|
| Bug ID | HB-005 |
| Module | Lead Generation — Thank-you Page |
| Type | UI / Data Display |
| Severity | High |
| Status | Confirmed |
| Reproducibility | Observed and verified against the API response in the tested session |
| Environment | HomeBuddy Staging |
| Business Impact | Incorrect customer-facing confirmation data |

### Description

After completing the lead-generation wizard, the Thank-you page displays a customer name that does not match the name entered during submission.

The lead retrieval API returns the correct submitted full name, but the UI displays a different name.

### Preconditions

- The wizard is accessible using an accepted ZIP code.
- The user can complete the supported remodeling flow.
- Valid customer contact information is available for the test.

### Steps to Reproduce

1. Start the Kitchen Remodeling wizard.
2. Complete the required remodeling and property questions.
3. Enter a valid full name and email address.
4. Continue to the phone-number step.
5. Enter a phone number and submit the request.
6. Confirm the phone number.
7. Wait for the Thank-you page to load.
8. Compare the displayed customer name with the submitted name.
9. Inspect the lead retrieval request in DevTools.

### Expected Result

The Thank-you page should display the name associated with the submitted lead.

If the design uses only the customer's first name, it should be derived correctly from the submitted full name.

### Actual Result

The Thank-you page displayed:

> Thank you Andrew, your contractor QA company will call soon!

The name **Andrew** did not match the full name entered during the test.

The lead retrieval API contained the correct submitted name.

### Technical Evidence

**Request:**

`GET /v1/public/leads/{leadId}`

**HTTP Status:**

`200 OK`

**Relevant response observations:**

| Field | API Response | UI |
|---|---|---|
| `fullName` | Correct submitted customer name | Displays Andrew |
| Contractor company | QA company | QA company |
| Lead status | `SUBMITTED` | Thank-you confirmation displayed |

The successful lead retrieval establishes that the submitted customer name was available in the API response.

However, the rendered thank-you message displayed another name.

### Impact Assessment

The Thank-you page is the final confirmation after customers provide personal and contact information.

Displaying a different customer's name can create uncertainty about whether the correct person's request was processed.

This is particularly concerning because the page confirms that a contractor will contact the customer.

**Severity justification:** High due to incorrect customer-facing information at the completion of a business-critical flow.

### Additional Observations

The contractor name **QA company** appeared in both the API response and the UI.

It may be legitimate staging test data and is not considered part of the confirmed name-mismatch defect.

The exact source of the displayed name Andrew was not established. Possible implementation explanations require developer investigation and are not presented as confirmed root causes.

---

## Conclusion

The three selected reports demonstrate defects across different areas of the HomeBuddy application:

| Category | Finding | Key Evidence |
|---|---|---|
| Functional | Kitchen Size Not sure blocks progression | Next enabled, HTTP 200, no navigation |
| API / Error Handling | Lead update fails with HTTP 422 | Failed PUT request and unhandled Console error |
| UI / Data Display | Thank-you page displays incorrect name | Correct API `fullName`, incorrect rendered name |

The functional and UI/data-display defects are supported by direct reproduction evidence.

The API issue is documented as an observed failure with provisional severity because its exact triggering condition and persistence impact were not fully established.

The remaining findings, including the Budget Not sure failure, incorrect Terms of Use navigation, unrelated asphalt shingles content, and Photo Gallery navigation issue, remain recorded in `02-Bug-List.md`.

Broader business-rule questions and UX recommendations are maintained separately in `04-QA-Observations-and-Business-Questions.md`.

---

**End of Top 3 Bug Reports**