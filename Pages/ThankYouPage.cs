
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace HomeBuddy.QA.Tests.Pages;

public class ThankYouPage
{
    private readonly IPage _page;

    public ThankYouPage(IPage page)
    {
        _page = page;
    }

    // ==========================================
    // Thank You Page - Locators
    // ==========================================

    private ILocator Step =>
        _page.Locator("#StepBodyId");

    public ILocator ConfirmationHeading =>
        Step.GetByRole(
            AriaRole.Heading,
            new()
            {
                NameRegex = new Regex(@"^Thank you\b")
            });

    public ILocator CallReminder =>
        Step.GetByText(
            "Please don't miss the call",
            new() { Exact = false });

    // ==========================================
    // Page Actions / Data Retrieval
    // ==========================================

    public async Task WaitUntilLoadedAsync()
    {
        await _page.WaitForURLAsync("**/thank-you");

        await ConfirmationHeading.WaitForAsync();
    }

    public async Task<string> GetConfirmationMessageAsync()
    {
        return await ConfirmationHeading.InnerTextAsync();
    }
}