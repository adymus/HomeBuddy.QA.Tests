using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

/// <summary>
/// Represents the kitchen elements selection step of the remodeling wizard.
/// Provides locators and actions for choosing which parts of the kitchen
/// the customer wants to remodel.
/// </summary>
public class KitchenElementsStep
{
    private readonly IPage _page;

    public KitchenElementsStep(IPage page)
    {
        _page = page;
    }

    private ILocator Step =>
        _page.Locator(".kitchenElementsStep");

    public ILocator KitchenCabinetsOption =>
        Step.GetByText(
            "Kitchen cabinets",
            new() { Exact = true });

    public ILocator KitchenCabinetsCheckbox =>
        Step.Locator(
            "[data-autotest-checkbox-updates-cabinets]");

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-button-next]");

    /// <summary>
    /// Selects kitchen cabinets as the remodeling requirement.
    /// This is the option used in the TC-010 lead submission scenario.
    /// </summary>
    public async Task SelectKitchenCabinetsAsync()
    {
        await KitchenCabinetsOption.ClickAsync();
    }

    /// <summary>
    /// Advances to the next wizard step using the Next button.
    /// </summary>
    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    /// <summary>
    /// Completes the kitchen elements selection step for TC-010.
    /// Selects kitchen cabinets and proceeds to the cabinet work question.
    /// </summary>
    public async Task CompleteAsync()
    {
        await SelectKitchenCabinetsAsync();
        await ContinueAsync();
    }
}