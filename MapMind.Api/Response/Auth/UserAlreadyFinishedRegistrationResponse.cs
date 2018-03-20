namespace MapMind.Api.Response.Auth
{
    public class UserAlreadyFinishedRegistrationResponse : BadRequestResponse
    {
        public override string Message => "User already finished registration.";
    }
}