namespace DC.DigitalSignature.Core.Models
{
    public class ResponseModel
    {
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;
        public string Value { get; set; } = string.Empty;
    }
}
