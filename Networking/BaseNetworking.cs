//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
#if IMOET_INCLUDE_NETWORKING
namespace Imoet.Networking
{
    using System.IO;
    public enum HttpRequestMethod
    {
        POST,
        GET,
        HEAD,
        PUT,
        PATCH,
        DELETE,
        COPY,
        OPTIONS,
        LINK
    }
    public delegate void OnRESTResponse(bool status, string message, StreamReader stream);
    public delegate void OnRESTResponse2(bool status, string message, byte[] data);

    public class RESTFileInput {
        public string fileName;
        public string filePath;
        public string fileMime;
    }
}
#endif