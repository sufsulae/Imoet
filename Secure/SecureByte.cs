namespace Imoet.Secure
{
    public class SecureByte : SecureNumber<byte>
    {
        public SecureByte() : this(0){ }
        public SecureByte(byte num) : base(){
            _value_ = num;
        }

        protected override byte _onGetValue(byte encodedValue, int salt)
        {
            return (byte)(encodedValue - salt);
        }
        protected override byte _onSetValue(byte valueInput, int salt)
        {
            return (byte)(valueInput + salt);
        }

        public static implicit operator SecureByte(byte num) {
            return new SecureByte(num);
        }
        public static implicit operator byte(SecureByte num)
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
            if (obj is byte) {
                return _value_ == (byte)obj;
            }
            return base.Equals(obj);
        }
    }
}
