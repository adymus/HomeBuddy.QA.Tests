# HomeBuddy — Exploratory Testing Report

**Project:** HomeBuddy — Kitchen Remodeling Lead Generation  
**Environment:** [HomeBuddy Staging](https://hb-test.stage.sirenltd.dev/)  
**Test Date:** November 2026  
**Platform:** Web application — Landing Page & Multi-Step Wizard  
**Testing Type:** Manual Exploratory Testing  
**Browser:** Microsoft Edge (Chromium)  
**Operating System:** Windows  
**Additional Tools:** Browser DevTools (Console, Network, Elements), Mobile Device Emulation

---

## 1. Objective

The purpose of this exploratory testing session was to evaluate the HomeBuddy Kitchen Remodeling application from both a functional and user-experience perspective.

The testing covered the main landing page, ZIP code validation, contractor availability, and the complete lead-generation wizard, including its conditional questions and final submission process.

The main objectives were to:

- Verify that users can navigate through the application and successfully submit a remodeling request.
- Explore positive, negative, and alternative user flows.
- Identify functional, UI/UX, validation, and API-related issues.
- Evaluate the clarity and consistency of the user experience.
- Inspect browser console errors and network requests when unexpected behavior occurs.
- Identify business-rule inconsistencies that require clarification from the Product Owner.

**Documentation approach:** This report records the exploratory testing activities, observed behavior, and questions raised during testing. Confirmed defects and severity classifications are maintained separately in `02-Bug-List.md`, while the three selected detailed bug reports are maintained in `03-Bug-Reports.md`.

## 2. Testing Approach

Testing was performed manually using a risk-based exploratory approach, focusing on functionality that could affect user progression, qualification, and lead submission.

The application was tested using:

| Technique | Description |
|---|---|
| Positive testing | Valid ZIP code, valid user details, eligible property, successful submission |
| Negative testing | Invalid email, invalid name, alphabetic input in numeric fields, missing required selections |
| Boundary testing | Numeric kitchen-size values, empty fields, single and multiple option selections |
| Conditional-flow testing | Cabinet requirements, property eligibility, homeowner authorization, budget warnings |
| Navigation testing | Next buttons, wizard exit confirmation, return to project, landing-page links |
| UI/UX review | Layout, misleading content, repeated controls, error feedback, button states |
| API investigation | HTTP status codes, request payloads, responses, and browser console exceptions |
| Responsive review | Desktop layout and selected mobile-emulated layouts |

Testing was performed on the staging environment. Backend behavior was investigated through browser DevTools where relevant.

This was exploratory testing rather than an exhaustive cross-browser, performance, security, or accessibility audit.

---

## 3. Landing Page Exploration

### 3.1 General layout and content

The landing page presents kitchen remodeling information, a ZIP code entry form, service benefits, remodeling options, images, testimonials, and multiple opportunities to request an estimate.

The main desktop layout is generally understandable, with a consistent blue, orange, and white visual theme.

The following sections were reviewed:

| Section | Observation |
|---|---|
| Main hero / ZIP code form | Visible and functional for the tested flow |
| Complete Kitchen Remodeling | Displays service benefits and kitchen imagery |
| Remodeling Options | Images and labels appear consistent with kitchen remodeling |
| Advantages | Content and layout appear appropriate |
| Photo Gallery | Images display correctly, but a navigation issue was observed |
| Benefits of Asphalt Shingles | Content does not match the kitchen remodeling service |
| People Gather Wherever There's Food | Kitchen-related content and imagery appear appropriate |
| How It Works | Three-step explanation is visible and logically structured |
| Trust Us From Start To Finish | Statistics display correctly; their source was not verified |
| Customer Testimonials | Testimonial cards display correctly; content authenticity was not verified |
| Footer | Contains navigation links and copyright information requiring review |

### 3.2 Landing page observations

**EXP-L01 — Unrelated roofing content**

A section titled **"Benefits Of Asphalt Shingles"** appears on the kitchen remodeling landing page.

This section discusses roofing materials, roof repairs, and asphalt shingle installation.

The content is unrelated to the advertised kitchen remodeling service and appears to be copied from a roofing landing page.

Expected improvement: Replace the roofing content with relevant kitchen remodeling information or remove the section.

**EXP-L02 — Materials warranty footnote**

Within the service benefits section, the text **"Materials warranty[1]"** contains a small reference marker.

No corresponding explanatory footnote or working destination was identified during exploration.

The reference appears incomplete and may confuse users.

Further clarification is required to determine whether it should open warranty conditions, reference a disclaimer, or be removed.

**EXP-L03 — Photo Gallery navigation**

The gallery displays kitchen-related photographs.

During manual testing:

- The right navigation arrow worked.
- The pagination indicators worked.
- The left navigation arrow did not respond during the observed interaction.

The gallery's backward-navigation behavior should be reproduced and checked at different slide positions before finalizing its technical cause.

**EXP-L04 — Terms of Use navigation**

Selecting the **"Terms of Use"** footer link redirected to the Privacy Policy destination instead of displaying the Terms of Use.

Expected behavior: Each link should navigate to its corresponding legal document.

**EXP-L05 — Repeated ZIP code forms**

ZIP code entry forms appear in multiple sections of the same landing page.

A sticky ZIP code form also remains visible while scrolling.

This creates repeated calls to action, which become particularly noticeable on smaller screens.

The repeated forms do not necessarily represent a functional defect, but their value should be evaluated from a UX and conversion perspective.

Potential improvement: Retain a single accessible sticky form or review which additional forms are genuinely necessary.

**EXP-L06 — Mobile layout**

The landing page was inspected using browser mobile emulation, including an iPhone 16 viewport of approximately 393 × 852 pixels.

The layout adapts to a narrow viewport, but the sticky ZIP code form can appear near another ZIP entry section, creating visual duplication.

Further responsive checks are recommended to assess spacing, sticky positioning, and content visibility across different viewport sizes.

**EXP-L07 — Copyright year**

The footer displays **© 2027** during testing conducted in November 2026.

This may be incorrect or reflect a future publication configuration.

The intended copyright year should be confirmed.

### 3.3 ZIP code and contractor availability

A valid ZIP code allowed navigation into the remodeling estimate wizard.

For a ZIP code without matching contractors, the application displayed a notification form inviting the user to enter an email address.

The following behaviors were observed:

| Scenario | Observed result |
|---|---|
| Empty email submitted | Required-field validation displayed |
| Invalid email submitted | Validation message displayed |
| Invalid email edited | Previously displayed error disappeared while typing |
| Valid email submitted | Confirmation message displayed |
| Try another ZIP Code | Returned the user to the main landing page |

**EXP-L08 — Email validation feedback inconsistency**

When an invalid email such as `test` was submitted, an error message was displayed.

Editing the value to another invalid value, such as `testa`, immediately removed the error message.

The disappearance of the message does not establish that invalid emails are accepted, since validation may run again on submission.

This behavior is recorded as a validation-feedback inconsistency requiring clarification, rather than a confirmed submission defect.

### 3.4 Initial console investigation

Browser DevTools showed several warnings and errors during landing-page loading, including:

- Unrecognized Permissions-Policy directives.
- Requests blocked by the browser or client (`ERR_BLOCKED_BY_CLIENT`).
- Failed favicon requests returning HTTP 502.
- JavaScript errors associated with third-party resources.

These were recorded for investigation.

Some errors may originate from browser extensions, tracking-prevention settings, or external services rather than core application functionality.

Their actual functional impact was not established during the landing-page exploration.

---

## 4. Multi-Step Wizard Exploration

### 4.1 Entry and project cancellation

After entering a valid ZIP code, the user is redirected to the remodeling wizard.

The wizard contains a progress indicator, selection cards, navigation buttons, and a close icon in the upper-right area.

Selecting the close icon displays a confirmation screen offering:

- **Cancel project** — returns to the main landing page.
- **Return to project** — returns to the previous wizard step.

Both actions worked during testing.

**UX observation:** The close icon is relatively small and has low visual prominence. Increasing its contrast or providing a clearer exit affordance could improve usability.

### 4.2 Kitchen remodeling elements

**Question:** "Which elements of the kitchen would you like to update?"

Available options:

1. Kitchen cabinets
2. Countertop(s)
3. Sink(s) and faucet(s)
4. Backsplash
5. Lighting
6. Flooring
7. Plumbing
8. Change layout

This screen supports multiple selections.

Observed behavior:

- One, several, or all options can be selected.
- Selected options are highlighted with orange borders.
- Selections can be removed individually.
- With no option selected, Next is disabled.
- With at least one selected option, Next is enabled.
- The Next button updates correctly as options are selected or deselected.

These selection and button-state behaviors worked as expected.

**EXP-W01 — Cabinet requirement and alternative selections**

Selecting Kitchen cabinets, either alone or together with other options, allows progression without a warning.

Selecting only options other than Kitchen cabinets and clicking Next triggers:

> Our contractors only do projects including kitchen cabinets. Would you still like to continue?

The message presents Yes and No buttons.

Selecting Yes allows progression into a subsequent question specifically about kitchen cabinets.

This creates a possible mismatch between the user's selected project requirements and the next step.

A user who selects only sinks, for example, is still directed toward cabinet-related questions after accepting the warning.

The intended business rule needs clarification.

**Important detail:** The warning appears after clicking Next, not immediately when selecting a non-cabinet option.

### 4.3 Kitchen cabinet work

**Question:** "What would you like to do with your kitchen cabinets?"

| Selected option | Observed behavior |
|---|---|
| Replace all or most cabinets | Continues without warning |
| Repair/replace a few cabinets | Displays a qualification warning |
| Not sure | No qualification warning observed |

When selecting the second option, the application displays:

> Our contractors only replace all or most cabinets. Would still you like to continue?

The warning indicates that the available contractors do not normally support smaller cabinet repair projects.

The wording also contains a grammatical issue: **"Would still you like"** should be **"Would you still like"**.

The purpose of allowing users to continue after selecting an unsupported cabinet project should be confirmed with the Product Owner.

### 4.4 Property type

**Question:** "What type of property is this?"

Available options:

- Single family home
- Multi-family home
- Commercial

Selecting the first two options did not trigger a warning.

Selecting Commercial displayed:

> Our contractors only work with single or multi-family homes. Would you still like to continue?

**EXP-W02 — Unsupported property type offered**

The Commercial option is selectable despite the displayed business rule that contractors only work with residential properties.

This is not automatically an implementation defect, since the business may intentionally collect out-of-scope leads.

However, the reason for offering an unsupported property type and allowing continuation should be clarified.

### 4.5 Mobile, modular, or manufactured home

**Question:** "Is it a mobile, modular or manufactured home?"

- Selecting No allows normal progression.
- Selecting Yes displays a qualification warning.

Warning:

> Unfortunately, our contractors do not work with mobile, modular or manufactured homes. Would you still like to continue?

**EXP-W03 — Continuing after an unsupported property selection**

The warning informs users that contractors do not provide the requested service, while still offering a Yes option to continue.

This behavior may cause users to invest additional time in an application that cannot fulfill their request.

A possible alternative is to display a polite service-unavailability message or offer a notification option.

However, the intended lead qualification and collection requirements must be confirmed before classifying the continuation as a defect.

### 4.6 Homeowner or authorized person

**Question:** "Are you the homeowner or authorized to make property changes?"

- Selecting Yes allows normal progression.
- Selecting No displays a warning.

Warning:

> Our contractors require the homeowner or someone authorized to make property changes be present during the estimate. Would you like to continue?

**EXP-W04 — Authorization requirement clarification**

The system allows users who answer No to continue after acknowledging the warning.

This might be intentional if a homeowner or authorized person can attend the later estimate even when the current user is not authorized.

The question and warning should make that distinction clearer.

A Product Owner decision is required on whether the application should stop the flow, request confirmation that an authorized person can attend, or continue collecting the lead.

### 4.7 Kitchen size

**Question:** "What is the approximate size of your kitchen in square feet?"

The screen contains a numeric entry field and a Not sure checkbox.

Observed results:

| Scenario | Observed result |
|---|---|
| No input and Not sure unchecked | Next disabled |
| Valid numeric input | Next enabled and progression works |
| Alphabetic input | Validation message displayed |
| Numeric value of `1` | Accepted and allowed progression |
| Five-digit numeric value | Accepted during testing |
| Not sure checked with empty input | Next enabled, but the wizard does not advance |

**EXP-W05 — Not sure selection does not allow progression**

Selecting Not sure enables the Next button.

After clicking Next, the application performs a request but remains on the same question.

Repeated attempts produce the same result.

The issue prevents users who do not know their kitchen size from completing this part of the normal flow.

**Network investigation:**

The request was inspected using DevTools.

- Method: `PUT`
- Endpoint pattern: `/v1/public/leads/{leadId}`
- HTTP response: `200 OK`

Observed payload:

```json
{
  "squareFeet": null,
  "notSure": "yes",
  "trustedFormUrl": "https://cert.trustedform.com/undefined"
}
```

The request reports a successful HTTP response, while the UI fails to move forward.

This suggests that an HTTP-level success does not produce the expected wizard-state transition for the Not sure scenario.

The exact application-level cause has not been established.

**Additional observation:** The payload contains an undefined TrustedForm certificate reference. This integration detail should be investigated separately rather than assumed to cause the navigation issue.

### 4.8 Budget selection

**Question:** "Do you know what your approximate budget is?"

Available options:

- Up to $5K
- $5K to $10K
- $10K to $30K
- More than $30K
- Not sure

Observed behavior:

Selecting Up to $5K displays:

> Unfortunately, a quality kitchen remodeling project requires a higher budget than you've entered. Would you still like to continue?

The other numeric budget ranges did not display the same warning in the tested flow.

**EXP-W06 — Budget Not sure option does not work**

The Not sure option is visible, but selecting it does not enable the Next button.

The issue was reproduced and is not limited to a single click attempt.

The underlying element was inspected, and it was identified as a radio input with a budget-associated value.

Unlike the kitchen-size Not sure issue, this problem prevents progression directly through the disabled button state.

This is a separate functional issue and should be tracked independently.

### 4.9 Full name and email address

**Question:** "Who should I prepare this estimate for?"

Required information:

- Full name
- Email address

The application displays validation messages for invalid names and invalid email formats.

Examples observed include:

- A message requiring the full name to use English letters and appropriate separators.
- A message requiring a valid email address.

Navigation is blocked when the information is invalid.

**EXP-W07 — Next button remains visually enabled for invalid data**

The Next button can remain orange even when one or both fields contain invalid information.

Clicking it does not advance the wizard, confirming that validation is enforced.

This is not a validation bypass.

It is a UX consistency improvement: the button could remain disabled until the required information is valid, matching behavior already used in earlier wizard steps.

### 4.10 API and console errors during lead updates

During progression through the budget and contact-information stages, DevTools recorded a failed lead-update request.

Observed evidence:

- Method: `PUT`
- Endpoint pattern: `/v1/public/leads/{leadId}`
- HTTP response: `422 Unprocessable Content`

One captured payload contained:

```json
{
  "budget": "2",
  "trustedFormUrl": "https://cert.trustedform.com/undefined"
}
```

The browser console also displayed:

```text
Uncaught (in promise) Error:
Request failed with status code 422
```

A later step produced an additional JavaScript exception:

```text
TypeError: Cannot read properties of undefined
(reading 'contractor')
```

**EXP-W08 — Lead-update API errors**

These errors were observed during the wizard flow, including steps where the interface continued to display subsequent questions.

A failed API request could potentially affect the saving or integrity of information collected from the user.

However, the actual persistence impact has not been fully established.

Further investigation should compare the failed request, its validation response, the saved lead data, and the steps that continue afterward.

The TrustedForm URL containing `undefined` is also suspicious and should be examined as part of the integration investigation.

### 4.11 Phone number and consent

**Question:** "What is your phone number?"

The application provides a phone number input and a Submit my request button.

After providing a phone number and submitting, the application presents a confirmation screen.

The confirmation screen offers:

- Edit phone number
- Phone number is correct

The positive submission and confirmation flow was completed.

The page also displays a consent statement describing potential calls and text messages from HomeBuddy or its partners.

**UX observation:** The consent text is lengthy and uses small, low-contrast typography.

Its readability could be improved, particularly on mobile devices.

The legal correctness of the wording was not audited during this assessment.

### 4.12 Thank-you page

After confirming the phone number, the application navigates to:

`/thank-you`

The page displays a success message, details about the next steps, and information about the free estimate.

**EXP-W09 — Incorrect customer name displayed**

The thank-you page displayed:

> Thank you Andrew, your contractor QA company will call soon!

However, the submitted customer name was different.

The issue was investigated using DevTools.

A `GET /v1/public/leads/{leadId}` request returned `200 OK`.

The response contained the correct submitted value in `fullName`, while the UI displayed Andrew.

The response also contained contractor information corresponding to the displayed company name.

This establishes a mismatch between the retrieved customer data and the name displayed in the thank-you message.

The contractor name itself may represent staging/test data and should be verified separately.

**EXP-W10 — No visible return navigation**

The thank-you page does not provide an obvious button or link to return to the landing page or start a new request.

The confirmation design may intentionally represent the end of the user journey, but a home-navigation option would improve usability.

### 4.13 Thank-you page network investigation

The lead retrieval endpoint responded successfully:

- Method: `GET`
- Endpoint pattern: `/v1/public/leads/{leadId}`
- Status: `200 OK`
- Lead status: `SUBMITTED`
- Returned customer name: Matched the information entered during testing
- Displayed customer name: Did not match

This is useful evidence because it demonstrates that the issue cannot be explained simply by the submitted name being absent from the retrieved lead data.

The exact source of the incorrect UI value requires developer investigation.

Additional browser console messages included blocked third-party script requests and Permissions-Policy warnings.

These environmental warnings should not be conflated with the confirmed customer-name mismatch.

---

## 5. Positive Test Results

Several important behaviors worked correctly during exploratory testing:

| Area | Verified behavior |
|---|---|
| Landing Page | Main content and images loaded |
| ZIP Code | A valid ZIP code started the wizard |
| Contractor Availability | Notification form and confirmation flow worked |
| Project Cancellation | Cancel project returned to landing page |
| Return to Project | Returned to the previously opened wizard step |
| Kitchen Elements | Multiple selection and deselection worked |
| Required Selection | Next correctly disabled with no kitchen elements selected |
| Cabinet Work | Primary replacement option allowed progression |
| Residential Property | Eligible property selection allowed progression |
| Numeric Kitchen Size | Valid numeric values allowed progression |
| Name/Email Validation | Invalid information blocked navigation |
| Phone Confirmation | Confirmation step was displayed |
| Final Submission | Wizard reached the thank-you page |

These results describe the tested paths only and do not imply exhaustive coverage of every validation or state combination.

## 6. Cross-Cutting Observations

During testing, several patterns appeared across the application.

### 6.1 Qualification warnings versus actual eligibility

The wizard frequently informs users that their project may not be supported but still offers a way to continue.

Examples include unsupported project types, commercial properties, manufactured homes, and insufficient budgets.

This may be intentional lead-collection behavior.

However, it creates the risk of users completing a long flow without a clear understanding of whether they qualify for contractor matching.

### 6.2 Inconsistent button states

Some screens correctly disable Next until valid input is available.

Other screens leave buttons visually active despite invalid data, or fail to enable them for a visible Not sure option.

A consistent approach would improve predictability for users.

### 6.3 Error handling and API reliability

The wizard sometimes progresses despite failed API requests.

Other requests return HTTP success responses without the expected UI progression.

These situations demonstrate why both UI behavior and network responses were reviewed rather than treating a successful HTTP status as proof of an end-to-end functional success.

### 6.4 Content quality

The application contains some unrelated, potentially incomplete, or confusing content.

Examples include roofing information on a kitchen remodeling page, an unexplained warranty reference, an incorrect legal link destination, and a mismatched name on the success page.

These issues may affect credibility and user trust even when the main interface remains operational.

---

## 7. Questions for the Product Owner

The following questions should be clarified before making assumptions about intended behavior.

| ID | Area | Question |
|---|---|---|
| PO-01 | Kitchen elements | Are kitchen cabinets mandatory for every supported remodeling project? |
| PO-02 | Project selection | If cabinets are mandatory, should other standalone project types remain available? |
| PO-03 | Unsupported projects | Should users be allowed to continue when the wizard states that no contractor supports their request? |
| PO-04 | Cabinet work | Should smaller repair projects and Not sure choices be accepted for lead collection? |
| PO-05 | Property types | Is the Commercial option intentionally included despite contractors only supporting residential homes? |
| PO-06 | Manufactured homes | Should unsupported property types lead to a service-unavailable screen instead of continuing the wizard? |
| PO-07 | Ownership | Can someone who is not authorized submit a request if an authorized person will attend the estimate? |
| PO-08 | Kitchen size | Is Not sure intended to be a valid answer that allows progression without a numeric size? |
| PO-09 | Budget | Should Not sure allow progression independently of the budget ranges? |
| PO-10 | Budget qualification | Is there an actual minimum budget requirement, or is the low-budget message informational? |
| PO-11 | Lead API | Should the wizard be allowed to progress when a lead-update request returns HTTP 422? |
| PO-12 | TrustedForm | Is a valid TrustedForm certificate required for lead submission, and how should certificate failures be handled? |
| PO-13 | Thank-you page | Should the thank-you message display the submitted customer name dynamically? |
| PO-14 | Legal links | What are the intended Terms of Use and Materials warranty destinations? |
| PO-15 | Landing Page | Should the asphalt shingles section be removed from the kitchen remodeling page? |
| PO-16 | ZIP code forms | Are all repeated ZIP entry forms intentional, including those displayed alongside the sticky form? |
| PO-17 | Footer | Should the copyright year be automatically updated? |
| PO-18 | Thank-you navigation | Should users have a visible option to return home or start another request? |

These questions are not automatically classified as software defects. Some may represent deliberate business requirements, design decisions, or opportunities for improvement.

---

## 8. Areas Requiring Additional Verification

The exploratory testing covered the main user journey and several important alternative paths.

The following areas were not exhaustively tested:

- Full ZIP code validation and every possible contractor-availability response.
- All combinations of conditional wizard answers.
- Browser refresh and recovery of partially completed wizard data.
- Browser Back and Forward behavior at every wizard step.
- Actual mobile devices and multiple browser engines.
- Detailed keyboard accessibility and screen-reader support.
- Performance under network throttling or server load.
- Complete phone number boundary and negative validation.
- Downstream lead delivery, contractor assignment, and notification processing.
- Backend persistence after each failed API update.
- Third-party consent and tracking integration reliability across different browser configurations.

These items should be considered when defining the larger regression suite.

---

## 9. Exploratory Testing Conclusion

The HomeBuddy application supports the primary kitchen remodeling lead-generation flow, and a successful submission path was completed.

The landing page provides a recognizable service introduction, and many of the wizard's selection controls and validation checks operate correctly.

However, exploratory testing revealed several areas that require attention.

The most significant concerns involve broken or inconsistent progression for Not sure options, lead-update API errors, incorrect customer information on the final confirmation page, and conditional qualification flows that may not align with the contractor eligibility rules presented to users.

Additional landing-page issues affect content relevance, navigation, and visual consistency.

**Overall assessment:** The main user journey is accessible, but the application would benefit from further functional investigation, clearer qualification rules, improved error handling, and a review of user-facing content before production readiness can be established.

All identified findings will be reviewed and categorized by severity in `02-Bug-List.md`.

The three most relevant findings will then be selected for detailed reporting in `03-Bug-Reports.md`, considering their impact, reproducibility, supporting evidence, and variety of defect types.

---

**End of Exploratory Testing Report**