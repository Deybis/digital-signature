
using DC.DigitalSignature.Core.Models;

namespace DC.DigitalSignature.Core
{
    public interface IFileService
    {
        ResponseModel Sign(SignatureModel model);
    }
}
