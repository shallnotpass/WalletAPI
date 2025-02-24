namespace Domain.Errors
{
    public record class Error
    {
        public Error(string message)
        {
            ErrorMessage = message;
        }
        public string? ErrorMessage { get; set; }
        public ErrorType type { get; set; }
    }
}
