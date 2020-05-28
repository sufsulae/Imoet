//Imoet Library
//Copyright © 2020 Yusuf Sulaeman
#if IMOET_INCLUDE_NETWORKING
namespace Imoet.Networking
{
    using Imoet.Threading;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using NetWebRequest = System.Net.WebRequest;

    public class WebRequestResponseData {
        public bool isSuccess;
        public int code;
        public byte[] data;
    }

    public delegate void WebRequestResponse(WebRequestResponseData data);

    public static class WebRequest
    {
        public static WebRequestResponseData POST(string uri, Dictionary<string, string> form) {
            return _req(uri, HttpRequestMethod.POST, form);
        }

        public static void POST(string uri, Dictionary<string, string> form, WebRequestResponse OnComplete) {
            _req(uri, HttpRequestMethod.POST, form, OnComplete);
        }

        public static WebRequestResponseData GET(string uri, Dictionary<string, string> form) {
            return _req(uri, HttpRequestMethod.GET, form);
        }

        public static void GET(string uri, Dictionary<string, string> form, WebRequestResponse OnComplete) {
            _req(uri, HttpRequestMethod.GET, form, OnComplete);
        }

        private static void _req(string uri, HttpRequestMethod method, Dictionary<string, string> form, WebRequestResponse webReqResponse) {
            var webReq = (HttpWebRequest)NetWebRequest.Create(uri);
            webReq.Method = method.ToString();
            webReq.ContentType = "application/x-www-form-urlencoded";

            var reqData = Encoding.UTF8.GetBytes(_flattenForm(form));
            webReq.ContentLength = reqData.Length;

            Worker.StartWorker((out object result) =>
            {
                try
                {
                    if (webReq.Method != "GET") {
                        using (var stream = webReq.GetRequestStream()) {
                            stream.Write(reqData, 0, reqData.Length);
                        }
                    }

                    var response = (HttpWebResponse)webReq.GetResponse();
                    var webResponseData = new WebRequestResponseData();
                    bool success = ((int)response.StatusCode) >= 200 && ((int)response.StatusCode) < 300;
                    webResponseData.isSuccess = success;
                    webResponseData.code = (int)response.StatusCode;
                    webResponseData.data = new byte[0];
                    if (response != null) {
                        using (var mem = new MemoryStream())
                        {
                            using (var stream = response.GetResponseStream())
                            {
                                while (true)
                                {
                                    var data = stream.ReadByte();
                                    if (data == -1)
                                        break;
                                    mem.WriteByte((byte)data);
                                }
                                webResponseData.data = mem.ToArray();
                            }
                        }
                    }
                    result = webResponseData;
                    response.Close();
                }
                catch (WebException e)
                {
                    var response = (HttpWebResponse)e.Response;
                    var webResponseData = new WebRequestResponseData();
                    webResponseData.isSuccess = false;
                    webResponseData.code = response != null ? (int)response.StatusCode : 404;
                    webResponseData.data = new byte[0];
                    result = webResponseData;
                    response?.Close();
                }
                return true;
            }, (e, result) => {
                if (e != null && !string.IsNullOrEmpty(e.Message)) {
                    Console.WriteLine("WebRequest Error: " + e);
                }
                webReqResponse?.Invoke(result as WebRequestResponseData);
            });
        }

        private static WebRequestResponseData _req(string uri, HttpRequestMethod method, Dictionary<string, string> form) {
            var webReq = (HttpWebRequest)NetWebRequest.Create(uri);
            webReq.Method = method.ToString();
            webReq.ContentType = "application/x-www-form-urlencoded";

            var reqData = Encoding.ASCII.GetBytes(_flattenForm(form));
            webReq.ContentLength = reqData.Length;

            using (var stream = webReq.GetRequestStream()) {
                stream.Write(reqData, 0, reqData.Length);
            }

            var webResponseData = new WebRequestResponseData();
            using (var response = (HttpWebResponse)webReq.GetResponse())
            {
                bool success = ((int)response.StatusCode) >= 200 && ((int)response.StatusCode) < 300;
                webResponseData.isSuccess = success;
                webResponseData.code = (int)response.StatusCode;
                if (success)
                {
                    using (var mem = new MemoryStream())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            while (true)
                            {
                                var data = stream.ReadByte();
                                if (data == -1)
                                    break;
                                mem.WriteByte((byte)data);
                            }
                            webResponseData.data = mem.ToArray();
                        }
                    }
                }
            }
            return webResponseData;
        }

        private static string _flattenForm(Dictionary<string, string> form) {
            var str = new StringBuilder();
            if (form != null) {
                foreach (var keyVal in form)
                    str.Append(keyVal.Key + "=" + Uri.EscapeDataString(keyVal.Value) + "&");
                str.Remove(str.Length - 1, 1);
            }
            return str.ToString();
        }
    }
}
#endif