
using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

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

    public async Task SelectKitchenCabinetsAsync()
    {
        await KitchenCabinetsOption.ClickAsync();
    }

    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    public async Task CompleteAsync()
    {
        await SelectKitchenCabinetsAsync();
        await ContinueAsync();
    }
}
