//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
#if IMOET_INCLUDE_NETWORKING
namespace Imoet.Networking
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Net;
    using System.IO;
    using Imoet.Utility;
    public class SimpleRESTForm
    {
        private string m_id;
        private Dictionary<string, string> m_val;
        private Dictionary<string, byte[]> m_dat;
        private List<RESTFile> m_file;
        private StringBuilder _builder;
        private static byte[] m_buffer = new byte[8192];

        public string id {
            get { return m_id; }
        }
        public byte[] data
        {
            get {
                if (_hasFile) {
                    StringBuilder builder = new StringBuilder();
                    MemoryStream stream = new MemoryStream();
                    byte[] data = null;
                    builder.Append("\r\n");
                    //Adding Form
                    foreach (var k in m_val) {
                        builder.Append("---------------------------" + m_id + "\r\n");
                        builder.Append("Content-Disposition: form-data; name=\"" + k.Key + "\"\r\n");
                        builder.Append(k.Value);
                    }
                    data = _ASCII.GetBytes(builder.ToString());
                    stream.Write(data, 0, data.Length);

                    //Adding File
                    foreach (var k in m_file) {
                        builder = new StringBuilder();
                        builder.Append("---------------------------" + m_id+ "\r\n");
                        builder.Append("Content-Disposition: form-data; name=\"" + k.key + "\";" + " filename=\""+k.file+"\"\r\n");
                        builder.Append("Content-Type: " + k.mime + "\r\n\r\n");
                        data = _ASCII.GetBytes(builder.ToString());
                        stream.Write(data, 0, data.Length);
                        FileStream fStream = new FileStream(k.file,FileMode.Open);
                        int bytesRead = 0;
                        while ((bytesRead = fStream.Read(m_buffer, 0, 8192)) != 0){
                            stream.Write(m_buffer, 0, bytesRead);
                        }
                        fStream.Close();
                    }
                    //Adding Raw Data
                    foreach (var k in m_dat) {
                        builder = new StringBuilder();
                        builder.Append("---------------------------" + m_id+"\r\n");
                        builder.Append("Content-Disposition: form-data; name=\"" + k.Key + "\"\r\n");
                        builder.Append("Content-Type: application/octet-stream\r\n\r\n");
                        data = _ASCII.GetBytes(builder.ToString());
                        stream.Write(data, 0, data.Length);
                        stream.Write(k.Value, 0, k.Value.Length);
                    }
                    //Closing
                    builder = new StringBuilder();
                    builder.Append("---------------------------" + m_id + "--");
                    data = _ASCII.GetBytes(builder.ToString());
                    stream.Write(data, 0, data.Length);

                    //Instance data
                    data = stream.ToArray();
                    stream.Close();
                    return data;
                }
                return null;
            }
        }
        public string[] header
        {
            get {
                if (_hasFile)
                {
                    return new string[] { "Content-Type", "multipart/form-data; boundary= " + "---------------------------" + m_id };
                }
                else {
                    return new string[] { "Content-Type", "application/x-www-form-urlencoded" };
                }
            }
        }

        public SimpleRESTForm():this(null) { }
        public SimpleRESTForm(string idName) {
            m_id = idName + _identifier;
            m_val = new Dictionary<string, string>();
            m_dat = new Dictionary<string, byte[]>();
            m_file = new List<RESTFile>();
            _builder = new StringBuilder();
        }
        public void AddField(string key, string value)
        {
            if (!m_val.ContainsKey(key))
                m_val.Add(key, value);
        }
        public void AddFile(string key, string file)
        {
            foreach (var rfile in m_file) {
                if (rfile.key == key)
                    return;
            }
            RESTFile restFile = new RESTFile();
            restFile.key = key;
            restFile.file = file;
            FileInfo fInfo = new FileInfo(file);
            restFile.mime = MimeTypeMap.GetMimeType(fInfo.Extension);
            m_file.Add(restFile);
        }
        public void AddBinaryData(string key, byte[] data)
        {
            if (!m_dat.ContainsKey(key))
            {
                m_dat.Add(key, data);
            }
        }

        private string _identifier {
            get {
                return DateTime.Now.Ticks.ToString("x");
            }
        }
        private Encoding _ASCII {
            get { return Encoding.ASCII; }
        }
        private bool _hasFile{
            get { return m_dat.Count != 0 || m_file.Count != 0; }
        }
        internal void _writeStream(Stream stream) {
            StringBuilder builder = new StringBuilder();
            byte[] data = null;
            //builder.Append("\r\n");
            ////Adding Form
            //foreach (var k in m_val)
            //{
            //    builder.Append("---------------------------" + m_id + "\r\n");
            //    builder.Append("Content-Disposition: form-data; name=\"" + k.Key + "\"" + "\r\n\r\n");
            //    builder.Append(k.Value + "\r\n");
            //}
            foreach (var k in m_val) {
                builder.Append(k.Key + "=" + k.Value + "&");
            }
            string s = builder.ToString();
            if (!string.IsNullOrEmpty(s)) {
                data = _ASCII.GetBytes(s.Remove(s.Length - 1, 1));
                stream.Write(data, 0, data.Length);
            }

            //Adding File
            foreach (var k in m_file)
            {
                builder = new StringBuilder();
                builder.Append("---------------------------" + m_id + "\r\n");
                builder.Append("Content-Disposition:form-data; name=\"" + k.key + "\";" + " filename=\"" + k.file + "\"" + "\r\n");
                builder.Append("Content-Type:" + k.mime + "\r\n\r\n");
                data = _ASCII.GetBytes(builder.ToString());
                stream.Write(data, 0, data.Length);
                //Write File Data
                FileStream fStream = new FileStream(k.file, FileMode.Open, FileAccess.ReadWrite);
                int bytesRead = 0;
                while ((bytesRead = fStream.Read(m_buffer, 0, 8192)) != 0)
                {
                    stream.Write(m_buffer, 0, bytesRead);
                }
                fStream.Close();
            }
            //Adding Raw Data
            foreach (var k in m_dat)
            {
                builder = new StringBuilder();
                builder.Append("---------------------------" + m_id + "\r\n");
                builder.Append("Content-Disposition: form-data; name=\"" + k.Key + "\"" + "\r\n");
                builder.Append("Content-Type: application/octet-stream" + "\r\n\r\n");
                data = _ASCII.GetBytes(builder.ToString());
                stream.Write(data, 0, data.Length);
                stream.Write(k.Value, 0, k.Value.Length);
            }
            //Closing
            builder = new StringBuilder();
            //builder.Append("\r\n---------------------------" + m_id + "--\r\n");
            data = _ASCII.GetBytes(builder.ToString());
            stream.Write(data, 0, data.Length);
        }

        public static SimpleRESTForm FromObject<T>(T obj) where T : new() {
            var result = new SimpleRESTForm();
            var objType = obj.GetType();
            var objFields = objType.GetFields();
            foreach (var field in objFields) {
                var fieldValue = field.GetValue(obj);
                result.AddField(field.Name, fieldValue == null?"":fieldValue.ToString());
            }
            return result;
        }
    }

    internal class RESTFile {
        public string key;
        public string file;
        public string mime;
    }
}
#endif