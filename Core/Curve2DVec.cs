//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
namespace Imoet
{
#if IMOET_INCLUDE_MATH
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class Curve2DVec
    {
        private List<CurvePoint<Vec2>> m_points;

        public int pointCount {
            get { return m_points.Count; }
        }

        public CurvePoint<Vec2> this[int idx]
        {
            get { return GetPoint(idx); }
            set { SetPoint(idx, value); }
        }

        public Curve2DVec() {
            m_points = new List<CurvePoint<Vec2>>();
        }
        public void AddPoint(int idx, Vec2 point) {
            var newPoint = new CurvePoint<Vec2>();
            newPoint.point = newPoint.handleA = newPoint.handleB = point;
            m_points.Insert(idx, newPoint);
        }
        public void AddPoint(int idx, CurvePoint<Vec2> point) {
            m_points.Insert(idx, point);
        }
        public void AddPoint(Vec2 point) {
            var newPoint = new CurvePoint<Vec2>();
            newPoint.point = newPoint.handleA = newPoint.handleB = point;
            m_points.Add(newPoint);
        }
        public void AddPoint(CurvePoint<Vec2> point) {
            m_points.Add(point);
        }
        public void RemovePoint(int idx) {
            m_points.RemoveAt(idx);
        }
        public CurvePoint<Vec2> GetPoint(int idx) {
            return m_points[idx];
        }
        public void SetPoint(int idx, CurvePoint<Vec2> point) {
            m_points[idx] = point;
        }

        public Vec2 Evaluate(float amount, CurveType type) {
            if (m_points.Count < 2)
                throw new ArgumentException("Number of CurverPoint is less than 2");
            //Evaluate Point Only At particular time

            //Find Which "Pair" that we want to calculate
            var tr = 1.0f / (m_points.Count-1);
            var tIdxF = UMath.LerpPercent(amount - 0.0000001f, 0.0f, tr);
            var tIdx = (int)UMath.Floor(tIdxF);
            var r = tIdxF - tIdx;

            //Get Point
            var p0 = m_points[tIdx].point;
            var p1 = m_points[tIdx].handleB;
            var p2 = m_points[tIdx + 1].handleA;
            var p3 = m_points[tIdx + 1].point;

            //Apply calculation
            var u = 1f - r;
            var u2 = u * u;
            var u3 = u2 * u;

            var r2 = r * r;
            var r3 = r2 * r;
            switch (type) {
                case CurveType.Linear:
                    return p0 + (p1 - p0) * r;
                case CurveType.Bezier:
                    return u3 * p0 + (3f * u2 * r) * p1 + (3f * u * r2) * p2 + r3 * p3;
                case CurveType.CatmullRom:
                    return (0.5f * (2.0f * p0 + (p3 - p1) * r + (2.0f * p1 - 5.0f * p0 + 4.0f * p3 - p2) * r2 + (-p1 + 3.0f * p0 - 3.0f * p3 + p2) * r3));
                default:
                    throw new ArgumentException("Something Goes Wrong");
            }
        }
    }
#endif
}
