namespace CSharpBlazorCICDSkeleton.Frontend.Common.ErrorMessages;

public static class FormErrorMessages
{
    public const string UserNameRequired = "User name is required";
    public const string MessageRequired = "Message is required";

    public const string StringLength3To15 = "Length must be between 3 and 15";
    public const string StringLength1To50 = "Length must be between 1 and 50";

    // L = Letters
    // N = Numbers
    // SC = Special Characters

    public const string StringAllowedLN = "Only letters and numbers are allowed";
    public const string StringAllowedLNSC = "Only letters, numbers, and (.,!?) are allowed";
}
