namespace Imoet.Secure
{
    public class SecureLong : SecureNumber<long>
    {
        public SecureLong() : this(0){ }
        public SecureLong(long num) : base() {
            _value_ = num;
        }

        protected override long _onGetValue(long encodedValue, int salt) {
            return encodedValue - salt;
        }
        protected override long _onSetValue(long inputValue, int salt) {
            return inputValue + salt;
        }

        public static implicit operator SecureLong(long num) {
            return new SecureLong(num);
        }
        public static implicit operator long(SecureLong num)
        {
            return num._value_;
        }

        public override int GetHashCode()
        {
            return _value_.GetHashCode();
        }
        public override string ToString()
        {
            return _value_.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj is long) {
                return _value_ == (long)obj;
            }
            return base.Equals(obj);
        }
    }
}
