//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
    using System;
    public static unsafe class UMath
    {
        private static Random random;
        private static float[] singleValue;
        private static float[] singleResult;
#if IMOET_UNSAFE
        private static float* sValPtr, sResPtr;
#endif
        public const float Epsilon = 1E-05f;
        public const float MaxAngle = 360.0f;
        public const float HalfAngle = 180.0f;
        public const float HalfPI = 1.57079637f;
        public const float DoublePI = 6.28318548f;
        public const float PI = 3.14159274f;
        static UMath()
        {
            singleResult = new float[1];
            singleValue = new float[1];
        }
#if IMOET_UNSAFE
        private static void _setPtr() {
            fixed (float* fl = &singleValue[0])
            {
                sValPtr = fl;
            }
            fixed (float* fl = &singleResult[0])
            {
                sResPtr = fl;
            }
        }
        public static void MassAdd(float* x, float* y, float* result, int dataCount)
        {
            for (int i = 0; i < dataCount; i++, x++, y++, result++)
            {
                *result = *x + *y;
            }
        }

        public static void MassSubtract(float* x, float* y, float* result, int dataCount)
        {
            for (int i = 0; i < dataCount; i++, x++, y++, result++)
            {
                *result = *x - *y;
            }
        }

        public static void MassMultiply(float* x, float* y, float* result, int dataCount)
        {
            for (int i = 0; i < dataCount; i++, x++, y++, result++)
            {
                *result = *x * *y;
            }
        }
        public static void MassMultiply(float* x, float y, int dataCount)
        {
            for (int i = 0; i < dataCount; i++, x++)
            {
                *x = *x * y;
            }
        }

        public static void MassDivided(float* x, float* y, float* result, int dataCount)
        {
            for (int i = 0; i < dataCount; i++, x++, y++, result++)
            {
                *result = *x / *y;
            }
        }
        public static void MassDivided(float* x, float y, int dataCount) {
            for (int i = 0; i < dataCount; i++, x++)
            {
                *x = *x / y;
            }
        }
        public static void MassDivided(float x, float* y, int dataCount)
        {
            for (int i = 0; i < dataCount; i++, x++)
            {
                *y = x / *y;
            }
        }
        public static void MassSqrt(float* data, int dataCount, float* result) {
            for (int i = 0; i < dataCount; i++, data++, result++)
            {
                *result = Sqrt(*data);
            }
        }

        public static float Sum(float* val, int dataCount) {
            float result = 0.0f;
            while (dataCount > 0)
            {
                result += *val;
                val++;
                dataCount--;
            }
            return result;
        }

        public static float SumAbs(float* val, int dataCount)
        {
            float result = 0.0f;
            while (dataCount > 0)
            {
                result += *val;
                val++;
                dataCount--;
            }
            return Abs(result);
        }

        public static float SumSquares(float* val, int dataCount)
        {
            float result = 0.0f;
            while (dataCount > 0)
            {
                result += *val * *val;
                val++;
                dataCount--;
            }
            return result;
        }
#endif
#if IMOET_INCLUDE_MATH
        /*MATRIX*/
        public static Matrix3x3 AddMatrix(Matrix3x3 a, Matrix3x3 b)
        {
            var m = default(Matrix3x3);
            //for (int i = 0; i < a.length; i++)
            //    m[i] = a[i] + b[i];
            m.v00 = a.v00 + b.v00;
            m.v01 = a.v01 + b.v01;
            m.v02 = a.v02 + b.v02;
            m.v10 = a.v10 + b.v10;
            m.v11 = a.v11 + b.v11;
            m.v12 = a.v12 + b.v12;
            m.v20 = a.v20 + b.v20;
            m.v21 = a.v21 + b.v21;
            m.v22 = a.v22 + b.v22;
            return m;
        }

        public static Matrix3x3 SubtractMatrix(Matrix3x3 a, Matrix3x3 b)
        {
            var m = default(Matrix3x3);
            // for (int i = 0; i < a.length; i++)
            // m[i] = a[i] - b[i];
            m.v00 = a.v00 - b.v00;
            m.v01 = a.v01 - b.v01;
            m.v02 = a.v02 - b.v02;
            m.v10 = a.v10 - b.v10;
            m.v11 = a.v11 - b.v11;
            m.v12 = a.v12 - b.v12;
            m.v20 = a.v20 - b.v20;
            m.v21 = a.v21 - b.v21;
            m.v22 = a.v22 - b.v22;
            return m;
        }

        public static Matrix3x3 MultiplyMatrix(Matrix3x3 a, Matrix3x3 b)
        {
            var m = new Matrix3x3();
            m.v00 = a.v00 * b.v00 + a.v01 * b.v10 + a.v02 * b.v20;
            m.v01 = a.v00 * b.v01 + a.v01 * b.v11 + a.v02 * b.v21;
            m.v02 = a.v00 * b.v02 + a.v01 * b.v12 + a.v02 * b.v22;
            m.v10 = a.v10 * b.v00 + a.v11 * b.v10 + a.v12 * b.v20;
            m.v11 = a.v10 * b.v01 + a.v11 * b.v11 + a.v12 * b.v21;
            m.v12 = a.v10 * b.v02 + a.v11 * b.v12 + a.v12 * b.v22;
            m.v20 = a.v20 * b.v00 + a.v21 * b.v10 + a.v22 * b.v20;
            m.v21 = a.v20 * b.v01 + a.v21 * b.v11 + a.v22 * b.v21;
            m.v22 = a.v20 * b.v02 + a.v21 * b.v12 + a.v22 * b.v22;
            return m;
        }
        public static Matrix3x3 MultiplyMatrix(Matrix3x3 a, float b) {
            var m = default(Matrix3x3);
            m.v00 = a.v00 * b;
            m.v01 = a.v01 * b;
            m.v02 = a.v02 * b;
            m.v10 = a.v10 * b;
            m.v11 = a.v11 * b;
            m.v12 = a.v12 * b;
            m.v20 = a.v20 * b;
            m.v21 = a.v21 * b;
            m.v22 = a.v22 * b;
            return m;
        }
        public static Matrix3x3 DivideMatrix(Matrix3x3 a, float b) {
            var m = default(Matrix3x3);
            m.v00 = a.v00 / b;
            m.v01 = a.v01 / b;
            m.v02 = a.v02 / b;    
            m.v10 = a.v10 / b;
            m.v11 = a.v11 / b;
            m.v12 = a.v12 / b;          
            m.v20 = a.v20 / b;
            m.v21 = a.v21 / b;
            m.v22 = a.v22 / b;
            return m;
        }

        public static float Determinant(Matrix3x3 m)
        {
            float d;
            d =
            + m.v00 * m.v11 * m.v22
            + m.v01 * m.v12 * m.v20
            + m.v02 * m.v10 * m.v21
            - m.v00 * m.v12 * m.v21
            - m.v01 * m.v10 * m.v22
            - m.v02 * m.v11 * m.v20;
            return d;
        }

        public static float Determinant(Matrix4x4 m) {
            float d;
            d =
            + m.v00 * m.v11 * m.v22 * m.v33
            + m.v01 * m.v12 * m.v23 * m.v30
            + m.v02 * m.v13 * m.v20 * m.v31
            + m.v03 * m.v10 * m.v21 * m.v32
            - m.v03 * m.v12 * m.v21 * m.v30
            - m.v02 * m.v11 * m.v20 * m.v33
            - m.v01 * m.v10 * m.v23 * m.v32
            - m.v00 * m.v13 * m.v22 * m.v31;
            return d;
        }

        public static Matrix4x4 AddMatrix(Matrix4x4 a, Matrix4x4 b) {
            var m = default(Matrix4x4);
            for (int i = 0; i < a.length; i++)
                m[i] = a[i] + b[i];
            return m;
        }
        public static Matrix4x4 SubtractMatrix(Matrix4x4 a, Matrix4x4 b)
        {
            var m = default(Matrix4x4);
            for (int i = 0; i < a.length; i++)
                m[i] = a[i] - b[i];
            return m;
        }
        public static Matrix4x4 MultiplyMatrix(Matrix4x4 a, Matrix4x4 b)
        {
            var m = default(Matrix4x4);
            m.v00 = (((a.v00 * b.v00) + (a.v01 * b.v10)) + (a.v02 * b.v20)) + (a.v03 * b.v30);
            m.v01 = (((a.v00 * b.v01) + (a.v01 * b.v11)) + (a.v02 * b.v21)) + (a.v03 * b.v31);
            m.v02 = (((a.v00 * b.v02) + (a.v01 * b.v12)) + (a.v02 * b.v22)) + (a.v03 * b.v32);
            m.v03 = (((a.v00 * b.v03) + (a.v01 * b.v13)) + (a.v02 * b.v23)) + (a.v03 * b.v33);
			
            m.v10 = (((a.v10 * b.v00) + (a.v11 * b.v10)) + (a.v12 * b.v20)) + (a.v13 * b.v30);
            m.v11 = (((a.v10 * b.v01) + (a.v11 * b.v11)) + (a.v12 * b.v21)) + (a.v13 * b.v31);
            m.v12 = (((a.v10 * b.v02) + (a.v11 * b.v12)) + (a.v12 * b.v22)) + (a.v13 * b.v32);
            m.v13 = (((a.v10 * b.v03) + (a.v11 * b.v13)) + (a.v12 * b.v23)) + (a.v13 * b.v33);
			
            m.v20 = (((a.v20 * b.v00) + (a.v21 * b.v10)) + (a.v22 * b.v20)) + (a.v23 * b.v30);
            m.v21 = (((a.v20 * b.v01) + (a.v21 * b.v11)) + (a.v22 * b.v21)) + (a.v23 * b.v31);
            m.v22 = (((a.v20 * b.v02) + (a.v21 * b.v12)) + (a.v22 * b.v22)) + (a.v23 * b.v32);
            m.v23 = (((a.v20 * b.v03) + (a.v21 * b.v13)) + (a.v22 * b.v23)) + (a.v23 * b.v33);
			
            m.v30 = (((a.v30 * b.v00) + (a.v31 * b.v10)) + (a.v32 * b.v20)) + (a.v33 * b.v30);
            m.v31 = (((a.v30 * b.v01) + (a.v31 * b.v11)) + (a.v32 * b.v21)) + (a.v33 * b.v31);
            m.v32 = (((a.v30 * b.v02) + (a.v31 * b.v12)) + (a.v32 * b.v22)) + (a.v33 * b.v32);
            m.v33 = (((a.v30 * b.v03) + (a.v31 * b.v13)) + (a.v32 * b.v23)) + (a.v33 * b.v33);
            return m;
        }
        public static Matrix4x4 MultiplyMatrix(Matrix4x4 a, float b) {
            var m = default(Matrix4x4);
            for (int i = 0; i < a.length; i++)
                m[i] = a[i] * b;
            return m;
        }
        public static Matrix4x4 DivideMatrix(Matrix4x4 a, float b) {
            var m = default(Matrix4x4);
            for (int i = 0; i < a.length; i++)
            {
                m[i] = a[i] / b;
            }
            return m;
        }
#endif
        public static float Lerp(float from, float to, float t)
        {
            return from + (to - from) * t;
        }
        public static float LerpPercent(float value, float from, float to)
        {
            return (value - from) / (to - from);
        }
        public static float Clamp(float v, float min, float max)
        {
            if (v < min)
                return min;
            if (v > max)
                return max;
            return v;
        }
        public static float Clamp01(float val)
        {
            if (val > 1.0f)
                return 1.0f;
            if (val < 0.0f)
                return 0.0f;
            return val;
        }
        public static float Sqrt(float v)
        {
            return (float)Math.Sqrt(v);
        }
        public static float Abs(float v)
        {
            return (float)Math.Abs(v);
        }
        public static float Abs2(float v)
        {
            if (v < 0)
                return -v;
            return v;
        }
        public static float Min(float value, float compare) {
            if (compare < value)
                return compare;
            return value;
        }
        public static float Max(float value, float compare) {
            if (compare > value)
                return compare;
            return value;
        }
        public static float Acos(float v)
        {
            return (float)Math.Acos(v);
        }
        public static float Asin(float v)
        {
            return (float)Math.Asin(v);
        }
        public static float Atan(float v)
        {
            return (float)Math.Atan(v);
        }
        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }
        public static float Ceil(float v)
        {
            return (float)Math.Ceiling(v);
        }
        public static float Floor(float v)
        {
            return (float)Math.Floor(v);
        }
        public static float Sin(float v)
        {
            return (float)Math.Sin(v);
        }
        public static float Cos(float v)
        {
            return (float)Math.Cos(v);
        }
        public static float Tan(float v)
        {
            return (float)Math.Tan(v);
        }
        public static float Log(float v)
        {
            return (float)Math.Log(v);
        }
        public static float Log10(float v)
        {
            return (float)Math.Log10(v);
        }
        public static float Round(float v)
        {
            return (float)Math.Round(v);
        }
        public static float Sign(float v)
        {
            return Math.Sign(v);
        }
        public static float Pow(float x, float y)
        {
            return (float)Math.Pow(x, y);
        }
        public static float Exp(float v)
        {
            return (float)Math.Exp(v);
        }
        public static float RandomRange(float min, float max)
        {
			random = new Random();
            float amount = (float)random.NextDouble();
            return Lerp(min, max, amount);
        }
        public static float LoopNum(float start, float end, float value) {
            float diff = end - start;
            while (value < start) {
                value += diff;
            }
            while (value > end) {
                value -= diff;
            }
            return value;
        }
        public static float Barycentric(float val1, float val2, float val3, float amnt1, float amnt2) {
            return val1 + (val2 - val1) * amnt1 + (val3 - val2) * amnt2;
        }
        public static float CatmullRom(float val1, float val2, float val3, float val4, float amount) {
            var sqr = amount * amount;
            var cube = sqr * amount;
            return (0.5f * (2.0f * val2 + (val3 - val1) * amount + (2.0f * val1 - 5.0f * val2 + 4.0f * val3 - val4) * sqr + (3.0f * val2 - val1 - 3.0f * val3 + val4) * cube));
        }
        public static float Hermite(float val1, float tan1, float val2, float tan2, float amount) {
            float sqr = amount * amount;
            float cube = sqr * amount;
            amount = Clamp01(amount);
            if (amount == 0f)
                return val1;
            else if (amount == 1f)
                return val2;
            else
                return (2 * val1 - 2 * val2 + tan2 + tan1) * cube + (3 * val2 - 3 * val1 - 2 * tan1 - tan2) * sqr + tan1 * amount + val1;
        }
        public static float SmoothStep(float val1, float val2, float amount) {
            var sqr = amount * amount;
            var cube = sqr * amount;
            amount = Clamp01(amount);
            return (2 * val1 - 2 * val2) * cube + (3 * val2 - 3 * val1) * sqr + val1;
        }
        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime) {
            smoothTime = Max(0.0001f, smoothTime);
            var num = 2f / smoothTime;
            var num2 = num * deltaTime;
            var d = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            var val1 = current - target;
            var val2 = target;
            var maxLength = maxSpeed * smoothTime;

            val1 = Max(val1, maxLength);
            target = current - val1;

            var val3 = (currentVelocity + num * val1) * deltaTime;
            currentVelocity = (currentVelocity - num * val3) * d;
            return target + (val1 + val3) * d;
        }
        public static float Deg2Rad(float degrees) {
            return degrees * 0.017453292519943295769236907684886f;
        }
        public static float Rad2Deg(float radians) {
            return radians * 57.295779513082320876798154814105f;
        }
    }
}
