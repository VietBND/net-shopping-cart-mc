namespace Identity.Api.Models.Requests
{
    public class AccountLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
