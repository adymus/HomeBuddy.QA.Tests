
using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

public class BudgetStep
{
    private readonly IPage _page;

    public BudgetStep(IPage page)
    {
        _page = page;
    }

    // ==========================================
    // STEP 7 - Budget Selection
    // ==========================================

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

    // Budget options - visible labels

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

    // Selected radio - useful for future assertions

    public ILocator Budget10KTo30KRadio =>
        Step.Locator(".budgetStep__grid .connectedCheckbox")
            .Filter(new() { HasText = "$10K to $30K" })
            .Locator("input[name='budget']");

    // Next button

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

    // ==========================================
    // Actions
    // ==========================================

    public async Task SelectBudget10KTo30KAsync()
    {
        await Budget10KTo30KOption.ClickAsync();
    }

    public async Task SelectNotSureAsync()
    {
        await NotSureOption.ClickAsync();
    }

    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    // Main E2E happy-path action

    public async Task CompleteAsync()
    {
        await Question.WaitForAsync();
        await SelectBudget10KTo30KAsync();
        await ContinueAsync();
    }
}