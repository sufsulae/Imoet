//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Vec2 : IEquatable<Vec2>
    {
        public float y;
        public float x;

        #region Contructor
        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Properties
        public static Vec2 one {
            get { return new Vec2(1f, 1f); }
        }

        public static Vec2 right {
            get { return new Vec2(1f, 0f); }
        }

        public static Vec2 up {
            get { return new Vec2(0f, 1f); }
        }

        public static Vec2 zero {
            get { return new Vec2(0f, 0f); }
        }

        public float this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return this.x;
                }
                if (index != 1)
                {
                    throw new IndexOutOfRangeException("Invalid Vec2 index!");
                }
                return this.y;
            }
            set
            {
                if (index != 0)
                {
                    if (index != 1)
                    {
                        throw new IndexOutOfRangeException("Invalid Vec2 index!");
                    }
                    this.y = value;
                }
                else
                {
                    this.x = value;
                }
            }
        }
        public float magnitude
        {
            get { return UMath.Sqrt(this.x * this.x + this.y * this.y); }
        }

        public Vec2 normalized
        {
            get
            {
                Vec2 result = new Vec2(this.x, this.y);
                result.Normalize();
                return result;
            }
        }

        public float sqrMagnitude
        {
            get { return this.x * this.x + this.y * this.y; }
        }
        #endregion

        #region Static Methods
        public static float Angle(Vec2 from, Vec2 to)
        {
            return AngleRad(from,to) * 57.29578f;
        }
        public static float AngleRad(Vec2 from, Vec3 to)
        {
            return UMath.Acos(UMath.Clamp(Vec2.Dot(from.normalized, to.normalized), -1f, 1f));
        }
        public static float Distance(Vec2 a, Vec2 b)
        {
            return (a - b).magnitude;
        }
        public static float Dot(Vec2 lhs, Vec2 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }
        public static Vec2 LerpClamped(Vec2 from, Vec2 to, float t)
        {
            t = UMath.Clamp01(t);
            return new Vec2(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t);
        }
        public static Vec2 Lerp(Vec2 from, Vec2 to, float t)
        {
            return new Vec2(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t);
        }
        public static Vec2 ClampMagnitude(Vec2 vector, float maxLength)
        {
            if (vector.sqrMagnitude > maxLength * maxLength)
            {
                return vector.normalized * maxLength;
            }
            return vector;
        }
        public static float SqrMagnitude(Vec2 a)
        {
            return a.x * a.x + a.y * a.y;
        }
        public static Vec2 SmoothDamp(Vec2 current, Vec2 target, ref Vec2 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            smoothTime = UMath.Max(0.0001f, smoothTime);
            float num = 2f / smoothTime;
            float num2 = num * deltaTime;
            float d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            Vec2 vector = current - target;
            Vec2 Vec2 = target;
            float maxLength = maxSpeed * smoothTime;
            vector = ClampMagnitude(vector, maxLength);
            target = current - vector;
            Vec2 vector3 = (currentVelocity + num * vector) * deltaTime;
            currentVelocity = (currentVelocity - num * vector3) * d;
            Vec2 vector4 = target + (vector + vector3) * d;
            if (Dot(Vec2 - current, vector4 - Vec2) > 0f)
            {
                vector4 = Vec2;
                currentVelocity = (vector4 - Vec2) / deltaTime;
            }
            return vector4;
        }
        #endregion

        #region Methods
        public void Normalize()
        {
            float magnitude = this.magnitude;
            if (magnitude > 1E-05f)
            {
                this /= magnitude;
            }
            else
            {
                this = zero;
            }
        }

        public void Scale(Vec2 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
        }

        public void Set(float new_x, float new_y)
        {
            this.x = new_x;
            this.y = new_y;
        }

        public float SqrMagnitude()
        {
            return this.x * this.x + this.y * this.y;
        }

        public string ToString(string format)
        {
            return string.Format("({0}, {1})", new object[]
            {
                this.x.ToString (format),
                this.y.ToString (format)
            });
        }
        #endregion

        #region Basic Operator
        public static unsafe Vec2 operator +(Vec2 a, Vec2 b)
        {
#if IMOET_UNSAFE
            Vec2 res = zero;
            UMath.MassAdd((float*)&a, (float*)&b, (float*)&res, 2);
            return res;
#else
            return new Vec2(a.x + b.x, a.y + b.y);
#endif
        }

        public static unsafe Vec2 operator /(Vec2 a, float d)
        {
#if IMOET_UNSAFE
            Vec2 res = a;
            UMath.MassDivided((float*)&a, d, 2);
            return res;
#else
            return new Vec2(a.x / d, a.y / d);
#endif
        }
        public static unsafe Vec2 operator *(float d, Vec2 a)
        {
#if IMOET_UNSAFE
            Vec2 res = a;
            UMath.MassMultiply((float*)&res, d, 2);
            return res;
#else
            return new Vec2(a.x * d, a.y * d);
#endif
        }

        public static unsafe Vec2 operator *(Vec2 a, float d)
        {
#if IMOET_UNSAFE
            Vec2 res = a;
            UMath.MassMultiply((float*)&res, d, 2);
            return res;
#else
            return new Vec2(a.x * d, a.y * d);
#endif
        }

        public static unsafe Vec2 operator -(Vec2 a, Vec2 b)
        {
#if IMOET_UNSAFE
            Vec2 res = zero;
            UMath.MassSubtract((float*)&a, (float*)&b, (float*)&res, 2);
            return res;
#else
            return new Vec2(a.x - b.x, a.y - b.y);
#endif
        }

        public static Vec2 operator -(Vec2 a)
        {
            return new Vec2(-a.x, -a.y);
        }
        #endregion

        #region Equal Operator
        public static bool operator ==(Vec2 lhs, Vec2 rhs)
        {
            return SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
        }

        public static bool operator !=(Vec2 lhs, Vec2 rhs)
        {
            return SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
        }
        #endregion

        #region Implicit Operator
        public static implicit operator Vec3(Vec2 v)
        {
            return new Vec3(v.x, v.y, 0f);
        }

        public static implicit operator Vec2(Vec3 v)
        {
            return new Vec2(v.x, v.y);
        } 
        #endregion


        public bool Equals(Vec2 vec) {
            return this == vec;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vec2))
            {
                return false;
            }
            Vec2 vector = (Vec2)other;
            return this.x.Equals(vector.x) && this.y.Equals(vector.y);
        }
        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }
        public override string ToString()
        {
            return string.Format("({0:F1}, {1:F1})", new object[]
            {
                this.x,
                this.y
            });
        }
    }
}