//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
#if IMOET_INCLUDE_NETWORKING
namespace Imoet.Networking
{
    using System.Collections.Generic;
    public class SimpleREST2Form
    {
        Dictionary<string, string> m_key;
        List<RESTFileInput> m_files;

        public SimpleREST2Form() {
            m_key = new Dictionary<string, string>();
        }

        public void AddField(string key, string value) {
            m_key.Add(key, value);
        }

        public void AddField(Dictionary<string, string> fields) {
            foreach (var items in fields) {
                m_key.Add(items.Key, items.Value);
            }
        }

        public void RemoveField(string key) {
            m_key.Remove(key);
        }

        public void RemoveField(string[] keys) {
            foreach (var item in keys) {
                m_key.Remove(item);
            }
        }
    }
}
#endif