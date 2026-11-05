using Microsoft.Playwright;

namespace HomeBuddy.QA.Tests.Pages.Wizard;

/// <summary>
/// Represents the customer contact information step of the kitchen remodeling wizard.
/// Provides locators and actions for entering the customer's full name and email
/// address before proceeding to the phone number step.
/// </summary>
public class ContactInfoStep
{
    private readonly IPage _page;

    public ContactInfoStep(IPage page)
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
                Name = "Who should I prepare this estimate for?",
                Exact = true
            });

    public ILocator FullNameInput =>
        Step.Locator(
            "[data-autotest-input-fullname-text]");

    public ILocator EmailInput =>
        Step.Locator(
            "[data-autotest-input-email-text]");

    public ILocator NextButton =>
        Step.Locator(
            "[data-autotest-button-submit-next]");

    /// <summary>
    /// Enters the customer's full name in the contact information form.
    /// </summary>
    /// <param name="fullName">Customer's full name.</param>
    public async Task EnterFullNameAsync(string fullName)
    {
        await FullNameInput.FillAsync(fullName);
    }

    /// <summary>
    /// Enters the customer's email address in the contact information form.
    /// </summary>
    /// <param name="email">Customer's email address.</param>
    public async Task EnterEmailAsync(string email)
    {
        await EmailInput.FillAsync(email);
    }

    /// <summary>
    /// Advances to the phone number step using the Next button.
    /// </summary>
    public async Task ContinueAsync()
    {
        await NextButton.ClickAsync();
    }

    /// <summary>
    /// Completes the contact information step for TC-010.
    /// Waits for the form to appear, enters the customer's full name and
    /// email address, and proceeds to the phone number step.
    /// </summary>
    /// <param name="fullName">Full name used for the lead submission.</param>
    /// <param name="email">Email address used for the lead submission.</param>
    public async Task CompleteAsync(
        string fullName,
        string email)
    {
        await Question.WaitForAsync();

        await EnterFullNameAsync(fullName);
        await EnterEmailAsync(email);

        await ContinueAsync();
    }
}
