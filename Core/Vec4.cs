//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Vec4 : IEquatable<Vec4>
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vec4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Vec4(float value)
        {
            this.x = this.y = this.z = this.w = value;
        }

        #region Basic Operator
        public static unsafe Vec4 operator +(Vec4 left, Vec4 right)
        {
#if IMOET_UNSAFE
            var res = default(Vec4);
            UMath.MassAdd((float*)&left, (float*)&right, (float*)&res, 4);
            return res;
#else
            return new Vec4(left.x + right.x, left.y + right.y, left.z + right.z, left.w + right.z);
#endif
        }
        public static unsafe Vec4 operator -(Vec4 left, Vec4 right)
        {
#if IMOET_UNSAFE
            var res = default(Vec4);
            UMath.MassAdd((float*)&left, (float*)&right, (float*)&res, 4);
            return res;
#else
            return new Vec4(left.x - right.x, left.y - right.y, left.z - right.z, left.w - right.z);
#endif
        }
        public static unsafe Vec4 operator *(Vec4 left, float right)
        {
#if IMOET_UNSAFE
            UMath.MassMultiply((float*)&left, right, 4);
            return left;
#else
            return new Vec4(left.x * right, left.y * right, left.z * right, left.w * right);
#endif
        }
        public static unsafe Vec4 operator /(Vec4 left, float right)
        {
#if IMOET_UNSAFE
            UMath.MassDivided((float*)&left, right, 4);
            return left;
#else
            return new Vec4(left.x / right, left.y / right, left.z / right, left.w / right);
#endif
        }
        #endregion

        #region Equal Operator
        public static bool operator ==(Vec4 left, Vec4 right)
        {
            return left.x == right.x && left.y == right.y && left.z == right.z && left.w == right.w;
        }
        public static bool operator !=(Vec4 left, Vec4 right)
        {
            return left.x != right.x || left.y != right.y || left.z != right.z || left.w != right.w;
        } 
        #endregion

        public bool Equals(Vec4 value) {
            return this == value;
        }
        public override bool Equals(object obj)
        {
            if (obj is Vec4)
                return (Vec4)obj == this;
            return false;
        }
        public override int GetHashCode() {
            return this.x.GetHashCode() + this.y.GetHashCode() + this.z.GetHashCode() + this.w.GetHashCode();
        }
    }
}