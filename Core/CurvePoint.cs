namespace Imoet
{
#if IMOET_INCLUDE_MATH
    [System.Serializable]
    public struct CurvePoint<T> where T : struct {
        public T point, handleA, handleB;
    }
#endif
}
