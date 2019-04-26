//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
#if IMOET_INCLUDE_UTILITY && IMOET_UNSAFE
namespace Imoet.Utility
{
    using System;
    public static unsafe class UnsafeTool
    {
        public static void internalMemCopy(byte* dst, byte* src, int dCount)
        {
            if (dCount >= 16)
            {
                do
                {
                    *(int*)dst[0] = *(int*)src[0];
                    *(int*)dst[4] = *(int*)src[4];
                    *(int*)dst[8] = *(int*)src[8];
                    *(int*)dst[12] = *(int*)src[12];
                    dst += 16;
                    src += 16;
                }
                while ((dCount -= 16) >= 16);
            }
            if (dCount > 0)
            {
                if ((dCount & 8) != 0)
                {
                    *(int*)dst[0] = *(int*)src[0];
                    *(int*)dst[4] = *(int*)src[4];
                    dst += 8;
                    src += 8;
                }
                if ((dCount & 4) != 0)
                {
                    *(int*)dst = *(int*)src;
                    dst += 4;
                    src += 4;
                }
                if ((dCount & 2) != 0)
                {
                    *(short*)dst = *(short*)src;
                    dst += 2;
                    src += 2;
                }
                if ((dCount & 1) != 0)
                {
                    *dst = *src;
                    dst++;
                    src++;
                }
            }
        }
        public static string PtrToString (IntPtr ptr)
        {
            return System.Runtime.InteropServices.Marshal.PtrToStringAuto(ptr);
        }

        public static System.IntPtr StringToPtr(string str)
        {
            return System.Runtime.InteropServices.Marshal.StringToHGlobalAuto(str);
        }
    }
}
#endif
