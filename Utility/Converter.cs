//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
#if IMOET_INCLUDE_UTILITY && IMOET_UNSAFE
namespace Imoet.Utility
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;
    /*
     * I used Undocumented C# keyword
     * Example:
     * 
     * (__makeref(object),__refValue(TypedReference,System.Type),__refType(TypedReference))
     * int x = 3;
     * var xRef = __makeref(x); // Return TypedReference (struct, cast-able to IntPtr)
     * Console.WriteLine(__reftype(xRef)); // Prints "System.Int32"
     * Console.WriteLine(__refvalue(xRef, int)); // Prints "3"
     *  __refvalue(xRef, int) = 10;
     * Console.WriteLine(__refvalue(xRef, int)); // Prints "10"
     * 
     * (__argList)
     * public int paramLength(__arglist)
     * {
     *      ArgIterator iterator = new ArgIterator(__arglist);
     *      return iterator.GetRemainingCount();
     * }
     * 
     * int x = this.paramLength(__arglist(49,34,54,6,"Manimoy")); // returns 5 
     * OR
     * TypedReference tf = iterator.GetNextArg();
     * print(TypedReference.ToObject(tf)) //return 5;
     */
    public static class Converter
    {
        public static byte[] Object2Bytes(Object obj)
        {
            if (obj == null)
                return null;

            using (MemoryStream stream = new MemoryStream()) {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(new byte[] { 10, 10 });
            }

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, obj);
                
                return stream.ToArray();
            }
        }

        public static object Bytes2Object(byte[] data)
        {
            if (data == null || data.Length == 0)
                return null;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                stream.Write(data, 0, data.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return bf.Deserialize(stream);
            }
        }
        public static unsafe byte[] Struct2Byte<T>(T obj) where T : struct {
            int len = SizeOf<T>();
            byte[] dt = new byte[len];
            var reff = __makeref(obj);
            byte* raw = (byte*)*((IntPtr*)&reff);
            for (int i = 0; i < len; i++, raw++)
            {
                dt[i] = *raw;
            }
            return dt;
        }
        public static unsafe T Byte2Struct<T>(byte[] data) where T : struct {
            T obj = default(T);
            var reff = __makeref(obj);
            byte* reffRaw = (byte*)*((IntPtr*)&reff);
            var dataLen = data.Length;
            for (int i = 0; i < dataLen; i++) {
                reffRaw[i] = data[i];
            }
            return __refvalue(reff,T);
        }

        //Source: http://benbowen.blog/post/fun_with_makeref/
        public static unsafe int SizeOf<T>() where T : struct
        {
            Type type = typeof(T);

            TypeCode typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return sizeof(bool);
                case TypeCode.Char:
                    return sizeof(char);
                case TypeCode.SByte:
                    return sizeof(sbyte);
                case TypeCode.Byte:
                    return sizeof(byte);
                case TypeCode.Int16:
                    return sizeof(short);
                case TypeCode.UInt16:
                    return sizeof(ushort);
                case TypeCode.Int32:
                    return sizeof(int);
                case TypeCode.UInt32:
                    return sizeof(uint);
                case TypeCode.Int64:
                    return sizeof(long);
                case TypeCode.UInt64:
                    return sizeof(ulong);
                case TypeCode.Single:
                    return sizeof(float);
                case TypeCode.Double:
                    return sizeof(double);
                case TypeCode.Decimal:
                    return sizeof(decimal);
                case TypeCode.DateTime:
                    return sizeof(DateTime);
                default:
                    T[] tArray = new T[2];
                    GCHandle tArrayPinned = GCHandle.Alloc(tArray, GCHandleType.Pinned);
                    try
                    {
                        TypedReference tRef0 = __makeref(tArray[0]);
                        TypedReference tRef1 = __makeref(tArray[1]);
                        IntPtr ptrToT0 = *((IntPtr*)&tRef0);
                        IntPtr ptrToT1 = *((IntPtr*)&tRef1);

                        return (int)(((byte*)ptrToT1) - ((byte*)ptrToT0));
                    }
                    finally
                    {
                        tArrayPinned.Free();
                    }
            }
        }
        public unsafe static TOut Reinterpret<TIn, TOut>(TIn curValue, int sizeBytes) 
            where TIn : struct 
            where TOut : struct
        {
            TOut result = default(TOut);

            TypedReference resultRef = __makeref(result);
            byte* resultPtr = (byte*)*((IntPtr*)&resultRef);

            TypedReference curValueRef = __makeref(curValue);
            byte* curValuePtr = (byte*)*((IntPtr*)&curValueRef);

            for (int i = 0; i < sizeBytes; ++i)
                resultPtr[i] = curValuePtr[i];
            return result;
        }

        public static T Bytes2Object<T>(byte[] data) {
            return (T)Bytes2Object(data);
        }
        public static byte[] ToBytes<T>(this T obj) where T : struct {
            return Struct2Byte<T>(obj);
        }
    }
}
#endif