using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJ_X100_memory_writer.Util
{
    internal class HexUtils
    {
        public string SwapEndianHex(string strValue)
        {
            int value;
            if(strValue == null || strValue.Equals(""))
            {
                return "";
            }
            if (strValue.Equals("AUTO"))
            {
                return "E480";
            }
             try
            {
                // strValueをint型に変換します。
                value = int.Parse(strValue);
            }
            catch (Exception)
            {
                throw new Exception("AUTO あるいは 数値ではない値が書き込まれています");
            }


            // 数値を16進数に変換し、4桁が保証されるようにします。
            string hex = value.ToString("X4");
            // 16進数表現のエンディアン（バイトオーダー）をスワップします。
            return hex.Substring(2, 2) + hex.Substring(0, 2);
        }

    }
}
