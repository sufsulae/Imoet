using System.Text;
namespace Imoet.Utility
{
    public static class Util
    {
        public static string ConvertToWellNum(int num)
        {
            var resBuilder = new StringBuilder();
            string numStr = num.ToString();
            int strLen = numStr.Length;
            for (int i = strLen - 1; i > -1; i--)
            {
                resBuilder.Append(numStr[(strLen - 1) - i]);
                if (i != 0 && i % 3 == 0)
                    resBuilder.Append(".");
            }
            return resBuilder.ToString();
        }

        public static string ConvertToWellString(string str)
        {
            var strBuilder = new StringBuilder();
            str = str.ToLowerInvariant();
            strBuilder.Append(char.ToUpperInvariant(str[0]));
            strBuilder.Append(str.Substring(1, str.Length - 1));
            return strBuilder.ToString();
        }
    }
}
