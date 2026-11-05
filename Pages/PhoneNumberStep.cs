using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

/// <summary>
/// Represents the phone number entry step of the kitchen remodeling wizard.
/// Provides locators and actions for entering the customer's phone number
/// and submitting the remodeling request.
///
/// Depending on application behavior, submission may proceed to the
/// optional phone confirmation step or directly to the Thank You page.
/// </summary>
public class PhoneNumberStep
{
    private readonly IPage _page;

    public PhoneNumberStep(IPage page)
    {
        _page = page;
    }

    private ILocator Step =>
        _page.Locator("#StepBodyId");

    public ILocator Question =>
        Step.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "What is your phone number?",
                Exact = true
            });

    public ILocator PhoneNumberInput =>
        Step.Locator(
            "[data-autotest-input-phonenumber-tel]");

    public ILocator SubmitRequestButton =>
        Step.Locator(
            "[data-autotest-button-submit-submit-my-request]");

    /// <summary>
    /// Enters the customer's phone number into the form.
    /// The application automatically applies US phone number formatting,
    /// so the test data contains only the 10-digit number.
    /// </summary>
    /// <param name="phoneNumber">Unformatted 10-digit US phone number.</param>
    public async Task EnterPhoneNumberAsync(string phoneNumber)
    {
        await PhoneNumberInput.FillAsync(phoneNumber);
    }

    /// <summary>
    /// Clicks "Submit my request" to continue the lead submission.
    /// The next screen depends on whether phone confirmation is required.
    /// </summary>
    public async Task SubmitRequestAsync()
    {
        await SubmitRequestButton.ClickAsync();
    }

    /// <summary>
    /// Completes the phone number entry step for TC-010.
    /// Waits for the phone number question, enters the customer contact
    /// number, and submits the remodeling request.
    ///
    /// The optional confirmation step is handled separately
    /// by PhoneConfirmationStep.
    /// </summary>
    /// <param name="phoneNumber">Customer's 10-digit US phone number.</param>
    public async Task CompleteAsync(string phoneNumber)
    {
        await Question.WaitForAsync();

        await EnterPhoneNumberAsync(phoneNumber);
        await SubmitRequestAsync();
    }
}