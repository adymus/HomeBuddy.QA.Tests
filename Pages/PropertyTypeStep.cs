
using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

public class PropertyTypeStep
{
    private readonly IPage _page;

    public PropertyTypeStep(IPage page)
    {
        _page = page;
    }

    // ==========================================
    // STEP 3 - Property Type
    // ==========================================

    private ILocator Step =>
        _page.Locator(".typeOfProperty");

    public ILocator Question =>
        Step.Locator("h4");

    // Radio inputs - for checking selection state

    public ILocator SingleFamilyRadio =>
        Step.Locator(
            "[data-autotest-radio-propertytype-single]");

    public ILocator MultiFamilyRadio =>
        Step.Locator(
            "[data-autotest-radio-propertytype-multi]");

    public ILocator CommercialRadio =>
        Step.Locator(
            "[data-autotest-radio-propertytype-commercial]");

    // Visible options - for user interactions

    public ILocator SingleFamilyOption =>
        Step.GetByText(
            "Single family home",
            new() { Exact = true });

    public ILocator MultiFamilyOption =>
        Step.GetByText(
            "Multi-family home",
            new() { Exact = true });

    public ILocator CommercialOption =>
        Step.GetByText(
            "Commercial",
            new() { Exact = true });

    // Next button

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

    // ==========================================
    // Actions
    // ==========================================

    public async Task SelectSingleFamilyAsync()
    {
        await SingleFamilyOption.ClickAsync();
    }

    public async Task SelectMultiFamilyAsync()
    {
        await MultiFamilyOption.ClickAsync();
    }

    public async Task SelectCommercialAsync()
    {
        await CommercialOption.ClickAsync();
    }

    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    // Main E2E happy-path action

    public async Task CompleteAsync()
    {
        await SelectSingleFamilyAsync();
        await ContinueAsync();
    }
}