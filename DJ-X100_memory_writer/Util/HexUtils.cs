using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJ_X100_memory_writer.Util
{
    internal class HexUtils
    {
        public string SwapEndianHexForTwoDigits(string strValue)
        {
            int value;
            if (strValue == null || strValue.Equals(""))
            {
                return "80";
            }
            try
            {
                value = int.Parse(strValue);
            }
            catch (Exception)
            {
                throw new Exception("数値ではない値が書き込まれています");
            }
            string hex = value.ToString("X2");

            return hex.Substring(1, 1) + hex.Substring(0, 1);
        }

        public string HexToDecimal(string hexValue)
        {
            if (string.IsNullOrEmpty(hexValue))
            {
                return "";
            }

            try
            {
                int decimalValue = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

                return decimalValue.ToString();
            }
            catch (Exception)
            {
                throw new Exception("有効な16進数値ではありません");
            }
        }

        public string SwapEndianHexForFourHex(string strValue)
        {
            int value;
            if(strValue == null || strValue.Equals("") || (strValue.Equals("OFF")))
            {
                return "0180";
            }
            if (strValue.Equals("AUTO"))
            {
                return "E480";
            }
             try
            {
                value = int.Parse(strValue);
            }
            catch (Exception)
            {
                throw new Exception("AUTO あるいは 数値ではない値が書き込まれています");
            }
            string hex = value.ToString("X4");

            return hex.Substring(2, 2) + hex.Substring(0, 2);
        }



        public bool DecodeFourHex(string hexString, out bool flag, out int value)
        {
            // 初期化
            flag = false;
            value = 0;

            // 16進数の文字列を整数に変換
            int num;
            try
            {
                num = Convert.ToInt32(hexString, 16);
            }
            catch (FormatException)
            {
                return false;
            }

            // リトルエンディアンなので、ビットを反転
            int bitFields = ((num & 0x00FF) << 8) | ((num & 0xFF00) >> 8);

            // AUTOフィールド（最上位ビット）と値フィールド（その他のビット）を取り出す
            flag = (bitFields & 0x8000) != 0;  // 最上位ビットを調べる
            value = bitFields & 0x7FFF;  // 最上位ビットを除く

            return true;
        }




        public string SwapEndianHexForEightDigits(string strValue)
        {
            int value;
            if (strValue == null || strValue.Equals("") || (strValue.Equals("ALL")))
            {
                return "01000080";
            }
            try
            {
                value = int.Parse(strValue);
            }
            catch (Exception)
            {
                throw new Exception("数値ではない値が書き込まれています");
            }
            string hex = value.ToString("X8");

            return hex.Substring(6, 2) + hex.Substring(4, 2) + hex.Substring(2, 2) + hex.Substring(0, 2);
        }

        public bool DecodeEightHex(string hexString, out bool all, out long value)
        {
            // 初期化
            all = false;
            value = 0;

            // 16進数の文字列を整数に変換
            long num;
            try
            {
                num = Convert.ToInt64(hexString, 16);
            }
            catch (FormatException)
            {
                return false;
            }

            // リトルエンディアンなので、ビットを反転
            long bitFields = ((num & 0x000000FF) << 24) | ((num & 0x0000FF00) << 8) | ((num & 0x00FF0000) >> 8) | ((num & 0xFF000000) >> 24);

            // AUTOフィールド（最上位ビット）と値フィールド（その他のビット）を取り出す
            all = (bitFields & 0x80000000) != 0;  // 最上位ビットを調べる
            value = bitFields & 0x7FFFFFFF;  // 最上位ビットを除く

            return true;
        }


    }
}
