
using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

public class ContactInfoStep
{
    private readonly IPage _page;

    public ContactInfoStep(IPage page)
    {
        _page = page;
    }

    // ==========================================
    // STEP 8 - Contact Information
    // ==========================================

    private ILocator Step =>
        _page.Locator("#StepBodyId");

    public ILocator Question =>
        Step.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Who should I prepare this estimate for?",
                Exact = true
            });

    // Input fields

    public ILocator FullNameInput =>
        Step.Locator(
            "[data-autotest-input-fullname-text]");

    public ILocator EmailInput =>
        Step.Locator(
            "[data-autotest-input-email-text]");

    // Next button

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

    // ==========================================
    // Actions
    // ==========================================

    public async Task EnterFullNameAsync(string fullName)
    {
        await FullNameInput.FillAsync(fullName);
    }

    public async Task EnterEmailAsync(string email)
    {
        await EmailInput.FillAsync(email);
    }

    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    // Main E2E happy-path action

    public async Task CompleteAsync(
        string fullName,
        string email)
    {
        await Question.WaitForAsync();

        await EnterFullNameAsync(fullName);
        await EnterEmailAsync(email);

        await ContinueAsync();
    }
}
