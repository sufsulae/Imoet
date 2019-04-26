//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct OffsetF {
        public float left,right, top, bottom;
        public OffsetF(float top, float right, float bottom, float left) {
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.left = left;
        }
        public float this[int idx] {
            get {
                switch (idx) {
                    case 0: return top;
                    case 1: return right;
                    case 2: return bottom;
                    case 3: return left;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public static bool operator ==(OffsetF l, OffsetF r) {
            for (int i = 0; i < 4; i++)
                if (l[i] != r[i])
                    return false;
            return true;
        }
        public static bool operator !=(OffsetF l, OffsetF r) {
            for (int i = 0; i < 4; i++)
                if (l[i] == r[i])
                    return false;
            return true;
        }
        public override int GetHashCode()
        {
            return top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode() + left.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is OffsetF)
                return (OffsetF)obj == this;
            return false;
        }
        public override string ToString()
        {
            return string.Format("top:{0}, right:{1}, bottom:{2}, left:{3}", top, right, bottom, left);
        }
    }
}
