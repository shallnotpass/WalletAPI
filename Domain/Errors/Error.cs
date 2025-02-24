namespace Domain.Errors
{
    public record class Error
    {
        public Error(ErrorType type, string message)
        {
            this.ErrorMessage = message;
            this.type = type;
        }
        public string? ErrorMessage { get; set; }
        public ErrorType type { get; set; }
    }
}
