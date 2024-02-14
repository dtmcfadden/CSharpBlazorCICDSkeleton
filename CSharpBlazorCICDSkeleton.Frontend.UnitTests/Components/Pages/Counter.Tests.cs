using Bunit;
using CSharpBlazorCICDSkeleton.Frontend.Components.Pages;

namespace CSharpBlazorCICDSkeleton.Frontend.UnitTests.Components.Pages;
public class CounterTests
{
    [Fact]
    public void OnCounterClick_WhenClickCounterButton_ShouldIncrementByOne()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Counter>();

        var clickMeBtn = cut.Find("button[aria-label='Click Me']");
        var currentCountP = cut.Find("p[aria-label='Current count']");

        // Assert Initial
        currentCountP.TextContent.MarkupMatches("Current count: 0");

        // Act
        clickMeBtn.Click();

        // Assert After
        currentCountP.TextContent.MarkupMatches("Current count: 1");
    }
}
