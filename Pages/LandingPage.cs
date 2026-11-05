using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages;

public class LandingPage
{
    private readonly IPage _page;

    public LandingPage(IPage page)
    {
        _page = page;
    }

    // Page elements

    public ILocator PageTitle =>
        _page.Locator(".header__title");

    public ILocator KitchenRemodelingSection =>
        _page.GetByText(
            "Complete Kitchen Remodeling",
            new() { Exact = true }
        );

    // Main ZIP code form (hero section)

    public ILocator HeaderZipForm =>
        _page.Locator("#zip_header");

    public ILocator ZipCodeInput =>
        HeaderZipForm.Locator("#zipCode");

    public ILocator GetEstimateButton =>
        HeaderZipForm.Locator(
            "[data-autotest-button-submit-0]"
        );

    // Page actions

    public async Task OpenAsync(string url)
    {
        await _page.GotoAsync(url);
    }

    public async Task EnterZipCodeAsync(string zipCode)
    {
        await ZipCodeInput.FillAsync(zipCode);
    }

    public async Task ClickGetEstimateAsync()
    {
        await GetEstimateButton.ClickAsync();
    }

    // Reusable action: start the remodeling wizard

    public async Task StartEstimateAsync(string zipCode)
    {
        await EnterZipCodeAsync(zipCode);
        await ClickGetEstimateAsync();
    }
}