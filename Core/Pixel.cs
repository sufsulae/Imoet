using System;
using System.Runtime.InteropServices;

namespace Imoet
{
    [Serializable,StructLayout(LayoutKind.Sequential)]
    public struct Pixel4 {
        public byte v1, v2, v3, v4;
        public Pixel4(byte v1, byte v2, byte v3, byte v4) {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
        }
        public static implicit operator Pixel3(Pixel4 val) {
            return new Pixel3(val.v1, val.v2, val.v3);
        }
        public static implicit operator Pixel4(Pixel3 val) {
            return new Pixel4(val.v1, val.v2, val.v3, 255);
        }
        public static bool operator ==(Pixel4 l, Pixel4 r) {
            return l.v1 == r.v1 && l.v2 == r.v2 && l.v3 == r.v3 && l.v4 == r.v4;
        }
        public static bool operator !=(Pixel4 l, Pixel4 r) {
            return l.v1 != r.v1 || l.v2 != r.v2 || l.v3 != r.v3 || l.v4 != r.v4;
        }
        public override int GetHashCode() {
            return v1.GetHashCode() + v2.GetHashCode() + v3.GetHashCode() + v4.GetHashCode();
        }
        public override bool Equals(object obj) {
            if (obj.GetType() == typeof(Pixel4))
                return this == (Pixel4)obj;
            return false;
        }
        public override string ToString() {
            return string.Format("v1: {0}, v2: {1}, v3: {2}, v4: {3}", v1, v2, v3, v4);
        }
    }

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Pixel3 {
        public byte v1, v2, v3;
        public Pixel3(byte v1, byte v2, byte v3)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }
        public static bool operator ==(Pixel3 l, Pixel3 r)
        {
            return l.v1 == r.v1 && l.v2 == r.v2 && l.v3 == r.v3;
        }
        public static bool operator !=(Pixel3 l, Pixel3 r)
        {
            return l.v1 != r.v1 || l.v2 != r.v2 || l.v3 != r.v3;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Pixel3))
                return this == (Pixel3)obj;
            return false;
        }
        public override string ToString()
        {
            return string.Format("v1: {0}, v2: {1}, v3: {2}", v1, v2, v3);
        }
        public override int GetHashCode()
        {
            return v1.GetHashCode() + v2.GetHashCode() + v3.GetHashCode();
        }
    }
}
