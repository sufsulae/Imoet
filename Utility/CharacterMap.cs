//Imoet Library
//Copyright © 2018 Yusuf Sulaeman
#if IMOET_INCLUDE_UTILITY
namespace Imoet.Utility
{
    using System;
    using System.Text;
    public static class CharacterMap
    {
        public const string standardChar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789;=,-./`[\\]')!@#$%^&*(:+<_>?~{|}\"\'\n\\\0\a\b\f\r\t\v ";
        public static string DecodeToString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            int dataLen = data.Length;
            for (int i = 0; i < dataLen; i++)
                sb.Append(standardChar[data[i]]);
            return sb.ToString();
        }
        public static void EncodeToData(string text, byte[] buffer)
        {
            int txtLen = text.Length;
            int buffLen = buffer.Length;
            int choosenLen = buffLen;

            if (buffLen > txtLen)
                choosenLen = txtLen;

            for (int i = 0; i < choosenLen; i++) {
                buffer[i] = GetIdx(text[i]);
            }
        }
        public static byte[] EncodeToData(string text)
        {
            int txtLen = text.Length;
            byte[] dataLen = new byte[txtLen];
            EncodeToData(text, dataLen);
            return dataLen;
        }

        public static byte GetIdx(char c)
        {
            return (byte)standardChar.IndexOf(c);
        }
        public static char GetChar(byte idx)
        {
            return standardChar[(int)idx];
        }
        public static char GetNumChar(int id)
        {
            if (id < 0 || id > 9 )
                throw new ArgumentOutOfRangeException("Char ID is out of range");
            return standardChar[id+52];
        }
        public static char GetLowerChar(int id)
        {
            if (id < 0 || id > 25)
                throw new ArgumentOutOfRangeException("Char ID is out of range");
            return standardChar[id];
        }
        public static char GetUpperChar(int id)
        {
            if (id < 0 || id > 25)
                throw new ArgumentOutOfRangeException("Char ID is out of range");
            return standardChar[id + 26];
        }
        public static char GetSymbolChar(int id)
        {
            if(id < 0 || id > 32)
                throw new ArgumentOutOfRangeException("Char ID is out of range");
            return standardChar[id + 62];
        }
        public static string GetEscapeSeqString(this char c) {
            switch (c) {
                case '\'':
                    return "\\" + "\'";
                case '\"':
                    return "\\" + "\"";
                case '\n':
                    return "\\" + "n";
                case '\\':
                    return "\\" + "\\";
                case '\0':
                    return "\\" + "0";
                case '\a':
                    return "\\" + "a";
                case '\b':
                    return "\\" + "b";
                case '\f':
                    return "\\" + "f";
                case '\r':
                    return "\\" + "r'";
                case '\t':
                    return "\\" + "t";
                case '\v':
                    return "\\" + "v";
            }
            return c + "";
        }
    }
}
#endif