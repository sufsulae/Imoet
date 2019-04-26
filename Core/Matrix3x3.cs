//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
#if IMOET_INCLUDE_MATH
    using System;
    [Serializable]
    public unsafe struct Matrix3x3
    {
        public float
            v00,
            v01,
            v02,
            v10,
            v11,
            v12,
            v20,
            v21,
            v22;
        public float this[int i] {
            get {
                switch (i) {
                    case 0: return v00;
                    case 1: return v01;
                    case 2: return v02;
                    case 3: return v10;
                    case 4: return v11;
                    case 5: return v12;
                    case 6: return v20;
                    case 7: return v21;
                    case 8: return v22;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set {
                switch (i)
                {
                    case 0: v00 = value; break;
                    case 1: v01 = value; break;
                    case 2: v02 = value; break;
                    case 3: v10 = value; break;
                    case 4: v11 = value; break;
                    case 5: v12 = value; break;
                    case 6: v20 = value; break;
                    case 7: v21 = value; break;
                    case 8: v22 = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
        public float this[int x, int y] {
            get {
                var idx = x + y * 9;
                return this[idx];
            }
            set {
                var idx = x + y * 9;
                this[idx] = value;
            }
        }
        public int length {
            get { return 9; }
        }

#if IMOET_UNSAFE
        public float* Ptr
        {
            get
            {
                fixed (Matrix3x3* m = &this)
                {
                    return (float*) m;
                }
            }
        }
#endif
        //Operator
        public static Matrix3x3 operator +(Matrix3x3 a, Matrix3x3 b)
        {
#if IMOET_UNSAFE
            var m = a;
            UMath.MassAdd(a.Ptr,b.Ptr,m.Ptr,9);
            return m;
#else
            return UMath.AddMatrix(a, b);
#endif
        }
        public static Matrix3x3 operator -(Matrix3x3 a, Matrix3x3 b)
        {
#if IMOET_UNSAFE
            Matrix3x3 m = new Matrix3x3();
            UMath.MassSubtract(a.Ptr, b.Ptr, m.Ptr, 9);
            return m;
#else
            return UMath.SubtractMatrix(a, b);
#endif
        }
        public static Matrix3x3 operator *(Matrix3x3 a, Matrix3x3 b)
        {
            return UMath.MultiplyMatrix(a, b);
        }
        public static Matrix3x3 operator *(Matrix3x3 a, float b) {
            return UMath.MultiplyMatrix(a, b);
        }
        public static Matrix3x3 operator /(Matrix3x3 a, float b) {
            return UMath.DivideMatrix(a, b);
        }
        public static Matrix3x3 operator -(Matrix3x3 a) {
            return UMath.MultiplyMatrix(a, -1);
        }
    }
#endif
}
