namespace MapMind.Api.Response.Auth
{
    public class InvalidCredentialsResponse : BadRequestResponse
    {
        public override string Message => "Invalid email/password.";
    }
}