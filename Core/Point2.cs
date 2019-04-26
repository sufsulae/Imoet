//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Point2
    {
        public int y;
        public int x;

        public Point2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Point2(int value) {
            this.x = this.y = value;
        }

        public static Point2 operator +(Point2 a, Point2 b)
        {
            return new Point2(a.x + b.x, a.y + b.y);
        }

        public static Point2 operator -(Point2 a, Point2 b)
        {
            return new Point2(a.x - b.x, a.y - b.y);
        }
        public static Point2 operator *(Point2 a, Point2 b)
        {
            return new Point2(a.x * b.x, a.y * b.y);
        }
        public static Point2 operator *(Point2 a, int b)
        {
            return new Point2(a.x * b, a.y * b);
        }
        public static Point2 operator *(int a, Point2 b)
        {
            return new Point2(a * b.x, a * b.y);
        }
        public static Point2 operator *(Point2 a, float b) {
            return a * (int)b;
        }
        public static Point2 operator *(float a, Point2 b) {
            return b * (int)a;
        }
        public static Point2 operator /(Point2 a, Point2 b) {
            return new Point2(a.x / b.x, a.y / b.y);
        }
        public static Point2 operator /(Point2 a, int b) {
            return new Point2(a.x / b, a.y / b);
        }
        public static Point2 operator /(Point2 a, float b) {
            return a / (int)b;
        }
        public static Point2 operator -(Point2 a) {
            return a * -1;
        }
        public static implicit operator Vec2(Point2 p)
        {
            return new Vec2(p.x, p.y);
        }
        public static implicit operator Point2(Vec2 p)
        {
            return new Point2((int)p.x, (int)p.y);
        }
        public static implicit operator Point3(Point2 p) {
            return new Point3(p.x, p.y, 0);
        }
        public static implicit operator Point2(Point3 p) {
            return new Point2(p.x, p.y);
        }

        public static Point2 noPoint {
            get { return new Point2(-1,-1);}
        }
        public static Point2 zero {
            get { return new Point2(0, 0); }
        }
        public static Point2 one {
            get { return new Point2(1, 1); }
        }

        public static bool operator ==(Point2 left, Point2 right)
        {
            return left.x == right.x && left.y == right.y;
        }
        public static bool operator !=(Point2 left, Point2 right)
        {
            return left.x != right.x || left.y == right.y;
        }
        public override bool Equals(object obj)
        {
            if (obj is Point2)
            {
                return this == (Point2)obj;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.x.GetHashCode() + this.y.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("x:{0} y:{1}", this.x, this.y);
        }
    }
}