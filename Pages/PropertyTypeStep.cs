using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

/// <summary>
/// Represents the property type selection step of the kitchen remodeling wizard.
/// Provides locators and actions for identifying the customer's property type
/// before proceeding to the manufactured home qualification step.
/// </summary>
public class PropertyTypeStep
{
    private readonly IPage _page;

    public PropertyTypeStep(IPage page)
    {
        _page = page;
    }

    private ILocator Step =>
        _page.Locator(".typeOfProperty");

    public ILocator Question =>
        Step.Locator("h4");

    public ILocator SingleFamilyRadio =>
        Step.Locator(
            "[data-autotest-radio-propertytype-single]");

    public ILocator MultiFamilyRadio =>
        Step.Locator(
            "[data-autotest-radio-propertytype-multi]");

    public ILocator CommercialRadio =>
        Step.Locator(
            "[data-autotest-radio-propertytype-commercial]");

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

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

    /// <summary>
    /// Selects "Single family home" as the property type.
    /// This is the option used in the TC-010 lead submission scenario.
    /// </summary>
    public async Task SelectSingleFamilyAsync()
    {
        await SingleFamilyOption.ClickAsync();
    }

    /// <summary>
    /// Selects "Multi-family home" as the property type.
    /// Available for additional property qualification scenarios.
    /// </summary>
    public async Task SelectMultiFamilyAsync()
    {
        await MultiFamilyOption.ClickAsync();
    }

    /// <summary>
    /// Selects "Commercial" as the property type.
    /// Available for testing commercial property qualification behavior.
    /// </summary>
    public async Task SelectCommercialAsync()
    {
        await CommercialOption.ClickAsync();
    }

    /// <summary>
    /// Advances to the next wizard step using the Next button.
    /// </summary>
    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    /// <summary>
    /// Completes the property type selection step for TC-010.
    /// Waits for the property question, selects "Single family home",
    /// and proceeds to the manufactured home qualification step.
    /// </summary>
    public async Task CompleteAsync()
    {
        await Question.WaitForAsync();
        await SelectSingleFamilyAsync();
        await ContinueAsync();
    }
}