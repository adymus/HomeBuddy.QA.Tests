using Microsoft.Playwright.NUnit;

namespace HomeBuddy.QA.Tests.Setup;

public abstract class BaseTest : PageTest
{
    protected string AppUrl =>
        "https://hb-test.stage.sirenltd.dev/";
}