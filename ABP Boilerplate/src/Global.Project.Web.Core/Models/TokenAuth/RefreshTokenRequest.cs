namespace Global.Project.Models.TokenAuth
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
    }
}
