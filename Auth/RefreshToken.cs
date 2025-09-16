namespace Tibaks_Backend.Auth
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }

        public string? CreatedByIp { get; set; }
        public bool IsRevoked { get; set; }


    }
}
