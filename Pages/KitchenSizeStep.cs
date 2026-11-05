
using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

public class KitchenSizeStep
{
    private readonly IPage _page;

    public KitchenSizeStep(IPage page)
    {
        _page = page;
    }

    // ==========================================
    // STEP 6 - Kitchen Size
    // ==========================================

    private ILocator Step =>
        _page.Locator("#StepBodyId");

    public ILocator Question =>
        Step.GetByText(
            "What is the approximate size of your kitchen",
            new() { Exact = false });

    // Numeric input
    public ILocator KitchenSizeInput =>
        Step.Locator(
            "[data-autotest-input-squarefeet-tel]");

    // Not sure checkbox
    public ILocator NotSureCheckbox =>
        Step.Locator(
            "[data-autotest-checkbox-notsure-]");

    public ILocator NotSureOption =>
        Step.Locator(
            "[data-autotest-checkbox-notsure-] + label");

    // Next button
    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

    // ==========================================
    // Actions
    // ==========================================

    public async Task EnterKitchenSizeAsync(string size)
    {
        await KitchenSizeInput.FillAsync(size);
    }

    public async Task SelectNotSureAsync()
    {
        await NotSureOption.ClickAsync();
    }

    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    // Main E2E happy-path action
    public async Task CompleteAsync(string size)
    {
        await Question.WaitForAsync();
        await EnterKitchenSizeAsync(size);
        await ContinueAsync();
    }
}