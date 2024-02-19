using AngleSharp.Dom;
using Bunit;
using CSharpBlazorCICDSkeleton.Frontend.Common.ErrorMessages;
using CSharpBlazorCICDSkeleton.Frontend.Components.Pages;
using CSharpBlazorCICDSkeleton.Frontend.UnitTests.CommonTestHelper;
using Microsoft.AspNetCore.SignalR;

namespace CSharpBlazorCICDSkeleton.Frontend.UnitTests.Components.Pages;
public class ChatTests
{
    [Theory]
    [InlineData("TestUser", "TestMessage")]
    public void OnMessageAdded_ShouldAddMessageToMessageTable(string user, string message)
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Chat>();

        var messagesListTbody = cut.Find("#messagesList > tbody");

        // Assert Initial
        Assert.Equal(0, (int)messagesListTbody.GetElementsByTagName("tr").Length);

        // Act
        cut.InvokeAsync(() => cut.Instance.AddMessage(user, message));

        // Assert After
        var trList = messagesListTbody.GetElementsByTagName("tr");
        Assert.Equal(1, trList.Length);

        var msgTdUser = cut.Find("#messagesList > tbody > tr:first-child > td[name='msgUser']");
        Assert.Equal(user, msgTdUser.TextContent);

        var msgTdMsg = cut.Find("#messagesList > tbody > tr:first-child > td[name='msgMessage']");
        Assert.Equal(message, msgTdMsg.TextContent);
    }

    [Theory]
    [InlineData(" ", new string[] { FormErrorMessages.UserNameRequired })]
    [InlineData("avaAVA09", new string[] { })]
    [InlineData("a", new[] { FormErrorMessages.StringLength3To15 })]
    [InlineData(TestStrings.LongStringLUN, new[] { FormErrorMessages.StringLength3To15 })]
    [InlineData("a!", new[] {
        FormErrorMessages.StringLength3To15,
        FormErrorMessages.StringAllowedLN
    })]
    [InlineData("aaa!", new[] { FormErrorMessages.StringAllowedLN })]
    public void OnUserInput_WhenUserInputIsValidated_ShouldFailValidation(string user, string[] errorMsg)
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Chat>();

        var userInputEl = cut.Find("#userInput");
        userInputEl.Change(user);
        var userFormGroup = userInputEl.Parent;

        // Assert Initial
        cut.InvokeAsync(() => cut.Instance.ValidateForm());

        // Arrange After Initial
        var userFormGroupChildren = userFormGroup!.ChildNodes;
        var validationMsg = userFormGroupChildren.GetElementsByClassName("validation-message");

        // Act
        Assert.Equal(errorMsg.Length, validationMsg.Length);

        if (errorMsg.Length == validationMsg.Length && validationMsg.Length > 0)
        {
            for (int i = 0; i < validationMsg.Length; i++)
            {
                Assert.Equal(validationMsg[i].TextContent, errorMsg[i]);
            }
        }
    }

    [Theory]
    [InlineData("ava 0 ava !,.?", new string[] { })]
    [InlineData(" ", new[] { FormErrorMessages.MessageRequired })]
    [InlineData(TestStrings.LongStringLUN,
        new[] { FormErrorMessages.StringLength1To50 })]
    [InlineData(TestStrings.LongStringLUN + "#$%", new[] {
        FormErrorMessages.StringLength1To50,
        FormErrorMessages.StringAllowedLNSC
    })]
    [InlineData("A#$%", new[] { FormErrorMessages.StringAllowedLNSC })]
    public void OnMessageInput_WhenMessageInputIsValidated_ShouldFailValidation(string message, string[] errorMsg)
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<Chat>();

        var msgInputEl = cut.Find("#messageInput");
        msgInputEl.Change(message);
        var msgFormGroup = msgInputEl.Parent;

        // Assert Initial
        cut.InvokeAsync(() => cut.Instance.ValidateForm());

        // Arrange After Initial
        var msgFormGroupChildren = msgFormGroup!.ChildNodes;
        var validationMsg = msgFormGroupChildren.GetElementsByClassName("validation-message");

        // Act
        Assert.Equal(errorMsg.Length, validationMsg.Length);

        if (errorMsg.Length == validationMsg.Length && validationMsg.Length > 0)
        {
            for (int i = 0; i < validationMsg.Length; i++)
            {
                Assert.Equal(validationMsg[i].TextContent, errorMsg[i]);
            }
        }
    }

    //[Theory]
    //[InlineData(" ", new string[] { FormErrorMessages.UserNameRequired },
    //    " ", new string[] { FormErrorMessages.MessageRequired })]
    //public void OnFormSubmit_WhenMessageInputIsValidated_ShouldFailValidation(string user, string[] userErrorMsg, string message, string[] messageErrorMsg)
    //{
    //    // Arrange
    //    using var ctx = new TestContext();
    //    var cut = ctx.RenderComponent<Chat>();

    //    var formEl = cut.Find("form");

    //    var userInputEl = cut.Find("#userInput");
    //    //userInputEl.Change("");
    //    userInputEl.Change(user);
    //    var userFormGroup = userInputEl.Parent;

    //    var msgInputEl = cut.Find("#messageInput");
    //    //msgInputEl.Change("");
    //    msgInputEl.Change(message);
    //    var msgFormGroup = msgInputEl.Parent;

    //    //var formSubmitBtn = cut.Find("#formSubmit");

    //    // Assert Initial
    //    cut.InvokeAsync(() => cut.Instance.ValidateForm());
    //    //cut.InvokeAsync(() => formEl.RemoveEventListener("submit"));
    //    //formSubmitBtn.Click();

    //    // Arrange After Initial
    //    var userFormGroupChildren = userFormGroup!.ChildNodes;
    //    var userValidationMsg = userFormGroupChildren.GetElementsByClassName("validation-message");

    //    var msgFormGroupChildren = msgFormGroup!.ChildNodes;
    //    var msgValidationMsg = msgFormGroupChildren.GetElementsByClassName("validation-message");

    //    // Act
    //    Assert.Equal(userErrorMsg.Length, userValidationMsg.Length);
    //    Assert.Equal(messageErrorMsg.Length, msgValidationMsg.Length);

    //    if (userErrorMsg.Length == userValidationMsg.Length && userValidationMsg.Length > 0)
    //    {
    //        for (int i = 0; i < userValidationMsg.Length; i++)
    //        {
    //            Assert.Equal(userValidationMsg[i].TextContent, userErrorMsg[i]);
    //        }
    //    }

    //    if (messageErrorMsg.Length == msgValidationMsg.Length && msgValidationMsg.Length > 0)
    //    {
    //        for (int i = 0; i < msgValidationMsg.Length; i++)
    //        {
    //            Assert.Equal(msgValidationMsg[i].TextContent, messageErrorMsg[i]);
    //        }
    //    }
    //}

    //[Fact]
    //public async Task HubsAreMockableViaType()
    //{
    //    var hub = new ChatHub();
    //    var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
    //    var all = new Mock<IClientContract>();

    //    hub.Clients = (IHubCallerClients)mockClients.Object;
    //    all.Setup(m => m.SendMessage(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
    //    mockClients.Setup(m => m.All).Returns(all.Object);
    //    await hub.SendMessage("foo", "bar");

    //    all.VerifyAll();
    //}
}
