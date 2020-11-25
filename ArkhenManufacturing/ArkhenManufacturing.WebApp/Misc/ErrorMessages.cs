namespace ArkhenManufacturing.WebApp.Misc
{
    /// <summary>
    /// Static class that contains error messages that are used by the attributes
    ///     on the ViewModels
    /// </summary>
    public static class ErrorMessages
    {
        public const string NumbersOnly = "Numbers between 0-9 are only allows";
        public const string NoSpecialCharacters = "Special characters aren't accepted";
        public const string NameCharacters = "Alphabet, spaces, and hyphens only";
        public const string PasswordCharacters = "Special characters and alphanumeric characters only";
        public const string EmailCharacters = "<username>@<domain>.<top level domain>";
        public const string PhoneNumber = @"[+###] (###) ###-####";
    }
}
