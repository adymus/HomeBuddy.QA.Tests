
using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

public class PhoneNumberStep
{
    private readonly IPage _page;

    public PhoneNumberStep(IPage page)
    {
        _page = page;
    }

    // ==========================================
    // STEP 9 - Phone Number
    // ==========================================

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

    // Phone number input
    public ILocator PhoneNumberInput =>
        Step.Locator(
            "[data-autotest-input-phonenumber-tel]");

    // Submit button
    public ILocator SubmitRequestButton =>
        Step.Locator(
            "[data-autotest-button-submit-submit-my-request]");

    // ==========================================
    // Actions
    // ==========================================

    public async Task EnterPhoneNumberAsync(string phoneNumber)
    {
        await PhoneNumberInput.FillAsync(phoneNumber);
    }

    public async Task SubmitRequestAsync()
    {
        await SubmitRequestButton.ClickAsync();
    }

    // Main E2E happy-path action
    public async Task CompleteAsync(string phoneNumber)
    {
        await Question.WaitForAsync();

        await EnterPhoneNumberAsync(phoneNumber);
        await SubmitRequestAsync();
    }
}