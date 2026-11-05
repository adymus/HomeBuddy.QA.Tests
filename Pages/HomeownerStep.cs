
using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

public class HomeownerStep
{
    private readonly IPage _page;

    public HomeownerStep(IPage page)
    {
        _page = page;
    }

    // ==========================================
    // STEP 5 - Homeowner Authorization
    // ==========================================

    private ILocator Step =>
        _page.Locator("#StepBodyId");

    public ILocator Question =>
        Step.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Are you the homeowner or authorized to make property changes?",
                Exact = true
            });

    // Radio inputs - useful for checking selection state

    public ILocator YesRadio =>
        Step.Locator(
            "[data-autotest-radio-owner-yes]");

    public ILocator NoRadio =>
        Step.Locator(
            "[data-autotest-radio-owner-no]");

    // Visible labels - used for clicking

    public ILocator YesOption =>
        Step.Locator(
            "[data-autotest-radio-owner-yes] + label");

    public ILocator NoOption =>
        Step.Locator(
            "[data-autotest-radio-owner-no] + label");

    // Next button

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-button-next]");

    // ==========================================
    // Actions
    // ==========================================

    public async Task SelectYesAsync()
    {
        await YesOption.ClickAsync();
    }

    public async Task SelectNoAsync()
    {
        await NoOption.ClickAsync();
    }

    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    // Main E2E happy-path action

    public async Task CompleteAsync()
    {
        await Question.WaitForAsync();
        await SelectYesAsync();
        await ContinueAsync();
    }
}