# HomeBuddy — Bug List

**Application:** HomeBuddy — Kitchen Remodeling  
**Environment:** https://hb-test.stage.sirenltd.dev/  
**Testing:** Manual exploratory testing — November 2026  
**Browser:** Microsoft Edge (Chromium)

## Overview

The following issues were identified during exploratory testing of the landing page and multi-step lead-generation wizard.

Issues are categorized by severity based on their observed behavior and potential customer or business impact. Findings requiring further confirmation are identified separately.

## High Severity

| ID | Bug / Finding | Area | Status |
|---|---|---|---|
| HB-001 | Kitchen Size: Selecting "Not sure" with an empty field does not advance the wizard, despite a successful API response (200 OK). | Wizard | Confirmed |
| HB-002 | Budget: Selecting "Not sure" does not enable the Next button, preventing progression. | Wizard | Confirmed |
| HB-003 | Selecting remodeling options without Kitchen cabinets allows progression to cabinet-specific questions after accepting a warning. | Wizard / Business Logic | Requires business clarification |
| HB-004 | Lead-update API returns HTTP 422 during wizard progression, with an unhandled JavaScript error. | API | Confirmed response failure; impact under investigation |
| HB-005 | Thank-you page displays the wrong customer name despite the API returning the submitted name correctly. | Thank-you Page | Confirmed |
| HB-006 | Terms of Use redirects to the Privacy Policy instead of the intended legal document. | Landing Page / Navigation | Confirmed |
| HB-007 | Unrelated "Benefits Of Asphalt Shingles" content appears on the Kitchen Remodeling landing page. | Landing Page / Content | Confirmed |

## Medium Severity

| ID | Bug / Finding | Area | Status |
|---|---|---|---|
| HB-008 | Photo Gallery left navigation arrow did not respond during testing, while the right arrow and pagination controls worked. | Landing Page / UI | Requires verification |

## Low Severity

| ID | Bug / Finding | Area | Status |
|---|---|---|---|
| HB-009 | "Materials warranty[1]" displays an unexplained reference marker without an identified explanation or destination. | Landing Page / Content | Requires clarification |
| HB-010 | Cabinet warning contains incorrect wording: "Would still you like to continue?" | Wizard / Content | Confirmed |
| HB-011 | Footer displays copyright year 2027 during testing in November 2026. | Landing Page / Footer | Requires clarification |
| HB-012 | Favicon resource requests return HTTP 502 Bad Gateway. | Landing Page / Network | Observed |
| HB-013 | Email validation feedback disappears after editing an invalid value, although the resulting value remains invalid. | Wizard / Validation UI | Observed |

## Severity Summary

| Severity | Findings |
|---|---:|
| Critical | 0 confirmed |
| High | 7 |
| Medium | 1 |
| Low | 5 |
| **Total** | **13** |

*Severity reflects current assessment priority rather than confirmed business impact for every finding.*

**Critical severity consideration:** HB-001 and HB-002 prevent certain users from completing the lead-generation flow through the provided "Not sure" options. Their impact may warrant Critical priority if evidence shows substantial conversion loss or the absence of an acceptable workaround.

## Formal Bug Reports — Selected Top 3

The following findings were selected for detailed formal bug reports based on their business impact, supporting evidence, and representation of different defect categories.

| ID | Category | Reason for Selection |
|---|---|---|
| **HB-001** | Functional / Wizard Navigation | Selecting "Not sure" for kitchen size prevents progression despite the API returning HTTP 200 OK. This blocks a valid user journey. |
| **HB-004** | API / Error Handling | A lead-update request returns HTTP 422, accompanied by an unhandled JavaScript error. This raises concerns about error handling and lead-data reliability. The exact persistence impact remains under investigation. |
| **HB-005** | UI / Data Display | The Thank-you page displays an incorrect customer name, although the lead retrieval API returns the correct submitted information. |

Detailed reproduction steps, expected and actual results, technical evidence, and impact assessments are available in **`03-Bug-Reports.md`**.

The remaining identified issues are retained in this bug list, while business-rule questions and UX recommendations are documented separately in **`04-QA-Observations-and-Business-Questions.md`**.