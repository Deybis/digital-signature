using DC.DigitalSignature.Core.Models;
using DC.DigitalSignature.Core.Utils;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Signatures;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;

namespace DC.DigitalSignature.Core.Service
{
    public class FileService : IFileService
    {
        public const string LocalFilePath = "LocalFilePath";

        public IConfiguration Configuration { get; }
        public FileService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ResponseModel Sign(SignatureModel model)
        {
            ResponseModel result = new ResponseModel();
            try
            {
                string signedDocument = string.Empty;
                var fileName = Guid.NewGuid().ToString();

                var imageBytes = Convert.FromBase64String(model.SignatureImage);
                var image = ImageDataFactory.Create(imageBytes);

                if (string.IsNullOrEmpty(model.File))
                {
                    result.Message = "Base64 del archivo es requerido";
                    return result;
                }

                string certificate = model.CertificatePath;
                char[] certificatePassword = model.CertificatePassword.ToCharArray();

                Pkcs12Store store = new(new FileStream(certificate, FileMode.Open, FileAccess.Read), certificatePassword);
                string alias = string.Empty;
                foreach (object a in store.Aliases)
                {
                    alias = (string)a;
                    if (store.IsKeyEntry(alias))
                    {
                        break;
                    }
                }
                ICipherParameters pk = store.GetKey(alias).Key;

                X509CertificateEntry[] ce = store.GetCertificateChain(alias);
                X509Certificate[] chain = new X509Certificate[ce.Length];
                for (int k = 0; k < ce.Length; ++k)
                {
                    chain[k] = ce[k].Certificate;
                }

                var path = Configuration.GetSection(LocalFilePath);
                DirectoryUtil.WriteLocalFile(path.Value!, fileName, model.Extension, model.File);

                string src = System.IO.Path.Combine(path.Value!, $"{fileName}-SRC{model.Extension}");
                string dest = System.IO.Path.Combine(path.Value!, $"{fileName}-DEST{model.Extension}");

                PdfReader reader = new(src);
                PdfSigner signer = new(reader, new FileStream(dest, FileMode.Create, FileAccess.Write), new StampingProperties());

                var renderinMode = ConvertUtil.GetEnumObjectByValue(model.SignatureMode);

                PdfSignatureAppearance appearance = signer.GetSignatureAppearance();
                appearance.SetReason(model.Reason)
                .SetLocation(model.Location)
                .SetPageRect(new Rectangle(model.CoordX, model.CoordY, model.Width, model.Height))
                .SetPageNumber(model.PageNumber)
                .SetRenderingMode(renderinMode)
                .SetSignatureGraphic(image);

                signer.SetFieldName(model.UserName);

                IExternalSignature pks = new PrivateKeySignature(pk, DigestAlgorithms.SHA256);
                signer.SignDetached(pks, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);

                var ds = File.ReadAllBytes(dest);
                signedDocument = Convert.ToBase64String(ds);

                DirectoryUtil.DeleteLocalFile(path.Value!, fileName, model.Extension);

                result.Value = signedDocument;
                result.IsSuccess = true;
                return result;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

    }
}
