# HomeBuddy — QA Observations & Business Questions

**Project:** HomeBuddy — Kitchen Remodeling  
**Environment:** https://hb-test.stage.sirenltd.dev/  
**Testing Period:** November 2026  
**Testing Type:** Manual Exploratory Testing  
**Document Purpose:** Product Owner / Business Analyst Clarification & UX Recommendations

---

## 1. Overview

During exploratory testing of the HomeBuddy Kitchen Remodeling landing page and lead-generation wizard, several behaviors were identified that require clarification from a business or user-experience perspective.

Although the application allows users to complete the main lead-generation flow, certain questions and eligibility warnings appear inconsistent with the services offered by the available contractors.

The main concern is whether the wizard is intended to **qualify customers for available contractors or collect leads regardless of their eligibility**.

This distinction is important because it directly affects the expected behavior of several wizard steps.

This document separates:

- **Business Questions:** Unclear requirements that require confirmation from the Product Owner or Business Analyst.
- **UX Recommendations:** Observed usability issues and potential improvements.
- **Technical Clarifications:** Questions about integration behavior, lead handling, and staging configuration.

Confirmed defects are recorded separately in `02-Bug-List.md`.

---

## 2. Business Questions — Lead Qualification & Eligibility

### BQ-01 — Are kitchen cabinets mandatory for all remodeling projects?

**Priority:** High  
**Area:** Kitchen Elements Selection

**Observed Behavior**

The wizard allows users to select several remodeling elements, including:

- Kitchen cabinets
- Countertops
- Sinks and faucets
- Backsplash
- Lighting
- Flooring
- Plumbing
- Change layout

Selecting any option without Kitchen cabinets and clicking Next displays:

> Our contractors only do projects including kitchen cabinets. Would you still like to continue?

After selecting Yes, the wizard continues to a question specifically about kitchen cabinets.

**Business Questions**

1. Are kitchen cabinets a mandatory requirement for every remodeling project?
2. If contractors only support projects involving cabinets, why are standalone services such as sinks, flooring or lighting selectable?
3. Should the wizard adapt its questions based on the user's selected remodeling elements?
4. Is continuing without cabinets intended to collect leads for future services?

**QA Recommendation**

If cabinets are mandatory, communicate the requirement before users select their remodeling elements.

If other services are supported independently, the wizard should present questions relevant to those services rather than requiring cabinet-specific answers.

**Potential Business Impact:** Incorrect lead qualification, irrelevant questions, customer confusion and abandonment.

---

### BQ-02 — Should smaller cabinet repair projects be eligible?

**Priority:** Medium  
**Area:** Cabinet Work Selection

**Observed Behavior**

Selecting the option to repair or replace only a few cabinets displays:

> Our contractors only replace all or most cabinets. Would still you like to continue?

The wizard still offers the option to continue.

**Business Questions**

1. Are small cabinet repair projects completely unsupported?
2. If they are unsupported, should customers be allowed to continue with the standard estimate process?
3. Does the business intentionally collect leads for services that may become available later?

**QA Recommendation**

If these projects cannot be serviced, display a clear eligibility message or offer an alternative contact flow.

The warning text also contains a grammatical error, documented separately in the Bug List.

---

### BQ-03 — Why can commercial property owners continue?

**Priority:** High  
**Area:** Property Type

**Observed Behavior**

The wizard allows users to select:

- Single-family home
- Multi-family home
- Commercial

Selecting Commercial and clicking Next displays:

> Our contractors only work with single or multi-family homes. Would you still like to continue?

**Business Questions**

1. Are commercial properties completely outside the supported service scope?
2. If so, why is Commercial offered as a selectable property type?
3. Should commercial customers be redirected to another service or informed that no suitable contractors are available?
4. Is the business intentionally interested in collecting commercial leads?

**QA Recommendation**

Define whether Commercial is an eligible, conditionally eligible or unsupported property type.

The wizard should communicate the outcome clearly rather than allowing users to assume they will receive an estimate from an eligible contractor.

**Potential Business Impact:** Unsupported leads, unnecessary processing and misleading customer expectations.

---

### BQ-04 — Should manufactured homes be excluded from the standard estimate flow?

**Priority:** High  
**Area:** Manufactured Home Qualification

**Observed Behavior**

Selecting Yes to the question:

> Is it a mobile, modular or manufactured home?

Displays:

> Unfortunately, our contractors do not work with mobile, modular or manufactured homes. Would you still like to continue?

The user is offered a Yes option despite being told the contractors do not support the property type.

**Business Questions**

1. Is this property type always ineligible?
2. What happens to the lead if the user continues?
3. Will the customer be contacted even though no suitable contractor is available?
4. Should the application provide an alternative notification or service-unavailable flow?

**QA Recommendation**

If these homes are ineligible, consider ending the standard estimate flow with a polite explanation.

An alternative option could allow users to request notification if suitable services become available.

This would avoid creating the expectation that a contractor can provide an estimate for an unsupported property.

---

### BQ-05 — Is homeowner authorization required to submit a lead?

**Priority:** High  
**Area:** Homeowner Authorization

**Observed Behavior**

The application asks:

> Are you the homeowner or authorized to make property changes?

Selecting No displays:

> Our contractors require the homeowner or someone authorized to make property changes be present during the estimate. Would you like to continue?

The warning allows the user to continue.

**Business Questions**

1. Must the person completing the wizard be the homeowner or an authorized representative?
2. Can another person arrange an estimate if the homeowner will attend?
3. Should the wizard explicitly confirm that an authorized person will be present?
4. Is authorization checked later by the contractor?

**QA Recommendation**

Clarify whether authorization is required during lead submission or only during the in-home estimate.

If attendance is the actual requirement, consider asking:

"Can you confirm that the homeowner or someone authorized to make property changes will be present during the estimate?"

This would align the question with the contractor's stated requirement.

---

### BQ-06 — What is the minimum supported remodeling budget?

**Priority:** Medium  
**Area:** Budget Qualification

**Observed Behavior**

Selecting Up to $5K displays:

> Unfortunately, a quality kitchen remodeling project requires a higher budget than you've entered. Would you still like to continue?

Other tested budget ranges did not trigger this warning.

**Business Questions**

1. Is $5,000 the minimum budget required by contractors?
2. Can a project below this amount still qualify under certain circumstances?
3. Should low-budget customers complete the standard quote flow?
4. Does continuing with a low budget create a valid lead or only register customer interest?

**QA Recommendation**

Clarify whether the budget requirement is strict or informational.

If projects below the minimum cannot be serviced, the wizard should explain this clearly and offer an appropriate alternative.

---

### BQ-07 — What is the intended purpose of eligibility warnings?

**Priority:** High  
**Area:** Overall Wizard Business Logic

**Observed Pattern**

Multiple steps display eligibility restrictions while still allowing users to continue:

| Condition | Warning indicates limited or unavailable service |
|---|---|
| Project excludes kitchen cabinets | Contractors only handle projects including cabinets |
| Only a few cabinets need repair | Contractors replace all or most cabinets |
| Commercial property | Contractors work with residential homes |
| Manufactured/mobile/modular home | Contractors do not support these homes |
| Low budget | Project requires a higher budget |
| User not authorized | Authorized person must attend the estimate |

**Primary Business Question**

Is the wizard intended to:

**A. Qualify leads only for currently available contractors?**

Or:

**B. Collect potential leads even when the request does not currently match contractor requirements?**

**QA Recommendation**

The intended qualification model should be defined and documented.

If the business accepts unsupported leads for future opportunities, the UI should explain what continuing actually means.

If the business accepts only eligible leads, unsupported selections should trigger an appropriate alternative flow instead of continuing through the standard estimate process.

**Potential Business Impact:** Lead quality, conversion rates, contractor efficiency and customer trust.

---

## 3. UX Observations & Recommendations

The following observations were identified during exploratory testing.

They are not automatically considered application defects.

| ID | Area | Observed Behavior | Suggested Improvement |
|---|---|---|---|
| UX-01 | Landing Page — ZIP Forms | Multiple ZIP code forms are displayed, while another form remains sticky during scrolling | Evaluate whether the sticky form makes repeated forms unnecessary, particularly on mobile |
| UX-02 | Mobile Layout | The sticky ZIP form and in-page ZIP form can appear close together | Avoid displaying duplicate form controls simultaneously where possible |
| UX-03 | Wizard Exit | The X button functions correctly but is relatively small and visually subtle | Improve visibility, contrast and clickable area |
| UX-04 | Contact Validation | Next remains orange when Full Name or Email is invalid, although navigation is correctly blocked | Consider disabling Next until both required inputs are valid |
| UX-05 | Email Validation | An error message disappears after editing an invalid value, even when the new value is still invalid | Revalidate on change or preserve feedback until the value becomes valid |
| UX-06 | Kitchen Size | Numeric kitchen size can coexist with the Not sure selection | Consider making numeric input and Not sure mutually exclusive |
| UX-07 | Phone Consent | The consent statement is lengthy and displayed using small text | Improve readability, contrast and mobile presentation |
| UX-08 | Thank-you Page | No clear Return to Homepage or Start New Request action is visible | Consider adding a simple return-home option |
| UX-09 | Qualification Warnings | Several warnings ask whether users want to continue without explaining the final outcome | Explain whether the request remains eligible or is being collected for future availability |
| UX-10 | ZIP Validation | Empty ZIP submission provides no visible feedback even though invalid lengths display errors | Display required-field feedback or prevent submission until input is provided |
| UX-11 | ZIP Formatting | Leading and trailing spaces around a valid ZIP can be accepted, while internal spaces are rejected | Clarify whether whitespace normalization is intentional and keep the validation guidance consistent |

### UX Considerations

Repeated calls to action may be intentional on a lead-generation page and are not necessarily defects.

Likewise, an enabled-looking Next button with working validation does not constitute a functional validation failure by itself.

These observations are recommendations intended to improve consistency and usability.

---

## 4. Landing Page Content & Business Clarifications

Several content issues were identified on the landing page. Confirmed defects are already included in `02-Bug-List.md`; the following questions concern their expected business content or presentation.

| ID | Topic | Business / Content Question |
|---|---|---|
| CONTENT-01 | Materials warranty `[1]` | Is the reference intended to link to warranty terms, a disclaimer or explanatory footnote? |
| CONTENT-02 | Benefits Of Asphalt Shingles | Is this section accidentally reused from a roofing template? What kitchen remodeling content should replace it? |
| CONTENT-03 | Terms of Use | What is the approved Terms of Use document and intended destination? |
| CONTENT-04 | Copyright 2027 | Is the future copyright year intentional or a configuration/content mistake? |
| CONTENT-05 | Contractor name | Is "QA company" an expected staging contractor, or should another name appear in the final confirmation? |
| CONTENT-06 | Testimonials and statistics | Are the displayed review texts, ratings and statistics approved for this landing page? |

These questions require confirmation of approved content rather than assumptions about the application's underlying implementation.

---

## 5. Technical & Integration Clarifications

### TECH-01 — How should rejected lead updates be handled?

**Observed Behavior**

During wizard progression, a `PUT /v1/public/leads/{leadId}` request returned HTTP 422.

A JavaScript exception was also observed, while the user interface continued to subsequent steps in the tested session.

**Questions for Development / Product Owner**

- What validation condition caused the HTTP 422 response?
- Was the rejected update required for successful lead creation?
- Was the relevant data persisted despite the failed request?
- Should users receive visible feedback when a lead update fails?
- Should the wizard prevent progression when a required update is rejected?

**Recommendation**

Establish the expected behavior for failed lead updates, including retry handling, user feedback and data consistency.

The exact root cause and persistence impact were not confirmed during exploratory testing.

### TECH-02 — TrustedForm certificate reference

**Observed Behavior**

Captured lead-update payloads included:

`"trustedFormUrl": "https://cert.trustedform.com/undefined"`

Third-party script-loading failures were also observed in browser DevTools.

**Questions**

- Is a valid TrustedForm certificate mandatory for lead processing?
- Why was the captured certificate reference undefined?
- Does staging intentionally bypass certificate generation?
- Could missing certificate data cause rejected requests?
- How should the application behave if the integration is unavailable?

**Recommendation**

Verify the intended behavior in the staging environment and whether the undefined value is expected, optional or invalid.

No causal relationship to the HTTP 422 response was established during testing.

### TECH-03 — Incorrect customer name on the Thank-you page

**Observed Behavior**

The thank-you page displayed Andrew even though the lead retrieval API returned the correct submitted customer name.

**Questions**

- Which data source should populate the customer name on the thank-you page?
- Is the displayed value generated from the lead response, browser state or another source?
- Is any customer placeholder data intentionally configured for staging?

**Recommendation**

Ensure that confirmation details reflect the same lead record successfully retrieved from the backend.

The data mismatch is already tracked as a confirmed defect in the Bug List.

### TECH-04 — Third-party scripts and browser Console warnings

**Observed Behavior**

The browser Console displayed blocked requests, Permissions-Policy warnings and failed static-resource requests.

**Questions**

- Which third-party services are required for the staging environment?
- Are blocked analytics or tracking requests expected during QA testing?
- Do any of the warnings affect lead attribution or conversion tracking?
- Should known staging-only Console errors be documented as accepted limitations?

**Recommendation**

Separate expected staging configuration warnings from functional application errors to make defect investigation and regression analysis more reliable.

---

## 6. Recommended Product Owner Discussion Priorities

The following items are recommended for the next refinement or clarification discussion.

| Priority | Topic | Decision Required |
|---|---|---|
| P1 | Kitchen cabinets requirement | Confirm whether cabinets are mandatory for every supported project |
| P1 | Unsupported property types | Define whether ineligible customers can continue and why |
| P1 | Lead qualification strategy | Clarify whether the wizard collects only eligible leads or also future opportunities |
| P1 | Homeowner authorization | Define who may request an estimate and who must attend |
| P1 | Lead-update errors | Confirm expected behavior when API persistence fails |
| P2 | Budget restrictions | Establish minimum supported budget and low-budget lead handling |
| P2 | Not sure answers | Confirm the intended acceptance and storage of unknown size and budget values |
| P2 | Contact and confirmation data | Confirm required data displayed after successful submission |
| P2 | Legal and warranty content | Confirm approved legal links, warranty references and correct landing-page content |
| P3 | UI consistency | Review sticky forms, button states, exit controls and final navigation |

**Priority definitions:**

- **P1:** Directly influences lead eligibility, core business flow or reliability.
- **P2:** Important functional or content requirements requiring alignment.
- **P3:** Primarily usability or presentation improvements.

These are discussion priorities, not defect severity ratings.

---

## 7. Conclusion

Exploratory testing revealed a recurring theme in the HomeBuddy lead-generation wizard: several eligibility questions describe contractor restrictions while still permitting users to continue.

This may reflect an intentional business strategy to collect potential leads beyond current contractor availability, or it may indicate that qualification rules are not fully enforced.

The application would benefit from a clearly defined decision on:

- Which remodeling projects and property types are supported.
- Which questions are informational versus mandatory eligibility checks.
- When unsupported customers should be redirected to an alternative flow.
- How failed lead updates and third-party integration errors should be handled.
- How to communicate eligibility and next steps without creating misleading customer expectations.

The UX and content observations provide additional opportunities to improve consistency, clarity and customer trust.

**This document serves as supplementary QA analysis for Product Owner, Business Analyst and Development discussions. It does not replace the formal Bug List or the three detailed bug reports required by the assessment.**

---

**End of QA Observations & Business Questions**