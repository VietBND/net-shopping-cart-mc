namespace ApplicationUser.Domain.Models.Requests
{
    public class AuthLoginRequest : Request
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
