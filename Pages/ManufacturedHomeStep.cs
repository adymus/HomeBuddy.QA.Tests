using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

/// <summary>
/// Represents the manufactured home qualification step of the kitchen
/// remodeling wizard. Provides locators and actions for identifying
/// whether the property is a mobile, modular, or manufactured home.
/// </summary>
public class ManufacturedHomeStep
{
    private readonly IPage _page;

    public ManufacturedHomeStep(IPage page)
    {
        _page = page;
    }

    private ILocator Step =>
        _page.Locator("#StepBodyId");

    public ILocator Question =>
        Step.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Is it a mobile, modular or manufactured home?",
                Exact = true
            });

    public ILocator YesRadio =>
        Step.Locator(
            "[data-autotest-radio-internalmobilehome-yes]");

    public ILocator NoRadio =>
        Step.Locator(
            "[data-autotest-radio-internalmobilehome-no]");

    public ILocator YesOption =>
        Step.Locator(
            "[data-autotest-radio-internalmobilehome-yes] + label");

    public ILocator NoOption =>
        Step.Locator(
            "[data-autotest-radio-internalmobilehome-no] + label");

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

    /// <summary>
    /// Selects "Yes" to indicate that the property is a mobile,
    /// modular, or manufactured home.
    /// </summary>
    public async Task SelectYesAsync()
    {
        await YesOption.ClickAsync();
    }

    /// <summary>
    /// Selects "No" to indicate that the property is not a mobile,
    /// modular, or manufactured home.
    /// </summary>
    public async Task SelectNoAsync()
    {
        await NoOption.ClickAsync();
    }

    /// <summary>
    /// Advances to the next wizard step using the Next button.
    /// </summary>
    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    /// <summary>
    /// Completes the manufactured home qualification step for TC-010.
    /// Waits for the question to appear, selects "No",
    /// and proceeds to the homeowner authorization step.
    /// </summary>
    public async Task CompleteAsync()
    {
        await Question.WaitForAsync();
        await SelectNoAsync();
        await ContinueAsync();
    }
}