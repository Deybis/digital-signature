using SysX509 = System.Security.Cryptography.X509Certificates;


namespace DC.DigitalSignature.Core.Models
{
    public class SignModel
    {
        public string Source { get; set; } = string.Empty;
        public string Target { get; set; } = string.Empty;
        public SysX509.X509Certificate2? Certificate { get; set; }

        public string Reason { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public bool AddVisibleSign { get; set; }
    }
}
