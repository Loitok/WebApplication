namespace WebApplication.BLL.ResultConstants
{
    public static class AuthServiceResultConstants
    {

        public const string UserAlreadyExists = "USER_EXISTS";

        public const string UserNotFound = "USER_NOT_FOUND";

        public const string InvalidUserNameOrPassword = "IVALID_USERNAME_OR_PASSWORD";

        public const string InvalidResetPasswordToken = "IVALID_RESET_PASSWORD_TOKEN";

        public const string InvalidRefreshToken = "INVALID_REFRESH_TOKEN";

        public const string ExpiredResetPasswordToken = "EXPIRED_RESET_TOKEN";
    }
}
