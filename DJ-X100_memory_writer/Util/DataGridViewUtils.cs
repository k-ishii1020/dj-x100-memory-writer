using System.Text;

namespace DJ_X100_memory_writer.Util
{
    internal class DataGridViewUtils
    {
        /// <summary>
        /// 文字列の表示長を計算します。
        /// ASCII文字は長さ1、非ASCII文字（全角文字を含む）は長さ2として計算します。
        /// </summary>
        /// <param name="str">長さを計算する文字列。</param>
        /// <returns>文字列の表示長。</returns>
        public int GetDisplayLength(string str)
        {
            int len = 0;
            foreach (char c in str)
            {
                if (c > '\u007f')
                {
                    len += 2;
                }
                else
                {
                    len += 1;
                }
            }
            return len;
        }

        /// <summary>
        /// 入力文字列を半角に変換します。
        /// このメソッドでは、全角記号・英数字・かな・カナと全角スペースを半角に変換します。
        /// それ以外の文字はそのままにします。
        /// </summary>
        /// <param name="input">半角に変換する文字列。</param>
        /// <returns>半角に変換された文字列。</returns>
        public string ConvertToHalfWidth(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if ('\uFF01' <= c && c <= '\uFF5E') // 全角記号・英数字・かな・カナ
                {
                    sb.Append((char)(c - 0xFEE0));
                }
                else if (c == '\u3000') // 全角スペース
                {
                    sb.Append('\u0020');
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 文字列を指定された最大長までカットします。
        /// ASCII文字は長さ1、非ASCII文字（全角文字を含む）は長さ2として計算します。
        /// </summary>
        /// <param name="input">カットする文字列。</param>
        /// <param name="maxLength">最大長。</param>
        /// <returns>指定された最大長までカットされた文字列。</returns>
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
