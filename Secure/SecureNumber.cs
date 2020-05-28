using System;
namespace Imoet.Secure
{
    public abstract class SecureNumber<T>
    {
        private int m_salt;
        public SecureNumber() {
            var type = typeof(T);
            if (!type.Equals(typeof(decimal))) {
                if (!type.IsPrimitive || type.Equals(typeof(string)))
                    throw new ArgumentException("SafeNumber class is not compatible with this kind of data type: " + type);
            }
            m_salt = SecureNumberManager.GetSalt<T>();
        }

        protected T encodedValue;
        protected virtual T _value_ {
            get {
                return _onGetValue(encodedValue,m_salt);
            } 
            set {
                encodedValue = _onSetValue(value, m_salt);
            }
        }
        protected virtual T _onGetValue(T encodedValue, int salt) {
            return default(T);
        }
        protected virtual T _onSetValue(T inputValue, int salt) {
            return default(T);
        }

        ~SecureNumber() {
            SecureNumberManager.RemoveSalt<T>(m_salt);
        }
    }
}
