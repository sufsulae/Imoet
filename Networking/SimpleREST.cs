//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
#if IMOET_INCLUDE_NETWORKING
namespace Imoet.Networking
{
    using System;
    using System.Net;
    using System.IO;
    using Imoet.Threading;
    public class SimpleREST {
        //Private Field
        private HttpWebRequest m_request;
        private string m_url;
        private int m_timeOut = 30000;
        private StreamReader m_stream;
        private WebResponse m_response;
        private WebException m_error;

        //Private static const field
        private const int defaultTimeOut = 30000;

        //Property
        public string url {
            get { return m_url; }
        }
        public int timeOut {
            get { return m_timeOut; }
            set { m_timeOut = value; }
        }
        public StreamReader stream {
            get { return m_stream; }
        } 
        public Exception error {
            get { return m_error; }
        }

        //Constructor
        public SimpleREST(string url)
        {
            m_url = url;
            var w = new WebClient();
        }
        public void Request(OnRESTResponse onRequest) {
            Request(HttpRequestMethod.GET, null, onRequest);
        }
        public void Request(byte[] data,OnRESTResponse onRequest) {
            Request(HttpRequestMethod.POST, data, onRequest);
        }
        public void Request(byte[] data, OnRESTResponse onRequest, params string[] headers) {
            Request(HttpRequestMethod.POST, data, onRequest, headers);
        }
        public void Request(HttpRequestMethod method, byte[] data, OnRESTResponse onRequest) {
            Request(method,data,onRequest,"Content-Type", "application/x-www-form-urlencoded");
        }
        public void Request(HttpRequestMethod method,byte[] data, OnRESTResponse onRequest, params string[] headers) {
            if (m_response != null)
                m_response.Close();
            if (m_stream != null)
                m_stream.Close();

            m_response = null;
            m_stream = null;
            m_error = null;

            m_request = (HttpWebRequest)WebRequest.Create(url);
            m_request.KeepAlive = true;
            m_request.Method = method.ToString();
            m_request.Timeout = defaultTimeOut;
            if (headers != null && headers.Length % 2 == 0)
            {
                int hLen = headers.Length;
                for (int i = 0; i < hLen; i+=2) {
                    string key = headers[i];
                    string value = headers[i + 1];
                    switch (key) {
                        case "Content-Type":
                            m_request.ContentType = value;
                           break;
                        case "Content-Length":
                            m_request.ContentLength = long.Parse(value);
                            break;
                        default:
                            m_request.Headers.Add(key, value);
                            break;
                    }
                }
            }

            if (data != null) {
                m_request.ContentLength = data.Length;
                using (var stream = m_request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            try
            {
                m_response = m_request.GetResponse();
                var strm = m_response.GetResponseStream();
                m_stream = new StreamReader(strm);
            }
            catch (WebException e) {
                m_error = e;
                var strm = e.Response.GetResponseStream();
                m_stream = new StreamReader(strm);
            }

            onRequest?.Invoke(m_error == null, m_error == null ? null : m_error.ToString(), m_stream);
        }

        public void Request(SimpleRESTForm form, OnRESTResponse onRequest) {
            m_request = (HttpWebRequest)WebRequest.Create(url);
            m_request.ContentType = form.header[1];
            m_request.Method = "POST";
            m_request.Timeout = defaultTimeOut;
            m_request.KeepAlive = true;
            m_request.Credentials = CredentialCache.DefaultCredentials;

            Stream reqStream = m_request.GetRequestStream();
            //FileStream reqStream = new FileStream("Dump.txt", FileMode.OpenOrCreate);
            form._writeStream(reqStream);
            reqStream.Close();
            try
            {
                m_response = m_request.GetResponse();
                var strm = m_response.GetResponseStream();
                m_stream = new StreamReader(strm);
            }
            catch (WebException e)
            {
                m_error = e;
                var stream = e.Response.GetResponseStream();
                m_stream = new StreamReader(stream);
            }
            finally
            {
                m_request = null;
            }
            onRequest?.Invoke(m_error == null, m_error == null ? null : m_error.ToString(), m_stream);
        }

        public void RequestAsync(OnRESTResponse onRequest) {
            RequestAsync(HttpRequestMethod.GET, null, onRequest);
        }

        public void RequestAsync(byte[] data,OnRESTResponse onRequest) {
            RequestAsync(HttpRequestMethod.POST, data, onRequest);
        }

        public void RequestAsync(byte[] data, OnRESTResponse onRequest, params string[] headers) {
            RequestAsync(HttpRequestMethod.POST, data, onRequest,headers);
        }

        public void RequestAsync(HttpRequestMethod method, byte[] data, OnRESTResponse onRequest) {
            RequestAsync(method, data, onRequest, "Content-Type", "application/x-www-form-urlencoded");
        }

        public void RequestAsync(HttpRequestMethod method,byte[] data, OnRESTResponse onRequest, params string[] headers)
        {
            Worker.WorkerDelegate onWork = delegate (out object result)
            {
                Request(method, data, null, headers);
                result = null;
                return true;
            };
            Worker.WorkerFinishedDelegate onFinish = delegate (Exception e, object result)
            {
                //if (e != null)
                //    throw e;
                //onRequest?.Invoke(m_error == null, m_error == null ? null : m_error.ToString(), m_stream);
                onRequest?.Invoke(m_error == null, m_error == null ? null : m_error.ToString(), m_stream);
            };
            Worker.StartWorker(onWork, onFinish);
        }

        public void RequestAsync(SimpleRESTForm form, OnRESTResponse onRequest)
        {
            Worker.WorkerDelegate onWork = delegate (out object result)
            {
                Request(form, onRequest);
                result = null;
                return true;
            };
            Worker.WorkerFinishedDelegate onFinish = delegate (Exception e, object result)
            {
                //if (e != null)
                //    throw e;
                //onRequest?.Invoke(m_error == null, m_error == null ? null : m_error.ToString(), m_stream);
                onRequest?.Invoke(m_error == null, m_error == null ? null : m_error.ToString(), m_stream);
            };
            Worker.StartWorker(onWork, onFinish);
        }
    }
}
#endif