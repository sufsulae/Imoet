//Imoet Library
//Copyright © 2020 Yusuf Sulaeman
namespace Imoet.Collections {

    using System.Collections;
    using System.Collections.Generic;

    [System.Serializable]
    public class SafeDictionary<TKey, TValue> : IEnumerable
    {
        private List<TKey> m_key;
        private List<TValue> m_value;

        public SafeDictionary()
        {
            m_key = new List<TKey>();
            m_value = new List<TValue>();
        }

        public int Count { get { return m_key.Count; } }
        public IEnumerable<TKey> Keys { get { return m_key; } }
        public IEnumerable<TValue> Values { get { return m_value; } }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public SafeDictionaryEnum<TKey, TValue> GetEnumerator()
        {
            return new SafeDictionaryEnum<TKey, TValue>(m_key, m_value);
        }

        public TValue this[TKey key]
        {
            get
            {
                for (int i = 0; i < Count; i++)
                {
                    if (m_key[i].Equals(key))
                        return m_value[i];
                }
                return default;
            }
            set
            {
                Add(key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (!ContainKey(key))
            {
                m_key.Add(key);
                m_value.Add(value);
            }
        }

        public bool Remove(TKey key)
        {
            var ret = false;
            var count = m_key.Count;
            for (int i = 0; i < count; i++)
            {
                if (m_key[i].Equals(key))
                {
                    m_key.RemoveAt(i);
                    m_value.RemoveAt(i);
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        public TValue Find(TKey key)
        {
            return this[key];
        }

        public TKey Find(TValue value)
        {
            return m_key[m_value.IndexOf(value)];
        }

        public bool ContainKey(TKey key)
        {
            return m_key.Contains(key);
        }

        public bool ContainValue(TValue value)
        {
            return m_value.Contains(value);
        }

        public TValue Index(int idx)
        {
            return m_value[idx];
        }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            var newDict = new Dictionary<TKey, TValue>();
            for (int i = 0; i < Count; i++)
                newDict.Add(m_key[i], m_value[i]);
            return newDict;
        }
    }

    public class SafeDictionaryEnum<TKey, TValue> : IEnumerator
    {
        private int m_pos = -1;
        private List<TKey> m_keys;
        private List<TValue> m_values;

        public SafeDictionaryEnum(List<TKey> keys, List<TValue> values)
        {
            m_keys = keys;
            m_values = values;
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            m_pos++;
            return (m_pos < m_keys.Count);
        }

        public void Reset()
        {
            m_pos = -1;
        }
        public KeyValuePair<TKey, TValue> Current
        {
            get { return new KeyValuePair<TKey, TValue>(m_keys[m_pos], m_values[m_pos]); }
        }
    }
}