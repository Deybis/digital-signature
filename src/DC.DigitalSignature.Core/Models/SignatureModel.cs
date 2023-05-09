namespace DC.DigitalSignature.Core.Models
{
    public class SignatureModel
    {
        public string File { get; set; } = string.Empty;
        public string SignatureImage { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string CertificatePath { get; set; } = string.Empty;
        public string CertificatePassword { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public float CoordX { get; set; } = 0;
        public float CoordY { get; set; } = 0;
        public float Width { get; set; } = 0;
        public float Height { get; set; } = 0;
        public int SignatureMode { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
    }

    public enum SignatureMode
    {
        Description = 0,
        NameAndDescription = 1,
        ImageAndDescription = 2,
        Image = 3
    }
}
