using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

/// <summary>
/// Represents the homeowner authorization step of the kitchen remodeling wizard.
/// Provides locators and actions for confirming whether the customer owns
/// the property or is authorized to make property changes.
/// </summary>
public class HomeownerStep
{
    private readonly IPage _page;

    public HomeownerStep(IPage page)
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
                Name = "Are you the homeowner or authorized to make property changes?",
                Exact = true
            });

    public ILocator YesRadio =>
        Step.Locator(
            "[data-autotest-radio-owner-yes]");

    public ILocator NoRadio =>
        Step.Locator(
            "[data-autotest-radio-owner-no]");

    public ILocator YesOption =>
        Step.Locator(
            "[data-autotest-radio-owner-yes] + label");

    public ILocator NoOption =>
        Step.Locator(
            "[data-autotest-radio-owner-no] + label");

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-button-next]");

    /// <summary>
    /// Selects "Yes" to confirm that the customer is the homeowner
    /// or is authorized to make changes to the property.
    /// </summary>
    public async Task SelectYesAsync()
    {
        await YesOption.ClickAsync();
    }

    /// <summary>
    /// Selects "No" for scenarios where the customer does not have
    /// ownership or authorization to make property changes.
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
    /// Completes the homeowner authorization step for TC-010.
    /// Waits for the authorization question, selects "Yes",
    /// and proceeds to the kitchen size selection step.
    /// </summary>
    public async Task CompleteAsync()
    {
        await Question.WaitForAsync();
        await SelectYesAsync();
        await ContinueAsync();
    }
}