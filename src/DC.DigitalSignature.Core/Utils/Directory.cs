namespace DC.DigitalSignature.Core.Utils
{
    public static class DirectoryUtil
    {
        public static bool WriteLocalFile(string path, string fileName, string extension, string base64)
        {
            bool result = false;
            try
            {
                if (string.IsNullOrWhiteSpace(base64)) return result;

                File.WriteAllBytes(Path.Combine(path, $"{fileName}-SRC{extension}"), Convert.FromBase64String(base64));
                File.WriteAllBytes(Path.Combine(path, $"{fileName}-DEST{extension}"), Convert.FromBase64String(base64));

                return result = true;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public static bool DeleteLocalFile(string path, string fileName, string extension)
        {
            bool result = false;
            try
            {
                File.Delete(Path.Combine(path, $"{fileName}-SRC{extension}"));
                File.Delete(Path.Combine(path, $"{fileName}-DEST{extension}"));

                return result = true;
            }
            catch (Exception)
            {
                return result;
            }
        }
    }
}
