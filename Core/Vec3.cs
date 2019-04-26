//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Vec3 : IEquatable<Vec3>
    {
        public float x;
        public float y;
        public float z;

        #region Constructor
        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vec3(float value)
        {
            this.x = this.y = this.z = value;
        }
        #endregion

        #region Property
        public float magnitude
        {
            get { return UMath.Sqrt(x * x + y * y + z * z); }
        }
        public float sqrMagnitude
        {
            get { return x * x + y * y + z * z;}
        }
        public Vec3 normalized
        {
            get {
                Vec3 v = this;
                v.Normalize();
                return v;
            }
        }
        public static Vec3 zero {
            get { return new Vec3(0, 0, 0); }
        }
        public static Vec3 right {
            get { return new Vec3(1, 0, 0); }
        }
        public static Vec3 left {
            get { return new Vec3(-1, 0, 0); }
        }
        public static Vec3 up {
            get { return new Vec3(0, 1, 0); }
        }
        public static Vec3 down {
            get { return new Vec3(0, -1, 0); }
        }
        public static Vec3 forward {
            get { return new Vec3(0, 0, 1); }
        }
        public static Vec3 backward {
            get { return new Vec3(0, 0, -1); }
        }
        #endregion

        #region Basic Operator
        public static unsafe Vec3 operator +(Vec3 l, Vec3 r)
        {
#if IMOET_UNSAFE
            Vec3 res = zero;
            UMath.MassAdd((float*)&l, (float*)&r, (float*)&res, 3);
            return res;
#else
            return new Vec3(l.x + r.x, l.y + r.y, l.z + r.z);
#endif
        }
        public static unsafe Vec3 operator -(Vec3 l, Vec3 r)
        {
#if IMOET_UNSAFE
            Vec3 res = zero;
            UMath.MassSubtract((float*)&l, (float*)&r, (float*)&res, 3);
            return res;
#else
            return new Vec3(l.x - r.x, l.y - r.y, l.z - r.z);
#endif
        }
        public static Vec3 operator -(Vec3 l)
        {
            return new Vec3(-l.x, -l.y, -l.z);
        }
        public static unsafe Vec3 operator *(Vec3 l, float r)
        {
#if IMOET_UNSAFE
            Vec3 res = l;
            UMath.MassMultiply((float*)&res, r, 3);
            return res;
#else
            return new Vec3(l.x * r, l.y * r, l.z * r);
#endif
        }
        public static Vec3 operator *(float l, Vec3 r) {
            return r * l;
        }
        public static Vec3 operator /(Vec3 l, float r) {
            return new Vec3(l.x / r, l.y / r, l.z / r);
        }
        public static bool operator ==(Vec3 l, Vec3 r)
        {
            return (l - r).sqrMagnitude < 9.99999944E-11f;
        }
        public static bool operator !=(Vec3 l, Vec3 r)
        {
            return (l - r).sqrMagnitude >= 9.99999944E-11f;
        }
#endregion

        #region Function
        public void Normalize()
        {
            float num = magnitude;
            if (num > 1E-05f)
            {
                this /= num;
            }
            else
            {
                this = zero;
            }
        }
        public void Set(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static float Dot(Vec3 lhs, Vec3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }
        public static Vec3 Cross(Vec3 lhs, Vec3 rhs)
        {
            return new Vec3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
        }
        public static float AngleRad(Vec3 from, Vec3 to)
        {
            return UMath.Acos(UMath.Clamp(Dot(from.normalized, to.normalized), -1f, 1f));
        }
        public static float Angle(Vec3 from, Vec3 to)
        {
            return AngleRad(from, to) * 57.29578f;
        }
        public static float Distance(Vec3 from, Vec3 to)
        {
            Vec3 vector = from - to;
            return vector.magnitude;
        }
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength)
        {
            if (vector.sqrMagnitude > maxLength * maxLength)
                return vector.normalized * maxLength;
            return vector;
        }
        public static Vec3 SmoothDamp(Vec3 current, Vec3 target, ref Vec3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = UMath.Max(0.0001f, smoothTime);
            float num = 2f / smoothTime;
            float num2 = num * deltaTime;
            float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            Vec3 vector = current - target;
            Vec3 vector2 = target;
            float maxLength = maxSpeed * smoothTime;
            vector = ClampMagnitude(vector, maxLength);
            target = current - vector;
            Vec3 Vec3 = (currentVelocity + num * vector) * deltaTime;
            currentVelocity = (currentVelocity - num * Vec3) * d;
            Vec3 vector4 = target + (vector + Vec3) * d;
            if (Dot(vector2 - current, vector4 - vector2) > 0f)
            {
                vector4 = vector2;
                currentVelocity = (vector4 - vector2) / deltaTime;
            }
            return vector4;
        }
        public float Max(Vec3 value) {
            float m = value.x;
            if (value.y > m) m = value.y;
            if (value.z > m) m = value.z;
            return m;
        }
        public float Min(Vec3 value) {
            float m = value.x;
            if (value.y < m) m = value.y;
            if (value.z < m) m = value.z;
            return m;
        }
        public Vec3 Reflect(Vec3 vector, Vec3 normal) {
            var res = vector;
            var dot = Dot(vector, normal);
            res -= (normal * 2.0f) * dot;
            return res;
        }
        #endregion

        public bool Equals(Vec3 vector)
        {
            return this == vector;
        }
        public override bool Equals(object obj)
        {
            if (obj is Vec3)
            {
                return Equals((Vec3)obj);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.x.GetHashCode() + this.y.GetHashCode() + this.z.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("{{0},{1},{2}}", x, y, z);
        }
    }
}