//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
#if IMOET_INCLUDE_NETWORKING
namespace Imoet.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Net;

    public class SimpleREST2
    {
        //Private Field
        private Uri m_uri;
        private HttpWebRequest m_request;
        private Dictionary<object, string> m_headers;

        //Public Property
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public int TimeOut { get; set; }

        //Constructor
        public SimpleREST2(string url) {
            m_uri = new Uri(url);
            m_headers = new Dictionary<object, string>();
        }

        //Public Method
        public bool AddHeader(Dictionary<string, string> headers) {
            if (headers == null)
                return false;
            foreach (var items in headers) {
                AddHeader(items.Key, items.Value);
            }
            return true;
        }
        public void AddHeader(HttpRequestHeader header, string value) {
            m_headers.Add(header, value);
        }
        public void AddHeader(HttpResponseHeader header, string value) {
            m_headers.Add(header, value);
        }
        public bool AddHeader(string key, string value) {
            if (string.IsNullOrEmpty(key))
                return false;

            //Try To Recognise Key
            object headerVal = null;
            headerVal = Enum.Parse(typeof(HttpRequestHeader), key, true);
            if (headerVal == null) {
                headerVal = Enum.Parse(typeof(HttpResponseHeader), key, true);
                if (headerVal == null)
                    headerVal = key;
            }

            //Adding Header
                m_headers.Add(headerVal, value); 
            return true;
        }

        public bool RemoveHeader(string key) {
            if (string.IsNullOrEmpty(key))
                return false;

            //Try To Recognise Key
            object headerVal = null;
            headerVal = Enum.Parse(typeof(HttpRequestHeader), key, true);
            if (headerVal == null)
            {
                headerVal = Enum.Parse(typeof(HttpResponseHeader), key, true);
                if (headerVal == null)
                    headerVal = key;
            }

            //Remove Header
                m_headers.Remove(headerVal);
            return true;
        }

        public bool RemoveHeader(params string[] headers)
        {
            if (headers == null || headers.Length == 0)
                return false;
            foreach (var items in headers) {
                if (!string.IsNullOrEmpty(items)) {
                    RemoveHeader(items);
                }
            }
            return true;
        }

        public void Request(HttpRequestMethod method,byte[] data, OnRESTResponse2 response) {
            var request = (HttpWebRequest)WebRequest.Create(m_uri);
            request.KeepAlive = true;
            request.Method = method.ToString();
            request.Timeout = TimeOut;
            foreach (var items in m_headers) {
                var headerKey = items.Key;
                var headerValue = items.Value;
                if (headerKey is HttpRequestHeader)
                    request.Headers.Add((HttpRequestHeader)headerKey, headerValue);
                else if (headerKey is HttpResponseHeader)
                    request.Headers.Add((HttpResponseHeader)headerKey, headerValue);
                else
                    request.Headers.Add((string)headerKey, headerValue);
            }
            if (data != null || data.Length != 0) {
                request.Method = "POST";
                string boundary = "-------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundaryBytes = _ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                string formTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                request.Credentials = CredentialCache.DefaultCredentials;
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                var reqStream = request.GetRequestStream();

                reqStream.Close();
            }
        }

        private Encoding _ASCII {
            get { return Encoding.ASCII; }
        }
        private Encoding _UTF8 {
            get { return Encoding.UTF8; }
        }
    }
}
#endif