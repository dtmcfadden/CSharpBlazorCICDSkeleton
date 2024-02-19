using CSharpBlazorCICDSkeleton.Frontend.Common.ErrorMessages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.ComponentModel.DataAnnotations;

namespace CSharpBlazorCICDSkeleton.Frontend.Components.Pages;

public partial class Chat
{
    [Inject]
    private NavigationManager? Navigation { get; set; }

    private ChatModel ChatModel = new();

    private EditContext? editContext;
    private HubConnection? hubConnection;
    private List<(string user, string message)> messages = [];
    private bool sendingMessage = false;

    public bool IsConnected =>
        hubConnection?.State == HubConnectionState.Connected;

    private InputText? UserInputRef;
    private InputText? MessageRef;

    protected override void OnInitialized()
    {
        editContext = new(ChatModel);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            UserInputRef.Element.Value.FocusAsync();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl(Navigation.ToAbsoluteUri("/chathub"))
            .Build();

        hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            AddMessage(user, message);
        });

        await hubConnection.StartAsync();
    }

    public void AddMessage(string user, string message)
    {
        messages.Add((user, message));
        InvokeAsync(StateHasChanged);
    }

    private async Task Submit()
    {
        if (editContext is not null
            && ValidateForm()
            && hubConnection is not null)
        {
            sendingMessage = true;

            await hubConnection.SendAsync("SendMessage", UserInputRef.Value, MessageRef.Value);

            ChatModel.Message = "";

            MessageRef.Element.Value.FocusAsync();

            sendingMessage = false;
        }
    }

    public bool ValidateForm()
    {
        return editContext.Validate();
    }

    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            await hubConnection.DisposeAsync();
        }
    }
}

public class ChatModel
{
    [Required(ErrorMessage = FormErrorMessages.UserNameRequired)]
    [Length(3, 15, ErrorMessage = FormErrorMessages.StringLength3To15)]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = FormErrorMessages.StringAllowedLN)]
    public string User { get; set; } = "";

    [Required(ErrorMessage = FormErrorMessages.MessageRequired)]
    [Length(1, 50, ErrorMessage = FormErrorMessages.StringLength1To50)]
    [RegularExpression(@"^[a-zA-Z0-9\s!,.?]+$", ErrorMessage = FormErrorMessages.StringAllowedLNSC)]
    public string Message { get; set; } = "";
}