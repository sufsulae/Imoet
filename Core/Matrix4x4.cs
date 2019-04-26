//Imoet Library
//Copyright © 2018 Yusuf Sulaeman

namespace Imoet
{
#if IMOET_INCLUDE_MATH
    using System;
    [Serializable]
    public unsafe struct Matrix4x4
    {
        public float
            v00,
            v01,
            v02,
            v03,
            v10,
            v11,
            v12,
            v13,
            v20,
            v21,
            v22,
            v23,
            v30,
            v31,
            v32,
            v33;
        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return v00;
                    case 1: return v01;
                    case 2: return v02;
                    case 3: return v03;
                    case 4: return v10;
                    case 5: return v11;
                    case 6: return v12;
                    case 7: return v13;
                    case 8: return v20;
                    case 9: return v21;
                    case 10: return v22;
                    case 11: return v23;
                    case 12: return v30;
                    case 13: return v31;
                    case 14: return v32;
                    case 15: return v33;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0: v00 = value; break;
                    case 1: v01 = value; break;
                    case 2: v02 = value; break;
                    case 3: v03 = value; break;
                    case 4: v10 = value; break;
                    case 5: v11 = value; break;
                    case 6: v12 = value; break;
                    case 7: v13 = value; break;
                    case 8: v20 = value; break;
                    case 9: v21 = value; break;
                    case 10: v22 = value; break;
                    case 11: v23 = value; break;
                    case 12: v30 = value; break;
                    case 13: v31 = value; break;
                    case 14: v32 = value; break;
                    case 15: v33 = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
        public float this[int x, int y]
        {
            get
            {
                var idx = x + y * 16;
                return this[idx];
            }
            set
            {
                var idx = x + y * 16;
                this[idx] = value;
            }
        }
        public int length { get { return 16; } }

#if IMOET_UNSAFE
        public float* Ptr
        {
            get
            {
                fixed (Matrix4x4* m = &this)
                {
                    return (float*) m;
                }
            }
        }
#endif
        //Operator
        public static Matrix4x4 operator +(Matrix4x4 a, Matrix4x4 b)
        {
#if IMOET_UNSAFE
            Matrix4x4 m = new Matrix4x4();
            UMath.MassAdd(a.Ptr,b.Ptr,m.Ptr,9);
            return m;
#else
            return UMath.AddMatrix(a, b);
#endif
        }
        public static Matrix4x4 operator -(Matrix4x4 a, Matrix4x4 b)
        {
#if IMOET_UNSAFE
            Matrix4x4 m = new Matrix4x4();
            UMath.MassSubtract(a.Ptr, b.Ptr, m.Ptr, 9);
            return m;
#else
            return UMath.SubtractMatrix(a, b);
#endif
        }
        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b)
        {
            return UMath.MultiplyMatrix(a, b);
        }
        public static Matrix4x4 operator *(Matrix4x4 a, float b) {
            return UMath.MultiplyMatrix(a, b);
        }
        public static Matrix4x4 operator /(Matrix4x4 a, float b)
        {
            return UMath.DivideMatrix(a, b);
        }
    }
#endif
}
