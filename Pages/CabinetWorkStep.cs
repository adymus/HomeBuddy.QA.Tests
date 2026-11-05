using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages;

/// <summary>
/// Represents the cabinet work selection step of the kitchen remodeling wizard.
/// Provides locators and actions for selecting the type of cabinet work
/// and continuing to the next step.
/// </summary>
public class CabinetWorkStep
{
    private readonly IPage _page;

    public CabinetWorkStep(IPage page)
    {
        _page = page;
    }

    private ILocator Step =>
        _page.Locator(".kitchenUpdateStep");

    public ILocator Question =>
        Step.Locator("h4");

    public ILocator ReplaceAllCabinets =>
        Step.Locator(
            "[data-autotest-radio-worktype-replaceall] + label");

    public ILocator RepairFewCabinets =>
        Step.Locator(
            "[data-autotest-radio-worktype-fewcabinets] + label");

    public ILocator NotSure =>
        Step.Locator(
            "[data-autotest-radio-worktype-notsure] + label");

    public ILocator ReplaceAllRadio =>
        Step.Locator(
            "[data-autotest-radio-worktype-replaceall]");

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

    /// <summary>
    /// Selects the option to replace all or most kitchen cabinets.
    /// This is the cabinet work option used in TC-010.
    /// </summary>
    public async Task SelectReplaceAllAsync()
    {
        await ReplaceAllCabinets.ClickAsync();
    }

    /// <summary>
    /// Advances to the next wizard step using the Next button.
    /// </summary>
    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    /// <summary>
    /// Completes the cabinet work step for the end-to-end lead submission test.
    /// Waits for the question to appear, selects replacement of all or most
    /// cabinets, and proceeds to the property type selection step.
    /// </summary>
    public async Task CompleteAsync()
    {
        await Question.WaitForAsync();
        await SelectReplaceAllAsync();
        await ContinueAsync();
    }
}