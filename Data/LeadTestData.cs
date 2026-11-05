
using NUnit.Framework;

namespace HomeBuddy.QA.Tests.Data;

public class LeadTestData
{
    public string ZipCode { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string KitchenSize { get; init; } = "120";

    public string FirstName =>
        FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];

    /// <summary>
    /// Loads customer test data from HomeBuddy.runsettings.
    /// </summary>
    public static LeadTestData Create()
    {
        return new LeadTestData
        {
            ZipCode = GetRequiredParameter("HB_TEST_ZIP"),
            FullName = GetRequiredParameter("HB_TEST_FULL_NAME"),
            Email = GetRequiredParameter("HB_TEST_EMAIL"),
            PhoneNumber = GetRequiredParameter("HB_TEST_PHONE")
        };
    }

    private static string GetRequiredParameter(string name)
    {
        var value = TestContext.Parameters.Get(name, string.Empty);

        if (string.IsNullOrWhiteSpace(value) ||
            value.StartsWith("YOUR_", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                $"Missing or invalid test configuration: {name}. " +
                "Check HomeBuddy.runsettings.");
        }

        return value;
    }
}