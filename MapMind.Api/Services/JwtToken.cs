namespace MapMind.Api.Services
{
    public class JwtToken
    {
        public string Token { get; set; }
        public long ExpiresIn { get; set; }
    }
}