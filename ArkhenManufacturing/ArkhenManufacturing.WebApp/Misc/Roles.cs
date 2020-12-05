namespace ArkhenManufacturing.WebApp.Misc
{
    public static class Roles
    {
        public const string AdminAndUser = Admin + "," + User;
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);
    }
}
