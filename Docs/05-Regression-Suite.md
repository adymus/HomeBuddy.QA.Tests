# HomeBuddy — Regression Test Suite Structure

**Project:** HomeBuddy — Kitchen Remodeling  
**Application:** Landing Page & Lead Generation Wizard  
**Environment:** https://hb-test.stage.sirenltd.dev/  
**Testing Period:** November 2026  
**Test Approach:** Risk-Based Functional Regression Testing  
**Document Type:** Test Suite Design

---

## 1. Objective

The purpose of this document is to define a structured regression testing approach for the HomeBuddy Kitchen Remodeling application.

The regression suite is organized around the main customer journey, starting from the landing page and ZIP code entry, continuing through the remodeling questionnaire, and ending with lead submission and the Thank-you page.

The primary testing focus is to ensure that application changes do not introduce regressions affecting:

- Customer access to the remodeling estimate wizard.
- Form validation and wizard navigation.
- Contractor eligibility and project qualification.
- Accurate collection and persistence of lead information.
- Successful submission and confirmation.
- Customer-facing content, navigation, and usability.

The proposed suite includes both positive and negative scenarios, with additional attention given to issues identified during exploratory testing.

This document defines the high-level regression suite. Ten detailed test cases are documented separately in `06-Test-Cases.md`.

---

## 2. Proposed Regression Suite Modules

| Suite ID | Module | Main Coverage | Priority |
|---|---|---|---|
| REG-01 | Landing Page — Content & Navigation | Page sections, images, text, footer, legal links, content accuracy | Medium |
| REG-02 | Landing Page — Interactive Components | Photo Gallery, carousel navigation, pagination, sticky ZIP forms | Medium |
| REG-03 | ZIP Code Validation | Required input, accepted format, invalid input, leading zeros, whitespace | High |
| REG-04 | Contractor Availability | Contractor matching, no-match fallback, notification email, retry navigation | High |
| REG-05 | Wizard Navigation & State Management | Required selections, Next/Back, exit confirmation, cancellation, answer retention | High |
| REG-06 | Project Qualification & Eligibility | Kitchen elements, cabinets, property type, manufactured homes, authorization, warnings | High |
| REG-07 | Kitchen Size & Budget | Numeric validation, unknown values, budget options, conditional warnings | High |
| REG-08 | Customer Contact Information | Full name, email, phone number, validation, consent, confirmation | High |
| REG-09 | Lead API & Error Handling | Lead creation/update/retrieval, request validation, HTTP errors, data consistency | High |
| REG-10 | Thank-you Page | Successful submission, customer name, contractor details, final confirmation | High |
| REG-11 | Responsive, Accessibility & Compatibility | Mobile/desktop layouts, keyboard usability, browser compatibility, readable content | Medium |

---

## 3. Regression Coverage

### REG-01 — Landing Page: Content & Navigation

**Objective:** Verify that the landing page displays accurate kitchen remodeling information and provides correct navigation.

**Coverage:**
- Main page sections load and display correctly.
- Headings, descriptions, images, and service benefits are relevant.
- No unrelated service content is displayed.
- Terms of Use and Privacy Policy open their intended destinations.
- Warranty references and disclaimers are complete and accessible.
- Footer information is accurate.

**Related findings:** Incorrect Terms of Use destination, unrelated asphalt-shingles content, unexplained warranty footnote, and copyright-year inconsistency.

### REG-02 — Landing Page: Interactive Components

**Objective:** Verify that interactive landing-page elements operate consistently.

**Coverage:**
- Photo Gallery forward and backward navigation.
- Gallery pagination indicators and boundary behavior.
- Testimonial carousel navigation.
- ZIP code forms across different landing-page sections.
- Sticky ZIP form behavior during scrolling.
- Interaction consistency on desktop and mobile.

**Related findings:** Gallery left-arrow observation and repeated ZIP code forms.

### REG-03 — ZIP Code Validation

**Objective:** Verify that ZIP code inputs accept valid values and reject invalid values with appropriate feedback.

**Coverage:**
- Empty ZIP code.
- Valid five-digit numeric ZIP code.
- Fewer or more than five digits.
- Alphabetic and special characters.
- Leading-zero values.
- Leading, trailing, and internal whitespace.
- Consistent behavior across repeated ZIP forms.
- Correct navigation after accepting a ZIP code.

### REG-04 — Contractor Availability & Fallback

**Objective:** Verify the correct customer flow based on contractor availability.

**Coverage:**
- ZIP code with available contractors.
- ZIP code without contractor matches.
- Correct no-contractor message.
- Notification email required-field validation.
- Invalid and valid email formats.
- Successful notification confirmation.
- Try another ZIP Code navigation.
- Error handling for failed availability requests.

### REG-05 — Wizard Navigation & State Management

**Objective:** Verify consistent navigation and preservation of selected data throughout the wizard.

**Coverage:**
- Initial wizard loading.
- Required selection validation.
- Next button enabled/disabled behavior.
- Single and multiple selections.
- Selection and deselection.
- Previous-step navigation where available.
- Exit confirmation.
- Return to Project behavior.
- Cancel Project behavior.
- Answer preservation when returning to a step.
- Browser Back, Forward, and Refresh behavior.
- Prevention of unintended duplicate submissions.

### REG-06 — Project Qualification & Eligibility

**Objective:** Verify the conditional questions and business rules used to qualify remodeling requests.

**Coverage:**
- Kitchen remodeling elements.
- Projects including and excluding kitchen cabinets.
- Full cabinet replacement versus minor repairs.
- Single-family, multi-family, and commercial properties.
- Manufactured, mobile, and modular homes.
- Homeowner authorization.
- Conditional eligibility warnings.
- Yes/No responses to warnings.
- Routing of supported and unsupported project types.

**Business dependency:** The intended handling of customers who continue after an unsupported-project warning requires Product Owner clarification.

### REG-07 — Kitchen Size & Budget

**Objective:** Verify valid data entry, unknown-value handling, and navigation.

**Coverage:**
- Empty kitchen-size input.
- Numeric input and invalid characters.
- Minimum, maximum, and unrealistic numeric values.
- Kitchen Size — Not sure.
- Budget range selection.
- Budget — Not sure.
- Low-budget eligibility warning.
- Selection-state consistency.
- Correct progression and stored values.

**Related findings:** Kitchen Size Not sure and Budget Not sure both prevent progression through their intended paths.

### REG-08 — Customer Contact Information

**Objective:** Verify that valid contact information can be submitted and invalid information is rejected.

**Coverage:**
- Required full name.
- Full-name format validation.
- Required email.
- Email format validation.
- Validation-message visibility.
- Next button behavior with invalid values.
- Phone number entry and validation.
- Phone number editing.
- Phone number confirmation.
- Consent information visibility.
- Successful continuation with approved test contact data.

### REG-09 — Lead API & Error Handling

**Objective:** Verify consistency and reliability of lead creation, updates, retrieval, and failure handling.

**Coverage:**
- Lead creation and update requests.
- Correct request payloads.
- Successful and unsuccessful HTTP responses.
- Backend validation errors.
- Failed network requests and timeouts.
- Handling of missing or optional values.
- Persistence of customer selections.
- Data integrity between API and UI.
- Unhandled JavaScript errors.
- Third-party integration failures.
- Prevention of misleading success states.

**Related findings:** HTTP 422 lead-update failure, unhandled JavaScript error, and undefined TrustedForm certificate reference.

### REG-10 — Thank-you Page

**Objective:** Verify accurate confirmation of submitted leads.

**Coverage:**
- Navigation after successful submission.
- Correct customer name.
- Correct contractor information.
- Lead status and confirmation message.
- Consistency between displayed information and API data.
- Handling of missing or invalid response data.
- Final-page navigation and usability.

**Related finding:** The Thank-you page displayed a different customer name from the one returned by the lead API.

### REG-11 — Responsive, Accessibility & Compatibility

**Objective:** Verify that the application remains usable across supported browsers, devices, and input methods.

**Coverage:**
- Desktop and mobile viewports.
- Sticky elements and form visibility.
- Text wrapping, spacing, and control overlap.
- Mobile wizard usability.
- Keyboard navigation and focus visibility.
- Form labels and validation accessibility.
- Text readability and contrast.
- Browser-specific functionality.
- Touch and pointer interaction behavior.

**Testing limitation:** The exploratory assessment included Microsoft Edge and selected mobile emulation. Cross-browser and accessibility testing are proposed regression coverage, not completed assessment results.

---

## 4. Regression Execution Strategy

A risk-based execution approach is recommended.

| Level | Scope | Execution |
|---|---|---|
| P0 — Smoke | Landing-page access, accepted ZIP, wizard access, supported project flow, contact submission, Thank-you page | After each relevant deployment |
| P1 — Core Regression | Required fields, wizard navigation, qualification, kitchen size, budget, contact validation, lead API, data accuracy | Before release and after core changes |
| P2 — Extended Regression | Content, legal navigation, gallery, responsive behavior, browser compatibility, secondary user paths | During planned regression cycles and related changes |

P0/P1/P2 indicate test execution priority and are separate from bug severity.

### Test Design Techniques

The proposed suite incorporates:

- **Equivalence partitioning:** Valid and invalid ZIP codes, names, emails, phone numbers, and numeric values.
- **Boundary-value analysis:** Minimum/maximum lengths and kitchen-size values.
- **Decision-table testing:** Contractor eligibility based on project and property selections.
- **State-transition testing:** Wizard progression, answer selection, cancellation, and confirmation.
- **End-to-end testing:** Successful lead generation from the landing page to final confirmation.
- **API/UI consistency testing:** Comparison between submitted lead data and the final displayed information.

---

## 5. Test Data & Dependencies

| Data / Dependency | Purpose |
|---|---|
| ELIGIBLE_ZIP | Valid ZIP code with available contractors |
| NO_MATCH_ZIP | Valid ZIP code without available contractors |
| Approved test customer | Safe submission of lead-generation test requests |
| Valid and invalid email values | Contact and notification validation |
| Approved test phone number | Phone submission and confirmation |
| Supported remodeling selections | Happy-path progression |
| Unsupported remodeling selections | Conditional warning and eligibility testing |
| Numeric and unknown size/budget values | Input validation and alternative-flow coverage |

Contractor availability is dependent on staging configuration and must be controlled or verified during test execution.

Repeated end-to-end executions should use approved test identities to avoid unnecessary real customer or contractor communications.

---

## 6. Entry and Exit Criteria

### Entry Criteria
- Staging environment is available.
- Build under test is known.
- Required test data and contractor-matching ZIP codes are available.
- Relevant services are accessible or known environment limitations are documented.
- Test cases are prepared for execution.

### Exit Criteria
- Planned P0 tests are executed.
- Core regression results are reviewed.
- Unresolved Critical and High issues are assessed for release impact.
- Failed tests have clear evidence and defect references.
- Known limitations, blocked cases, and outstanding business-rule questions are documented.
- UI/API consistency is verified for successfully submitted test leads.

---

## 7. Selected Detailed Test Cases

The following ten cases were selected for this assessment:

| Test ID | Scenario | Module |
|---|---|---|
| TC-001 | Terms of Use navigation | REG-01 |
| TC-002 | ZIP code input validation | REG-03 |
| TC-003 | No-contractor fallback and notification email | REG-04 |
| TC-004 | Photo Gallery navigation | REG-02 |
| TC-005 | Kitchen-element multi-selection and required selection | REG-05 |
| TC-006 | Wizard exit, Return to Project, and Cancel Project | REG-05 |
| TC-007 | Kitchen Size — Not sure progression | REG-07 |
| TC-008 | Budget — Not sure progression | REG-07 |
| TC-009 | Full Name and Email validation | REG-08 |
| TC-010 | End-to-end lead submission and confirmation accuracy | REG-09 / REG-10 |

These cases provide representative coverage of core functionality and identified risks. They do not constitute exhaustive coverage of the full regression suite.

### Automation Candidates

The following scenarios offer the highest anticipated regression automation value:

| Test ID | Automation Value | Rationale |
|---|---|---|
| TC-010 | Very High | Verifies the complete business-critical journey from landing page to submitted lead and confirmation |
| TC-002 | High | Repeatable, data-driven ZIP code validation with relatively low maintenance cost |
| TC-007 | High | Protects a known progression defect from recurring after correction |

TC-010 is the preferred scenario for the assessment's automation code challenge.

---

## 8. Conclusion

The proposed regression suite provides structured coverage of the HomeBuddy Kitchen Remodeling application across landing-page behavior, lead qualification, validation, navigation, API integration, submission, and confirmation.

The highest priority is assigned to the lead-generation journey because failures in these areas can directly prevent or compromise customer submissions.

The ten detailed manual test cases are maintained in `06-Test-Cases.md`.

---

**End of Regression Test Suite Structure**