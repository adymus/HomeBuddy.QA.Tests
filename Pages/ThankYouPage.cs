using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages;

/// <summary>
/// Represents the Thank You page displayed after a successful
/// kitchen remodeling lead submission.
///
/// Provides locators and methods for verifying the final confirmation
/// and retrieving the customer name displayed in the confirmation message.
///
/// Related defect: HB-005 - Incorrect customer name on Thank You page.
/// </summary>
public class ThankYouPage
{
    private readonly IPage _page;

    public ThankYouPage(IPage page)
    {
        _page = page;
    }

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

    /// <summary>
    /// Waits for navigation to the Thank You page and verifies that
    /// the confirmation heading is visible before continuing.
    /// </summary>
    public async Task WaitUntilLoadedAsync()
    {
        await _page.WaitForURLAsync("**/thank-you");

        await ConfirmationHeading.WaitForAsync();
    }

    /// <summary>
    /// Retrieves the confirmation message displayed after lead submission.
    /// Used by TC-010 to verify that the customer's first name matches
    /// the name entered during the contact information step.
    /// </summary>
    /// <returns>
    /// The confirmation heading text displayed on the Thank You page.
    /// </returns>
    public async Task<string> GetConfirmationMessageAsync()
    {
        return await ConfirmationHeading.InnerTextAsync();
    }
}