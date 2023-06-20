using Microsoft.VisualBasic;
using System.Text;

namespace DJ_X100_memory_writer.Util
{
    internal class DataGridViewUtils
    {
        public int GetAdjustedLength(string s)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");

            int count = 0;

            foreach (char c in s)
            {
                if (sjisEnc.GetByteCount(c.ToString()) > 1)
                {
                    // 全角文字の場合
                    count += 2;
                }
                else
                {
                    // 半角文字の場合
                    count += 1;
                }
            }

            return count;
        }

        public string GetConvertedByteCountShiftJis(string s, ref int length)
        {
            string converted = Strings.StrConv(s, VbStrConv.Narrow, 0x0411);
            converted = converted.Replace("　", " ");  // 全角スペースを半角スペースに置換

            length = Encoding.GetEncoding("Shift_JIS").GetByteCount(converted);

            return converted;
        }

        public string CutToLength(string input, int maxLength)
        {
            StringBuilder output = new StringBuilder();
            int currentLength = 0;

            foreach (char c in input)
            {
                int charDisplayLength = c > '\u007f' ? 2 : 1;

                if (currentLength + charDisplayLength > maxLength)
                {
                    break;
                }

                output.Append(c);
                currentLength += charDisplayLength;
            }

            return output.ToString();
        }
    }
}
