﻿using System;
namespace Imoet.Secure
{
    public class SecureFloat : SecureNumber<decimal>
    {
        public SecureFloat() : this(0.0f){ }
        public SecureFloat(float num) : base() {
            Console.WriteLine("Number Setted: " + num);
            _value_ = (decimal)num;
        }

        protected override decimal _onGetValue(decimal encodedValue, int salt) {
            return encodedValue - salt;
        }
        protected override decimal _onSetValue(decimal valueInput, int salt) {
            return valueInput + salt;
        }

        public static implicit operator SecureFloat(float num) {
            Console.WriteLine("Number input: " + num);
            return new SecureFloat(num);
        }
        public static implicit operator float(SecureFloat num)
        {
            return (float)num._value_;
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
            if (obj is float) {
                return (float)_value_ == (float)obj;
            }
            return base.Equals(obj);
        }
    }
}
