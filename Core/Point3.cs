//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Point3
    {
        public int z;
        public int y;
        public int x;

        #region Contructor
        public Point3(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Point3(int value) {
            this.x = this.y = this.z = value;
        }
        #endregion
        public static Point3 one {
            get { return new Point3(1, 1, 1); }
        }
        public static Point3 zero {
            get { return new Point3(); }
        }

        public static Point3 operator +(Point3 left, Point3 right)
        {
            var res = left;
            res.x = left.x + right.x;
            res.y = left.y + right.y;
            res.z = left.z + right.z;
            return res;
        }
        public static Point3 operator -(Point3 left, Point3 right)
        {
            var res = left;
            res.x = left.x - right.x;
            res.y = left.y - right.y;
            res.z = left.z - right.z;
            return res;
        }
        public static Point3 operator -(Point3 val) {
            return val * -1;
        }
        public static Point3 operator *(Point3 left, int right)
        {
            var res = left;
            res.x = left.x * right;
            res.y = left.y * right;
            res.z = left.z * right;
            return res;
        }
        public static Point3 operator *(int left, Point3 right) {
            return right * left;
        }
        public static Point3 operator *(Point3 left, float right) {
            return left * (int)right;
        }
        public static Point3 operator *(float left, Point3 right) {
            return (int)left * right;
        }
        public static Point3 operator /(Point3 left, int right)
        {
            var res = left;
            res.x = left.x / right;
            res.y = left.y / right;
            res.z = left.z / right;
            return res;
        }
        public static Point3 operator /(Point3 left, float right) {
            return left / (int)right;
        }

        public static implicit operator Vec3(Point3 v) {
            return new Vec3(v.x, v.y, v.z);
        }
        public static implicit operator Point3(Vec3 v) {
            return new Point3((int)v.x, (int)v.y, (int)v.z);
        }

        public static bool operator ==(Point3 left, Point3 right) {
            return left.x == right.x && left.y == right.y && left.z == right.z;
        }
        public static bool operator !=(Point3 left, Point3 right) {
            return left.x != right.x || left.y == right.y || left.z == right.z;
        }
        public override bool Equals(object obj)
        {
            if (obj is Point3) {
                return this == (Point3)obj;
            }
            return false;
        }
        public override int GetHashCode() {
            return UMath.Max(x + y + z,int.MaxValue).GetHashCode();
        }
        public override string ToString() {
            return string.Format("{{0},{1},{2}|", x, y, z);
        }

    }
}
