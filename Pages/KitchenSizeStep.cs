using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

/// <summary>
/// Represents the kitchen size step of the remodeling wizard.
/// Provides locators and actions for entering the approximate kitchen
/// size or selecting the "Not sure" option.
/// </summary>
public class KitchenSizeStep
{
    private readonly IPage _page;

    public KitchenSizeStep(IPage page)
    {
        _page = page;
    }

    private ILocator Step =>
        _page.Locator("#StepBodyId");

    public ILocator Question =>
        Step.GetByText(
            "What is the approximate size of your kitchen",
            new() { Exact = false });

    public ILocator KitchenSizeInput =>
        Step.Locator(
            "[data-autotest-input-squarefeet-tel]");

    public ILocator NotSureCheckbox =>
        Step.Locator(
            "[data-autotest-checkbox-notsure-]");

    public ILocator NotSureOption =>
        Step.Locator(
            "[data-autotest-checkbox-notsure-] + label");

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

    /// <summary>
    /// Enters the approximate kitchen size in square feet.
    /// </summary>
    /// <param name="size">Kitchen size expressed as a numeric string.</param>
    public async Task EnterKitchenSizeAsync(string size)
    {
        await KitchenSizeInput.FillAsync(size);
    }

    /// <summary>
    /// Attempts to select the "Not sure" option when the customer
    /// does not know the approximate kitchen size.
    /// Related defect: HB-001 - Unable to proceed using "Not sure".
    /// </summary>
    public async Task SelectNotSureAsync()
    {
        await NotSureOption.ClickAsync();
    }

    /// <summary>
    /// Advances to the next wizard step using the Next button.
    /// </summary>
    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    /// <summary>
    /// Completes the kitchen size step for TC-010.
    /// Waits for the question to appear, enters a numeric kitchen size,
    /// and proceeds to the budget selection step.
    ///
    /// The numeric path is used because the "Not sure" option
    /// is affected by the known defect HB-001.
    /// </summary>
    /// <param name="size">Approximate kitchen size in square feet.</param>
    public async Task CompleteAsync(string size)
    {
        await Question.WaitForAsync();
        await EnterKitchenSizeAsync(size);
        await ContinueAsync();
    }
}