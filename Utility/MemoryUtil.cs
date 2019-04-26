#if IMOET_INCLUDE_UTILITY || IMOET_INCLUDE_LIBRARYMANAGER
namespace Imoet.Utility
{
    using System;
    using System.IO;
    public static class MemoryUtil
    {
        public static byte[] GetStreamBytes(Stream stream) {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream()) {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0) {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
#endif