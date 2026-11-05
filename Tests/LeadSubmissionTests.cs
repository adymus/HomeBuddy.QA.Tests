using HomeBuddy.QA.Tests.Data;
using HomeBuddy.QA.Tests.Pages;
using HomeBuddy.QA.Tests.Pages.Wizard;
using HomeBuddy.QA.Tests.Setup;

namespace HomeBuddy.QA.Tests.Tests;

[TestFixture]
[Category("E2E")]
public class LeadSubmissionTests : BaseTest
{
    /// <summary>
    /// TC-010: Verifies the end-to-end kitchen remodeling lead submission
    /// journey, from the landing page to the Thank You page.
    ///
    /// Handles the optional phone confirmation step and verifies that
    /// the final confirmation displays the submitted customer's first name.
    ///
    /// Related defect: HB-005 - Incorrect customer name on Thank You page.
    /// </summary>
    [Test]
    public async Task TC010_LeadSubmission_ShouldDisplayCorrectCustomerName()
    {
        var testData = LeadTestData.Create();

        // Start the estimate using a ZIP code with available contractors.
        var landingPage = new LandingPage(Page);

        await landingPage.OpenAsync(AppUrl);
        await landingPage.StartEstimateAsync(testData.ZipCode);

        // Select kitchen cabinets as the remodeling requirement.
        var kitchenElements = new KitchenElementsStep(Page);

        await kitchenElements.CompleteAsync();

        // Choose to replace all or most kitchen cabinets.
        var cabinetWork = new CabinetWorkStep(Page);

        await cabinetWork.CompleteAsync();

        // Select a supported residential property type.
        var propertyType = new PropertyTypeStep(Page);

        await propertyType.CompleteAsync();

        // Confirm that the property is not a manufactured home.
        var manufacturedHome = new ManufacturedHomeStep(Page);

        await manufacturedHome.CompleteAsync();

        // Confirm ownership or authorization to modify the property.
        var homeowner = new HomeownerStep(Page);

        await homeowner.CompleteAsync();

        // Enter a numeric kitchen size to follow the valid submission path.
        // The "Not sure" issue is documented in TC-007 (HB-001).
        var kitchenSize = new KitchenSizeStep(Page);

        await kitchenSize.CompleteAsync(testData.KitchenSize);

        // Select the $10K to $30K remodeling budget range.
        var budget = new BudgetStep(Page);

        await budget.CompleteAsync();

        // Provide customer details for the lead submission.
        // The first name will be checked against the final confirmation.
        var contactInfo = new ContactInfoStep(Page);

        await contactInfo.CompleteAsync(
            testData.FullName,
            testData.Email);

        // Enter the phone number and submit the remodeling request.
        // The application applies its own phone number formatting.
        var phoneNumber = new PhoneNumberStep(Page);

        await phoneNumber.CompleteAsync(testData.PhoneNumber);

        // Handle the optional phone confirmation screen.
        // The application may also proceed directly to the Thank You page.
        var phoneConfirmation = new PhoneConfirmationStep(Page);

        if (await phoneConfirmation.IsConfirmationRequiredAsync())
        {
            // Verify that the displayed phone number matches the input,
            // ignoring the application's formatting and +1 country code.
            var displayedPhoneNumber =
                await phoneConfirmation.GetDisplayedPhoneNumberAsync();

            var normalizedPhoneNumber =
                Regex.Replace(displayedPhoneNumber, @"\D", "");

            var expectedPhoneNumber =
                "1" + Regex.Replace(testData.PhoneNumber, @"\D", "");

            Assert.That(
                normalizedPhoneNumber,
                Is.EqualTo(expectedPhoneNumber),
                "Phone confirmation displays a different number.");

            // Confirm the phone number to complete the submission.
            await phoneConfirmation.ConfirmAsync();

            TestContext.Out.WriteLine(
                "Phone confirmation completed successfully.");
        }
        else
        {
            TestContext.Out.WriteLine(
                "Phone confirmation was skipped by the application.");
        }

        // Verify successful navigation to the Thank You page.
        var thankYouPage = new ThankYouPage(Page);

        await thankYouPage.WaitUntilLoadedAsync();

        Assert.That(
            new Uri(Page.Url).AbsolutePath,
            Is.EqualTo("/thank-you"),
            "Lead submission did not reach the Thank You page.");

        // Validate the customer's first name in the confirmation message.
        // This assertion provides automated coverage for HB-005.
        var confirmationMessage =
            await thankYouPage.GetConfirmationMessageAsync();

        TestContext.Out.WriteLine(
            $"Confirmation message: {confirmationMessage}");

        Assert.That(
            confirmationMessage,
            Does.StartWith($"Thank you {testData.FirstName},"),
            $"Customer name mismatch. Actual: {confirmationMessage}");
    }
}