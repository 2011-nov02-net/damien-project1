namespace ArkhenManufacturing.WebApp.Misc {

    /// <summary>
    /// Static class that contains Regular Expressions that are used by the attributes
    ///     on the ViewModels
    /// </summary>
    public static class RegularExpressions 
    {
        public const string NumbersOnly = @"^[0-9]*$";
        public const string NoSpecialCharacters = @"^[A-Za-z0-9 '-,.#]*$";
        public const string NameCharacters = @"^[A-Za-z0-9 -]*$";
        public const string UserNameCharacters = @"^[A-Za-z0-9]*$";
        // TODO: Fill out the password characters regex
        public const string PasswordCharacters = @"^[A-Za-z0-9!@#$%&-_]{8,}$";
        // TODO: Fill out the email characters regex
        public const string EmailCharacters = @"^[a-zA-Z]+[a-zA-Z0-9\.]*\@[a-zA-Z]+[a-zA-Z0-9]+\.([a-zA-Z]+[a-zA-Z0-9]+){0,253}";
        // TODO: Fill out phone number characters regex
        public const string PhoneNumber = @"^(\+[0-9]{0,3})?[ -]?[0-9]{3}[ -]?[0-9]{3}[ -]?[0-9]{4}";
    }

}