
using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages;

public class CabinetWorkStep
{
    private readonly IPage _page;

    public CabinetWorkStep(IPage page)
    {
        _page = page;
    }

    // Step 2 - Cabinet Work

    private ILocator Step =>
        _page.Locator(".kitchenUpdateStep");

    public ILocator Question =>
        Step.Locator("h4");

    // Options
    public ILocator ReplaceAllCabinets =>
        Step.Locator(
            "[data-autotest-radio-worktype-replaceall] + label");

    public ILocator RepairFewCabinets =>
        Step.Locator(
            "[data-autotest-radio-worktype-fewcabinets] + label");

    public ILocator NotSure =>
        Step.Locator(
            "[data-autotest-radio-worktype-notsure] + label");

    // Selected state
    public ILocator ReplaceAllRadio =>
        Step.Locator(
            "[data-autotest-radio-worktype-replaceall]");

    // Navigation
    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

    // Actions
    public async Task SelectReplaceAllAsync()
    {
        await ReplaceAllCabinets.ClickAsync();
    }

    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    // E2E happy path
    public async Task CompleteAsync()
    {
        await Question.WaitForAsync();
        await SelectReplaceAllAsync();
        await ContinueAsync();
    }
}