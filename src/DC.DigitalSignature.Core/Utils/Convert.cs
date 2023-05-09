using iText.Signatures;

namespace DC.DigitalSignature.Core.Utils
{
    public static class ConvertUtil
    {
        public static PdfSignatureAppearance.RenderingMode GetEnumObjectByValue(int valueId)
        {
            return (PdfSignatureAppearance.RenderingMode)Enum.ToObject(typeof(PdfSignatureAppearance.RenderingMode), valueId);
        }
    }
}
