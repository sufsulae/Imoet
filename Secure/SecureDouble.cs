namespace Imoet.Secure
{
    public class SecureDouble : SecureNumber<decimal>
    {
        public SecureDouble() : this(0.0d){ }
        public SecureDouble(double num) : base() {
            _value_ = (decimal)num;
        }

        protected override decimal _onGetValue(decimal encodedValue, int salt) {
            return encodedValue - salt;
        }
        protected override decimal _onSetValue(decimal valueInput, int salt) {
            return valueInput + salt;
        }

        public static implicit operator SecureDouble(double num) {
            return new SecureDouble(num);
        }
        public static implicit operator double(SecureDouble num)
        {
            return (double)num._value_;
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
            if (obj is double) {
                return (double)_value_ == (double)obj;
            }
            return base.Equals(obj);
        }
    }
}
