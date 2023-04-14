namespace AdaptiveEnglishTrainer.Authorization.Entity
{
    public class AuthenticationResponseDto
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
