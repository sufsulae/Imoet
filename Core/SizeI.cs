//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct SizeI
    {
        public int width;
        public int height;

        public SizeI(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public SizeI(int value) {
            this.width = this.height = value;
        }

        #region Property
        public float ratio {
            get { return (float)width / (float)height; }
        }
        #endregion

        #region Basic Operator
        public static SizeI operator +(SizeI a, SizeI b) {
            return new SizeI(a.width + b.width, a.height + b.height);
        }

        public static SizeI operator -(SizeI a, SizeI b)
        {
            return new SizeI(a.width - b.width, a.height - b.height);
        }
        public static SizeI operator *(SizeI a, int b)
        {
            return new SizeI(a.width * b, a.height * b);
        }
        public static SizeI operator *(int a, SizeI b)
        {
            return new SizeI(a * b.width, a * b.height);
        }
        public static SizeI operator *(SizeI a, float b) {
            return a * (int)b;
        }
        public static SizeI operator *(float a, SizeI b) {
            return b * (int)a;
        }
        public static SizeI operator /(SizeI a, int b) {
            return new SizeI(a.width / b, a.height / b);
        }
        public static SizeI operator /(SizeI a, float b) {
            return a / (int)b;
        }
        public static SizeI operator -(SizeI a) {
            return a * -1;
        }
        #endregion

        #region Implicit Operator
        public static implicit operator Vec2(SizeI p) {
            return new Vec2(p.width, p.height);
        }
        public static implicit operator SizeI(Vec2 p)
        {
            return new SizeI((int)p.x, (int)p.y);
        }
        public static implicit operator Point2(SizeI p) {
            return new Point2(p.width, p.height);
        }
        public static implicit operator SizeI(Point2 p) {
            return new SizeI(p.x, p.y);
        }
        #endregion

        #region Equal Operator
        public static bool operator ==(SizeI left, SizeI right) {
            return left.width == right.width && left.height == right.height;
        }
        public static bool operator !=(SizeI left, SizeI right)
        {
            return left.width != right.width || left.height == right.height;
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj is SizeI)
                return this == (SizeI)obj;
            return false;
        }
        public override int GetHashCode()
        {
            return this.width.GetHashCode() + this.height.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("x:{0} y:{1}", this.width, this.height);
        }
    }
}