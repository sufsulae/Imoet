//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Offset {
        public int left,right, top, bottom;
        public Offset(int top, int right, int bottom, int left) {
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.left = left;
        }
        public int this[int idx] {
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
        public static bool operator ==(Offset l, Offset r) {
            for (int i = 0; i < 4; i++)
                if (l[i] != r[i])
                    return false;
            return true;
        }
        public static bool operator !=(Offset l, Offset r) {
            for (int i = 0; i < 4; i++)
                if (l[i] == r[i])
                    return false;
            return true;
        }
        public static implicit operator Offset(OffsetF val) {
            return new Offset((int)val.top, (int)val.right, (int)val.bottom, (int)val.left);
        }
        public static implicit operator OffsetF(Offset val) {
            return new OffsetF(val.top, val.right, val.bottom, val.left);
        }
        public override int GetHashCode()
        {
            return top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode() + left.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is Offset)
                return (Offset)obj == this;
            return false;
        }
        public override string ToString()
        {
            return string.Format("top:{0}, right:{1}, bottom:{2}, left:{3}", top, right, bottom, left);
        }
    }
}
