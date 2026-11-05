using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages;

/// <summary>
/// Represents the optional phone confirmation step of the kitchen
/// remodeling wizard. Handles both possible submission paths:
/// confirming the phone number or proceeding directly to the Thank You page.
/// </summary>
public class PhoneConfirmationStep
{
    private readonly IPage _page;

    public PhoneConfirmationStep(IPage page)
    {
        _page = page;
    }

    private ILocator Step =>
        _page.Locator("#StepBodyId");

    public ILocator Question =>
        Step.GetByRole(
            AriaRole.Heading,
            new() { Name = "Please confirm your phone number." });

    public ILocator PhoneNumberInput =>
        Step.Locator(
            "[data-autotest-input-phonenumber-tel]");

    public ILocator ConfirmButton =>
        Step.Locator(
            "[data-autotest-button-submit-phone-number-is-correct]");

    public ILocator EditPhoneButton =>
        Step.Locator(
            "[data-autotest-button-button-edit-phone-number]");

    private ILocator ThankYouHeading =>
        Step.GetByRole(
            AriaRole.Heading,
            new() { NameRegex = new Regex(@"^Thank you\b") });

    /// <summary>
    /// Waits until either the phone confirmation screen or the
    /// Thank You page becomes visible.
    /// Returns true when phone confirmation is required and false
    /// when the application proceeds directly to the Thank You page.
    /// </summary>
    /// <returns>
    /// True if phone confirmation is displayed; otherwise, false.
    /// </returns>
    public async Task<bool> IsConfirmationRequiredAsync()
    {
        await ConfirmButton
            .Or(ThankYouHeading)
            .First
            .WaitForAsync(new()
            {
                State = WaitForSelectorState.Visible,
                Timeout = 30000
            });

        return await ConfirmButton.IsVisibleAsync();
    }

    /// <summary>
    /// Retrieves the phone number displayed on the confirmation screen.
    /// Used by TC-010 to verify that the entered number is preserved
    /// correctly after the application applies its formatting.
    /// </summary>
    /// <returns>The phone number displayed in the input field.</returns>
    public async Task<string> GetDisplayedPhoneNumberAsync()
    {
        return await PhoneNumberInput.InputValueAsync();
    }

    /// <summary>
    /// Confirms that the displayed phone number is correct
    /// and continues the lead submission process.
    /// </summary>
    public async Task ConfirmAsync()
    {
        await ConfirmButton.ClickAsync();
    }
}