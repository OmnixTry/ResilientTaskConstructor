namespace AdaptiveEnglishTrainer.Authorization.Dto
{
    public class RegistrationSuccessResponseDto
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
