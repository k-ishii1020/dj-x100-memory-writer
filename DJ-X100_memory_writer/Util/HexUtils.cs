using System;
using System.Collections.Generic;
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




        public string SwapEndianHexForFourHex(string strValue)
        {
            int value;
            if(strValue == null || strValue.Equals(""))
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

        public string SwapEndianHexForEightDigits(string strValue)
        {
            int value;
            if (strValue == null || strValue.Equals(""))
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
    }
}
