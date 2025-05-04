namespace LibraryAPI.Application.Dtos
{
    public class ErrorResponseDto(string message, string details,int statusCode)
    {
        public string Message { get; set; } = message;
        public string Details { get; set; } = details;
        public int StatusCode { get; set; } = statusCode;
    }
    
}
