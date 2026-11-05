
using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

public class ManufacturedHomeStep
{
    private readonly IPage _page;

    public ManufacturedHomeStep(IPage page)
    {
        _page = page;
    }

    // ==========================================
    // STEP 4 - Manufactured Home
    // ==========================================

    private ILocator Step =>
        _page.Locator("#StepBodyId");

    public ILocator Question =>
        Step.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Is it a mobile, modular or manufactured home?",
                Exact = true
            });

    // Radio inputs - useful for checking selection state

    public ILocator YesRadio =>
        Step.Locator(
            "[data-autotest-radio-internalmobilehome-yes]");

    public ILocator NoRadio =>
        Step.Locator(
            "[data-autotest-radio-internalmobilehome-no]");

    // Visible labels - used for clicking

    public ILocator YesOption =>
        Step.Locator(
            "[data-autotest-radio-internalmobilehome-yes] + label");

    public ILocator NoOption =>
        Step.Locator(
            "[data-autotest-radio-internalmobilehome-no] + label");

    // Next button

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

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
        await SelectNoAsync();
        await ContinueAsync();
    }
}