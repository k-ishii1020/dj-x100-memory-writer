using DJ_X100_memory_writer.Util;

namespace DJ_X100_memory_writer.domain
{
    internal class Validator
    {
        DataGridViewUtils dataGridViewUtils = new DataGridViewUtils();

        public string ValidateMemoryNameColumn(string input, int rowIndex, int columnIndex, bool isCsvImport)
        {
            string errorMessage = string.Empty;

            int inputLength = dataGridViewUtils.GetDisplayLength(input);
            if (inputLength > 28)
            {
                string convertedInput = dataGridViewUtils.ConvertToHalfWidth(input);
                int convertedInputLength = dataGridViewUtils.GetDisplayLength(convertedInput);

                if (convertedInputLength <= 28)
                {
                    errorMessage = isCsvImport
                        ? $"エラー: 行 {rowIndex + 1} の 'ネーム' 列は半角28文字、全角14文字以内にしてください。オーバーした部分は自動的にカットされました。"
                        : $"行 {rowIndex + 1} の 'ネーム' 列は半角28文字、全角14文字以内にしてください。\n半角に変換すると規定文字数に収まります。\n変換しますか？";
                }
                else
                {
                    errorMessage = $"エラー: 行 {rowIndex + 1} の 'ネーム' 列は半角28文字、全角14文字以内にしてください。オーバーした部分は自動的にカットされました。";
                }
            }

            return errorMessage;
        }



    }
}
