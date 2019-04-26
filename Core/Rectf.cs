//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct Rectf
    {
        public float x, y, width, height;
        public Rectf(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        
        //Property
        public float xMax{
            get { return x + width; }
            set { this.width = value - x; }
        }
        public float yMax {
            get { return y + height; }
            set { this.height = value - y; }
        }
        public Vec2 center {
            get { return new Vec2(xMax / 2, yMax / 2); }
        }
        public Vec2 centerOrigin {
            get { return new Vec2(x + width / 2, y + height / 2); }
        }
        public Vec2 position {
            get { return new Vec2(x, y); }
            set { this.x = value.x; this.y = value.y; }
        }
        public Vec2 size {
            get { return new Vec2(width, height); }
            set { this.width = value.x; this.height = value.y; }
        }

        //Methods
        public bool Contains(Vec2 point) {
            return point.x >= this.x && point.x < this.xMax && point.y >= this.y && point.y < this.yMax;
        }
        public bool Contains(Vec3 point) {
            return point.x >= this.x && point.x < this.xMax && point.y >= this.y && point.y < this.yMax;
        }
        public bool Contains(Rectf rect){
            return this.x <= rect.x && rect.xMax <= this.xMax && this.y <= rect.y && rect.yMax <= this.yMax;
        }
        public bool IntersectWith(Rectf rect){
            return rect.x < this.xMax && this.x < rect.xMax && rect.y < this.yMax && this.y < rect.yMax;
        }
        public Vec2[] GetCorner()
        {
            Vec2[] res = new Vec2[4];
            GetCorner(res);
            return res;
        }
        public void GetCorner(Vec2[] corner)
        {
            if (corner == null)
                throw new ArgumentNullException("Corner must not null array");
            int count = corner.Length;
            count = (int)UMath.Clamp(count, 0, 4);
            for (int i = 0; i < count; i++)
            {
                corner[0] = new Vec2(x, y);
                corner[1] = new Vec2(x + width, y);
                corner[2] = new Vec2(x, y + height);
                corner[3] = new Vec2(x + width, y + height);
            }
        }
        public override string ToString() {
            return "{" + x + "," + y + "," + width + "," + height + "}";
        }
        public static implicit operator Rect(Rectf r) {
            return new Rect((int)r.x, (int)r.y, (int)r.width, (int)r.height);
        }
    }
}
