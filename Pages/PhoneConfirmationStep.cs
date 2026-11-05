
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace HomeBuddy.QA.Tests.Pages;

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
        Step.Locator("[data-autotest-input-phonenumber-tel]");

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
    /// Waits for either the optional phone confirmation step
    /// or the Thank You page.
    /// Returns true if phone confirmation is required.
    /// </summary>
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
    /// Retrieves the phone number displayed for confirmation.
    /// </summary>
    public async Task<string> GetDisplayedPhoneNumberAsync()
    {
        return await PhoneNumberInput.InputValueAsync();
    }

    /// <summary>
    /// Confirms the phone number and continues submission.
    /// </summary>
    public async Task ConfirmAsync()
    {
        await ConfirmButton.ClickAsync();
    }
}
