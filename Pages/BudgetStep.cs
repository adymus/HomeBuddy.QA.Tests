using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

/// <summary>
/// Represents the budget selection step of the kitchen remodeling wizard.
/// Provides locators and actions for selecting a budget range and
/// continuing to the next step.
/// </summary>
public class BudgetStep
{
    private readonly IPage _page;

    public BudgetStep(IPage page)
    {
        _page = page;
    }

    private ILocator Step =>
        _page.Locator(".budgetStep");

    public ILocator Question =>
        Step.GetByRole(
            AriaRole.Heading,
            new()
            {
                Name = "Do you know what your approximate budget is?",
                Exact = true
            });

    public ILocator UpTo5KOption =>
        Step.GetByText(
            "Up to $5K",
            new() { Exact = true });

    public ILocator Budget5KTo10KOption =>
        Step.GetByText(
            "$5K to $10K",
            new() { Exact = true });

    public ILocator Budget10KTo30KOption =>
        Step.GetByText(
            "$10K to $30K",
            new() { Exact = true });

    public ILocator MoreThan30KOption =>
        Step.GetByText(
            "More than $30K",
            new() { Exact = true });

    public ILocator NotSureOption =>
        Step.GetByText(
            "Not sure",
            new() { Exact = true });

    public ILocator Budget10KTo30KRadio =>
        Step.Locator(".budgetStep__grid .connectedCheckbox")
            .Filter(new() { HasText = "$10K to $30K" })
            .Locator("input[name='budget']");

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

    /// <summary>
    /// Selects the $10K to $30K budget range used in the
    /// end-to-end lead submission scenario.
    /// </summary>
    public async Task SelectBudget10KTo30KAsync()
    {
        await Budget10KTo30KOption.ClickAsync();
    }

    /// <summary>
    /// Attempts to select the "Not sure" budget option.
    /// Available for separate negative and exploratory test scenarios.
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
    /// Completes the budget selection step for TC-010.
    /// Waits for the budget question, selects the $10K to $30K
    /// range, and proceeds to the customer contact information step.
    /// </summary>
    public async Task CompleteAsync()
    {
        await Question.WaitForAsync();
        await SelectBudget10KTo30KAsync();
        await ContinueAsync();
    }
}