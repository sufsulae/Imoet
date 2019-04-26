using System;
using System.Runtime.InteropServices;

namespace Imoet
{
    [Serializable,StructLayout(LayoutKind.Sequential)]
    public struct Pixel4F {
        //Field
        private float m_v1, m_v2, m_v3, m_v4;

        //Property
        public float v1 {
            get { return m_v1; }
            set { m_v1 = UMath.Clamp01(value); }
        }
        public float v2
        {
            get { return m_v2; }
            set { m_v2 = UMath.Clamp01(value); }
        }
        public float v3
        {
            get { return m_v3; }
            set { m_v3 = UMath.Clamp01(value); }
        }
        public float v4
        {
            get { return m_v4; }
            set { m_v4 = UMath.Clamp01(value); }
        }

        //Structure
        public Pixel4F(float v1, float v2, float v3, float v4) {
            this.m_v1 = UMath.Clamp01(v1);
            this.m_v2 = UMath.Clamp01(v2);
            this.m_v3 = UMath.Clamp01(v3);
            this.m_v4 = UMath.Clamp01(v4);
        }

        //Implicit Operator
        public static implicit operator Pixel3F(Pixel4F val) {
            return new Pixel3F(val.v1, val.v2, val.v3);
        }
        public static implicit operator Pixel4F(Pixel3F val) {
            return new Pixel4F(val.v1, val.v2, val.v3, 255);
        }

        //Equal Operator
        public static bool operator ==(Pixel4F l, Pixel4F r) {
            return l.v1 == r.v1 && l.v2 == r.v2 && l.v3 == r.v3 && l.v4 == r.v4;
        }
        public static bool operator !=(Pixel4F l, Pixel4F r) {
            return l.v1 != r.v1 || l.v2 != r.v2 || l.v3 != r.v3 || l.v4 != r.v4;
        }
        public override int GetHashCode() {
            return v1.GetHashCode() + v2.GetHashCode() + v3.GetHashCode() + v4.GetHashCode();
        }
        public override bool Equals(object obj) {
            if (obj.GetType() == typeof(Pixel4F))
                return this == (Pixel4F)obj;
            return false;
        }
        public override string ToString() {
            return string.Format("v1: {0}, v2: {1}, v3: {2}, v4: {3}", v1, v2, v3, v4);
        }
    }

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Pixel3F {
        //Field
        private float m_v1, m_v2, m_v3;

        //Property
        public float v1
        {
            get { return m_v1; }
            set { m_v1 = UMath.Clamp01(value); }
        }
        public float v2
        {
            get { return m_v2; }
            set { m_v2 = UMath.Clamp01(value); }
        }
        public float v3
        {
            get { return m_v3; }
            set { m_v3 = UMath.Clamp01(value); }
        }

        //Structure
        public Pixel3F(float v1, float v2, float v3)
        {
            this.m_v1 = UMath.Clamp01(v1);
            this.m_v2 = UMath.Clamp01(v2);
            this.m_v3 = UMath.Clamp01(v3);
        }

        //Equal Operator
        public static bool operator ==(Pixel3F l, Pixel3F r)
        {
            return l.v1 == r.v1 && l.v2 == r.v2 && l.v3 == r.v3;
        }
        public static bool operator !=(Pixel3F l, Pixel3F r)
        {
            return l.v1 != r.v1 || l.v2 != r.v2 || l.v3 != r.v3;
        }

        //Overrided Methods
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Pixel3F))
                return this == (Pixel3F)obj;
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
