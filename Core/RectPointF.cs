using System;

namespace Imoet
{
    [Serializable]
    public struct RectPointF
    {
        public Vec3 TopLeft, TopRight, BottomRight, BottomLeft;
        public RectPointF(Vec3 TopLeft, Vec3 TopRight, Vec3 BottomRight, Vec3 BottomLeft) {
            this.TopLeft = TopLeft;
            this.TopRight = TopRight;
            this.BottomRight = BottomRight;
            this.BottomLeft = BottomLeft;
        }
        public Vec3 this[int idx] {
            get {
                switch (idx) {
                    case 0: return TopLeft;
                    case 1: return TopRight;
                    case 2: return BottomRight;
                    case 3: return BottomLeft;
                }
                throw new IndexOutOfRangeException();
            }
            set {
                switch (idx) {
                    case 0: TopLeft = value; return;
                    case 1: TopRight = value; return;
                    case 2: BottomRight = value; return;
                    case 3: BottomLeft = value; return;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public static implicit operator RectPoint(RectPointF v) {
            return new RectPoint(v.TopLeft, v.TopRight, v.BottomRight, v.BottomLeft);
        }
        public static implicit operator RectPointF(RectPoint v) {
            return new RectPointF(v.TopLeft, v.TopRight, v.BottomRight, v.BottomLeft);
        }
        public static bool operator ==(RectPointF l, RectPointF r) {
            for (int i = 0; i < 4; i++) {
                if (l[i] != r[i])
                    return false;
            }
            return true;
        }
        public static bool operator !=(RectPointF l, RectPointF r)
        {
            for (int i = 0; i < 4; i++)
            {
                if (l[i] == r[i])
                    return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            for (int i = 0; i < 4; i++)
                hashCode += this[i].GetHashCode();
            return hashCode;
        }
        public override bool Equals(object obj)
        {
            if (obj is RectPointF)
                return (RectPointF)obj == this;
            return false;
        }
        public override string ToString()
        {
            return string.Format(
                "TopLeft:{0}, TopRight:{1}, BottomLeft:{2}, BottomRight:{3}",
                TopLeft, TopRight, BottomLeft, BottomRight);
        }
    }
}
