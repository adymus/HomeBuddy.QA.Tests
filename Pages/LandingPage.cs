using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages;

/// <summary>
/// Represents the HomeBuddy Kitchen Remodeling landing page.
/// Provides locators and actions for accessing the application,
/// entering a ZIP code, and starting the remodeling estimate wizard.
/// </summary>
public class LandingPage
{
    private readonly IPage _page;

    public LandingPage(IPage page)
    {
        _page = page;
    }

    public ILocator PageTitle =>
        _page.Locator(".header__title");

    public ILocator KitchenRemodelingSection =>
        _page.GetByText(
            "Complete Kitchen Remodeling",
            new() { Exact = true });

    public ILocator HeaderZipForm =>
        _page.Locator("#zip_header");

    public ILocator ZipCodeInput =>
        HeaderZipForm.Locator("#zipCode");

    public ILocator GetEstimateButton =>
        HeaderZipForm.Locator(
            "[data-autotest-button-submit-0]");

    /// <summary>
    /// Navigates to the HomeBuddy Kitchen Remodeling landing page.
    /// </summary>
    /// <param name="url">Application URL configured in the runsettings file.</param>
    public async Task OpenAsync(string url)
    {
        await _page.GotoAsync(url);
    }

    /// <summary>
    /// Enters the customer's ZIP code into the main landing page form.
    /// </summary>
    /// <param name="zipCode">ZIP code used to request a remodeling estimate.</param>
    public async Task EnterZipCodeAsync(string zipCode)
    {
        await ZipCodeInput.FillAsync(zipCode);
    }

    /// <summary>
    /// Submits the ZIP code form to begin the remodeling estimate wizard.
    /// </summary>
    public async Task ClickGetEstimateAsync()
    {
        await GetEstimateButton.ClickAsync();
    }

    /// <summary>
    /// Starts the estimate journey by entering the ZIP code and
    /// submitting the landing page form.
    /// Used as the entry point for the TC-010 end-to-end scenario.
    /// </summary>
    /// <param name="zipCode">ZIP code with available contractor coverage.</param>
    public async Task StartEstimateAsync(string zipCode)
    {
        await EnterZipCodeAsync(zipCode);
        await ClickGetEstimateAsync();
    }
}