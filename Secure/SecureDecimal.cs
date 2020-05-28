namespace Imoet.Secure
{
    public class SecureDecimal : SecureNumber<decimal>
    {
        public SecureDecimal() : this(0.0m){ }
        public SecureDecimal(decimal num) : base(){
            _value_ = num;
        }

        protected override decimal _onGetValue(decimal encodedValue, int salt)
        {
            return encodedValue - salt;
        }
        protected override decimal _onSetValue(decimal valueInput, int salt)
        {
            return valueInput + salt;
        }

        public static implicit operator SecureDecimal(decimal num) {
            return new SecureDecimal(num);
        }
        public static implicit operator decimal(SecureDecimal num)
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
            if (obj is decimal) {
                return _value_ == (decimal)obj;
            }
            return base.Equals(obj);
        }
    }
}
